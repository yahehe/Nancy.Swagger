using System;
using System.Linq;

using Nancy.Routing;
using Nancy.Swagger.ApiDeclaration;
using Nancy.Swagger.ResourceListing;

using Newtonsoft.Json;

namespace Nancy.Swagger.Modules
{
    using System.Collections.Generic;

    public class SwaggerModule : NancyModule
    {
        public SwaggerModule(IRouteCacheProvider routeCacheProvider)
            : base(SwaggerConfig.ResourceListingPath)
        {
            this.Get["/"] = _ =>
            {
                var metadata = routeCacheProvider
                    .GetCache()
                    .RetrieveMetadata<SwaggerRouteData>()
                    .OfType<SwaggerRouteData>(); // filter nulls

                var resourceListing = new ResourceListing.ResourceListing
                {
                    Apis = metadata
                        .Select(d => d.ResourcePath)
                        .Distinct()
                        .Select(path => new Resource { Path = path })
                };

                return JsonConvert.SerializeObject(resourceListing);
            };

            this.Get["/{resourcePath*}"] = _ =>
            {
                string path = "/" + _.resourcePath;
                var metadata = routeCacheProvider
                    .GetCache()
                    .RetrieveMetadata<SwaggerRouteData>()
                    .OfType<SwaggerRouteData>() // filter nulls
                    .Where(d => d.ResourcePath == path);

                var apiDeclaration = new ApiDeclaration.ApiDeclaration
                {
                    BasePath = new Uri("/", UriKind.Relative),
                    Apis =
                        metadata.GroupBy(d => d.ApiPath).Select(
                            group =>
                            new Api
                            {
                                Path = group.Key,
                                Operations = group.Select(d => d.ToOperation())
                            }),
                };

                var models = GetOperationModels(metadata).Union(GetParameterModels(metadata)).Distinct();

                apiDeclaration.Models = models.Select(t => t.DefaultModelId())
                        .Select(id => new Model { Id = id })
                        .ToDictionary(m => m.Id, m => m);

                return JsonConvert.SerializeObject(apiDeclaration);
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