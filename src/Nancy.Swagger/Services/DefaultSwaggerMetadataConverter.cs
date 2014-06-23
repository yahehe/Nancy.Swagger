using System;
using System.Collections.Generic;
using System.Linq;
using Swagger.Model.ApiDeclaration;
using Swagger.Model.ResourceListing;

namespace Nancy.Swagger.Services
{
    public class DefaultSwaggerMetadataConverter : ISwaggerMetadataConverter
    {
        public ResourceListing GetResourceListing(IEnumerable<SwaggerRouteData> routeData)
        {
            return new ResourceListing
            {
                Apis = routeData
                    .Select(d => d.ResourcePath)
                    .Distinct()
                    .Select(path => new Resource { Path = path })
            };
        }

        public ApiDeclaration GetApiDeclaration(IEnumerable<SwaggerRouteData> routeData)
        {
            var apiDeclaration = new ApiDeclaration
            {
                BasePath = new Uri("/", UriKind.Relative),
                Apis = routeData.GroupBy(d => d.ApiPath).Select(GetApi)
            };

            var models = GetOperationModels(routeData).Union(GetParameterModels(routeData)).Distinct();

            apiDeclaration.Models = models.Select(t => t.DefaultModelId())
                    .Select(id => new Model { Id = id })
                    .ToDictionary(m => m.Id, m => m);

            return apiDeclaration;
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