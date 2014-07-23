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

            var modelsData = RetrieveSwaggerModelsData(routeData);

            apiDeclaration.Models = modelsData.Select(model => CreateModel(model))
                                              .ToDictionary(m => m.Id, m => (object)m);

            return apiDeclaration;
        }

        protected abstract IEnumerable<SwaggerRouteData> RetrieveSwaggerRouteData();



        protected virtual IEnumerable<SwaggerModelData> RetrieveSwaggerModelsData(IEnumerable<SwaggerRouteData> routeData)
        {
            return GetOperationModels(routeData)
                        .Union(GetParameterModels(routeData))
                        .Distinct()
                        .Select(type => new SwaggerModelData
                        {
                            ModelType = type
                            // TODO: implement and use the DSL liddelj described in 
                            //       https://github.com/khellang/Nancy.Swagger/pull/3 and
                            //       https://github.com/khellang/Nancy.Swagger/pull/5
                        });
        }

        private Model CreateModel(SwaggerModelData model)
        {
            return new Model
            {
                Id = model.ModelType.DefaultModelId(),
                Description = model.Description,
                Required = model.Properties
                                .Where(p => p.Required)
                                .Select(p => p.Name)
                                .ToList(),
                Properties = model.Properties
                                  .ToDictionary(p => p.Name, p => (object)CreateModelProperty(p))
                // TODO: SubTypes and Discriminator
            };
        }

        private ModelProperty CreateModelProperty(SwaggerModelPropertyData modelPropertyData)
        {
            var propertyType = modelPropertyData.Type;

            var modelProperty = new ModelProperty
            {
                DefaultValue = modelPropertyData.DefaultValue,
                Description = modelPropertyData.Description,
                Enum = modelPropertyData.Enum,
                Minimum = modelPropertyData.Minimum,
                Maximum = modelPropertyData.Maximum
            };

            if (Primitive.IsPrimitive(propertyType))
            {
                var primitive = Primitive.FromType(propertyType);
                modelProperty.Format = primitive.Format;
                modelProperty.Type = primitive.Type;
            }
            else if (propertyType.IsContainer())
            {
                modelProperty.Type = "array";
                modelProperty.UniqueItems = modelPropertyData.UniqueItems;

                var itemsType = propertyType.GetElementType() ?? propertyType.GetGenericArguments().FirstOrDefault();
                if (Primitive.IsPrimitive(itemsType))
                {
                    var itemsPrimitive = Primitive.FromType(itemsType);
                    modelProperty.Items = new Items
                    {
                        Type = itemsPrimitive.Type,
                        Format = itemsPrimitive.Format
                    };
                }
                else
                {
                    modelProperty.Items = new Items { Ref = itemsType.DefaultModelId() };
                }
            }
            else
            {
                modelProperty.Ref = propertyType.DefaultModelId();
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