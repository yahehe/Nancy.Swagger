using System;
using System.Collections.Generic;
using System.Linq;
using Swagger.ObjectModel;
using Swagger.ObjectModel.ApiDeclaration;
using Swagger.ObjectModel.ResourceListing;

namespace Nancy.Swagger.Services
{
    [SwaggerApi]
    public abstract class SwaggerMetadataConverter : ISwaggerMetadataConverter
    {
        public ResourceListing GetResourceListing()
        {
            return new ResourceListing
            {
                Apis = RetrieveSwaggerRouteData()
                    .Select(d => d.ResourcePath)
                    .Distinct()
                    .Select(path => new Resource { Path = path })
                    .OrderBy(resource => resource.Path)
            };
        }

        public ApiDeclaration GetApiDeclaration(string resourcePath)
        {
            var routeData = RetrieveSwaggerRouteData()
                .Where(d => d.ResourcePath == resourcePath)
                .ToList();

            var apiDeclaration = new ApiDeclaration
            {
                BasePath = new Uri("/", UriKind.Relative),
                Apis = routeData.GroupBy(d => d.ApiPath).Select(GetApi).OrderBy(api => api.Path)
            };

            var modelsData = RetrieveSwaggerModelData();
            var modelsForRoutes = GetModelsForRoutes(routeData, modelsData);

            apiDeclaration.Models = modelsForRoutes.SelectMany(CreateModel)
                .OrderBy(m => m.Id)
                .ToDictionary(m => m.Id, m => m);

            return apiDeclaration;
        }

        protected abstract IList<SwaggerRouteData> RetrieveSwaggerRouteData();

        protected abstract IList<SwaggerModelData> RetrieveSwaggerModelData();

        protected IEnumerable<SwaggerModelData> GetModelsForRoutes(
            IList<SwaggerRouteData> routeData,
            IList<SwaggerModelData> modelData)
        {
            return GetDistinctModelTypes(routeData).Select(type => EnsureModelData(type, modelData));
        }

        protected IEnumerable<Type> GetDistinctModelTypes(IList<SwaggerRouteData> routeData)
        {
            return GetOperationModels(routeData)
                .Union(GetParameterModels(routeData))
                .Select(GetType)
                .Where(type => !Primitive.IsPrimitive(type))
                .Distinct();
        }

        private static Type GetType(Type type)
        {
            if (type.IsContainer())
            {
                return type.GetElementType() ?? type.GetGenericArguments().First();
            }

            return type;
        }

        private SwaggerModelData EnsureModelData(Type type, IList<SwaggerModelData> modelData)
        {
            return modelData.FirstOrDefault(x => x.ModelType == type) ?? new SwaggerModelData(type);
        }

        private IEnumerable<Model> CreateModel(SwaggerModelData model)
        {
            var classProperties = model.Properties.Where(x => !Primitive.IsPrimitive(x.Type) && !x.Type.IsEnum && !x.Type.IsGenericType);

            var modelsData = RetrieveSwaggerModelData();

            foreach (var swaggerModelPropertyData in classProperties)
            {
                var properties = GetPropertiesFromType(swaggerModelPropertyData.Type);

                var modelDataForClassProperty =
                    modelsData.FirstOrDefault(x => x.ModelType == swaggerModelPropertyData.Type);

                var id = modelDataForClassProperty == null
                    ? swaggerModelPropertyData.Type.Name
                    : modelDataForClassProperty.ModelType.DefaultModelId();

                var description = modelDataForClassProperty == null
                    ? swaggerModelPropertyData.Description
                    : modelDataForClassProperty.Description;

                var required = modelDataForClassProperty == null
                    ? properties.Where(p => p.Required || p.Type.IsImplicitlyRequired())
                        .Select(p => p.Name)
                        .OrderBy(name => name)
                        .ToList()
                    : modelDataForClassProperty.Properties
                        .Where(p => p.Required || p.Type.IsImplicitlyRequired())
                        .Select(p => p.Name)
                        .OrderBy(name => name)
                        .ToList();

                var modelproperties = modelDataForClassProperty == null
                    ? properties.OrderBy(x => x.Name).ToDictionary(p => p.Name, CreateModelProperty)
                    : modelDataForClassProperty.Properties.OrderBy(x => x.Name)
                        .ToDictionary(p => p.Name, CreateModelProperty);

                yield return new Model
                {
                    Id = id,
                    Description = description,
                    Required = required,
                    Properties = modelproperties
                };
            }

            var topLevelModel = new Model
            {
                Id = model.ModelType.DefaultModelId(),
                Description = model.Description,
                Required = model.Properties
                    .Where(p => p.Required || p.Type.IsImplicitlyRequired())
                    .Select(p => p.Name)
                    .OrderBy(name => name)
                    .ToList(),
                Properties = model.Properties
                    .OrderBy(p => p.Name)
                    .ToDictionary(p => p.Name, CreateModelProperty)

                // TODO: SubTypes and Discriminator
            };

            yield return topLevelModel;
        }

        private ModelProperty CreateModelProperty(SwaggerModelPropertyData modelPropertyData)
        {
            var propertyType = modelPropertyData.Type;

            var isClassProperty = !Primitive.IsPrimitive(propertyType);

            var modelProperty = modelPropertyData.Type.ToDataType<ModelProperty>(isClassProperty);
            
            modelProperty.DefaultValue = modelPropertyData.DefaultValue;
            modelProperty.Description = modelPropertyData.Description;
            modelProperty.Enum = modelPropertyData.Enum;
            modelProperty.Minimum = modelPropertyData.Minimum;
            modelProperty.Maximum = modelPropertyData.Maximum;

            if (modelPropertyData.Type.IsContainer())
            {
                modelProperty.UniqueItems = modelPropertyData.UniqueItems;
            }

            return modelProperty;
        }

        private static Api GetApi(IGrouping<string, SwaggerRouteData> @group)
        {
            return new Api
            {
                Path = @group.Key,
                Operations = @group
                    .Select(d => d.ToOperation())
                    .OrderBy(o => o.Method)
            };
        }

        private static IEnumerable<Type> GetOperationModels(IEnumerable<SwaggerRouteData> metadata)
        {
            return metadata
                .Where(d => d.OperationModel != null)
                .Select(d => d.OperationModel);
        }

        private static IEnumerable<Type> GetParameterModels(IEnumerable<SwaggerRouteData> metadata)
        {
            return metadata
                .SelectMany(d => d.OperationParameters)
                .Where(p => p.ParameterModel != null)
                .Select(p => p.ParameterModel);
        }

        private IList<SwaggerModelPropertyData> GetPropertiesFromType(Type type)
        {
            return type.GetProperties()
                .Select(property => new SwaggerModelPropertyData
                {
                    Name = property.Name,
                    Type = property.PropertyType
                }).ToList();
        }
    }
}