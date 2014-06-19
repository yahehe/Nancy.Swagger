using System;
using System.Linq;

using Nancy.Routing;
using Nancy.Swagger.ApiDeclaration;
using Nancy.Swagger.ResourceListing;

using Newtonsoft.Json;

namespace Nancy.Swagger.Modules
{
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
                                Operations =
                                    group.Select(
                                        desc =>
                                        new Operation
                                        {
                                            Nickname = desc.OperationNickname,
                                            Summary = desc.OperationSummary,
                                            Method = desc.OperationMethod,
                                            Notes = desc.OperationNotes,
                                            Parameters = desc.OperationParameters,
                                            ResponseMessages = desc.OperationResponseMessages,
                                            Produces = desc.OperationProduces,
                                            Consumes = desc.OperationConsumes,
                                            Type = desc.OperationType
                                        })
                            })
                };

                return JsonConvert.SerializeObject(apiDeclaration);
            };
        }
    }
}