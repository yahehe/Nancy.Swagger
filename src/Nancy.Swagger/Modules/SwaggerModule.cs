using System;
using System.Collections.Generic;
using System.Linq;

using Nancy.Routing;
using Swagger.Model.ApiDeclaration;
using Swagger.Model.ResourceListing;

namespace Nancy.Swagger.Modules
{
    public class SwaggerModule : NancyModule
    {
        public SwaggerModule(IRouteCacheProvider routeCacheProvider)
            : base(SwaggerConfig.ResourceListingPath)
        {
            Get["/"] = _ =>
            {
                var metadata = routeCacheProvider
                    .GetCache()
                    .RetrieveMetadata<SwaggerRouteData>()
                    .OfType<SwaggerRouteData>(); // filter nulls

                var resourceListing = new ResourceListing
                {
                    Apis = metadata
                        .Select(d => d.ResourcePath)
                        .Distinct()
                        .Select(path => new Resource { Path = path })
                };

                return resourceListing.ToJson();
            };

            Get["/{resourcePath*}"] = _ =>
            {
                string path = "/" + _.resourcePath;
                var metadata = routeCacheProvider
                    .GetCache()
                    .RetrieveMetadata<SwaggerRouteData>()
                    .OfType<SwaggerRouteData>() // filter nulls
                    .Where(d => d.ResourcePath == path)
                    .ToList();

                var apiDeclaration = new ApiDeclaration
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

                return apiDeclaration.ToJson();
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