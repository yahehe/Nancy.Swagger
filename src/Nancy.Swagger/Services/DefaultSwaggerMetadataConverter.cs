using System;
using System.Collections.Generic;
using System.Linq;
using Swagger.Model.ApiDeclaration;
using Swagger.Model.ResourceListing;
using Nancy.Routing;

namespace Nancy.Swagger.Services
{
    public class DefaultSwaggerMetadataConverter : SwaggerMetadataConverter
    {
        private readonly IRouteCacheProvider _routeCacheProvider;

        public DefaultSwaggerMetadataConverter(IRouteCacheProvider routeCacheProvider)
        {
            _routeCacheProvider = routeCacheProvider;
        }
        
        protected override IEnumerable<SwaggerRouteData> RetrieveSwaggerRouteData()
        {
            return _routeCacheProvider
                .GetCache()
                .RetrieveMetadata<SwaggerRouteData>()
                .OfType<SwaggerRouteData>(); // filter nulls
        }
    }
}