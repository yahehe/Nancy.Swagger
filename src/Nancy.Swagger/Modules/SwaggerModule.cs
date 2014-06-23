using System;
using System.Collections.Generic;
using System.Linq;

using Nancy.Routing;
using Nancy.Swagger.Services;
using Swagger.Model.ApiDeclaration;
using Swagger.Model.ResourceListing;

namespace Nancy.Swagger.Modules
{
    public class SwaggerModule : NancyModule
    {
        public SwaggerModule(IRouteCacheProvider routeCacheProvider, ISwaggerMetadataConverter converter)
            : base(SwaggerConfig.ResourceListingPath)
        {
            Get["/"] = _ =>
            {
                var metadata = routeCacheProvider
                    .GetCache()
                    .RetrieveMetadata<SwaggerRouteData>()
                    .OfType<SwaggerRouteData>(); // filter nulls

                return converter.GetResourceListing(metadata).ToJson();
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

                return converter.GetApiDeclaration(metadata).ToJson();
            };
        }
    }
}