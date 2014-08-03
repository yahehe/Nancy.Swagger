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
                Apis = routeData.GroupBy(d => d.ApiPath).Select(GetApi)
            };

            var modelsData = this.RetrieveSwaggerModelData();
            var modelsForRoutes = this.GetModelsForRoutes(routeData, modelsData);

            apiDeclaration.Models = modelsForRoutes.Select(model => CreateModel(model))
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

        private Model CreateModel(SwaggerModelData model)
        {
            return new Model
            {
                Id = model.ModelType.DefaultModelId(),
                Description = model.Description,
                Required = model.Properties
                                .Where(p => p.Required || p.Type.IsImplicitlyRequired())
                                .Select(p => p.Name)
                                .ToList(),
                Properties = model.Properties
                                  .ToDictionary(p => p.Name, p => CreateModelProperty(p))
                // TODO: SubTypes and Discriminator
            };
        }

        private ModelProperty CreateModelProperty(SwaggerModelPropertyData modelPropertyData)
        {
            var propertyType = modelPropertyData.Type;

            var modelProperty = modelPropertyData.Type.ToDataType<ModelProperty>();
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
                Operations = @group.Select(d => d.ToOperation())
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
    }
}