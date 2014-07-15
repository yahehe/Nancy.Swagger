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
        public SwaggerModule(ISwaggerMetadataConverter converter)
            : base(SwaggerConfig.ResourceListingPath)
        {
            Get["/"] = _ =>
            {
                return converter.GetResourceListing().ToJson();
            };

            Get["/{resourcePath*}"] = _ =>
            {
                return converter.GetApiDeclaration("/" + _.resourcePath).ToJson();
            };
        }
    }
}