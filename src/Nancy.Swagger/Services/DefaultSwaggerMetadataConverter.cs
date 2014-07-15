using System;
using System.Collections.Generic;
using System.Linq;
using Swagger.Model.ApiDeclaration;
using Swagger.Model.ResourceListing;
using Nancy.Routing;

namespace Nancy.Swagger.Services
{
    public class DefaultSwaggerMetadataConverter : ISwaggerMetadataConverter
    {
        IRouteCacheProvider routeCacheProvider;

        public DefaultSwaggerMetadataConverter(IRouteCacheProvider routeCacheProvider)
        {
            this.routeCacheProvider = routeCacheProvider;
        }

        protected virtual IEnumerable<SwaggerRouteData> RetrieveSwaggerRouteData()
        {
            return routeCacheProvider
                    .GetCache()
                    .RetrieveMetadata<SwaggerRouteData>()
                    .OfType<SwaggerRouteData>(); // filter nulls
        }

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