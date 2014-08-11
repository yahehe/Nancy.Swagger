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

            var modelsData = this.RetrieveSwaggerModelData();
            var modelsForRoutes = this.GetModelsForRoutes(routeData, modelsData);

            apiDeclaration.Models = modelsForRoutes.SelectMany(model => CreateModel(model))
                                              .OrderBy(m => m.Id)
                                              .ToDictionary(m => m.Id, m => m);

            return apiDeclaration;
        }

        protected abstract IEnumerable<SwaggerRouteData> RetrieveSwaggerRouteData();

        protected abstract IEnumerable<SwaggerModelData> RetrieveSwaggerModelData();

        protected virtual IEnumerable<SwaggerModelData> GetModelsForRoutes(
            IEnumerable<SwaggerRouteData> routeData,
            IEnumerable<SwaggerModelData> modelData)
        {
            return GetDistinctModelTypes(routeData).Select(type => EnsureModelData(type, modelData));
        }

        protected virtual IEnumerable<Type> GetDistinctModelTypes(IEnumerable<SwaggerRouteData> routeData)
        {
            return GetOperationModels(routeData)
                        .Union(GetParameterModels(routeData))
                        .Select(type =>
                        {
                            if (type.IsContainer())
                            {
                                return type.GetElementType() ?? type.GetGenericArguments().FirstOrDefault();
                            }
                            return type;
                        })
                        .Where(type => !Primitive.IsPrimitive(type))
                        .Distinct();
        }

        private SwaggerModelData EnsureModelData(Type type, IEnumerable<SwaggerModelData> modelData)
        {
            if (modelData.Any(d => d.ModelType == type))
            {
                return modelData.First(d => d.ModelType == type);
            }

            return new SwaggerModelData(type);
        }

        private IEnumerable<Model> CreateModel(SwaggerModelData model)
        {
             var models = new List<Model>();

            var classProperties = model.Properties.Where(x => !Primitive.IsPrimitive(x.Type));

             var modelsData = this.RetrieveSwaggerModelData();

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
                    ? properties.OrderBy(x => x.Name).ToDictionary(p => p.Name, p => CreateModelProperty(p))
                    : modelDataForClassProperty.Properties.OrderBy(x => x.Name)
                        .ToDictionary(p => p.Name, p => CreateModelProperty(p));

                var item = new Model()
                {
                    Id = id,
                    Description = description,
                    Required = required,
                    Properties = modelproperties
                };

                models.Add(item);
            }

            var toplevelmodel = new Model
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
                                  .ToDictionary(p => p.Name, p => CreateModelProperty(p))
                // TODO: SubTypes and Discriminator
            };
            models.Add(toplevelmodel);

            return models;
        }

        private ModelProperty CreateModelProperty(SwaggerModelPropertyData modelPropertyData)
        {
            var propertyType = modelPropertyData.Type;

            var classProperty = !Primitive.IsPrimitive(propertyType);

            var modelProperty = modelPropertyData.Type.ToDataType<ModelProperty>(topLevelModel: classProperty);
            modelProperty.DefaultValue = modelPropertyData.DefaultValue;
            modelProperty.Description = modelPropertyData.Description;
            modelProperty.Enum = modelPropertyData.Enum;
            modelProperty.Minimum = modelPropertyData.Minimum;
            modelProperty.Maximum = modelPropertyData.Maximum;

            if (modelPropertyData.Type.IsContainer()) {
                modelProperty.UniqueItems = modelPropertyData.UniqueItems;
            }

            return modelProperty;
        }

        private static Api GetApi(IGrouping<string, SwaggerRouteData> @group)
        {
            return new Api
            {
                Path = @group.Key,
                Operations = @group.Select(d => d.ToOperation()).OrderBy(o => o.Method)
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

        private IEnumerable<SwaggerModelPropertyData> GetPropertiesFromType(Type type)
        {
            return type.GetProperties()
                .Select(property => new SwaggerModelPropertyData
                {
                    Name = property.Name,
                    Type = property.PropertyType
                });
        }
    }
}