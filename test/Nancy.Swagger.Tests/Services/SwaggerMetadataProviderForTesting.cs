using System.Collections.Generic;
using System.Linq;
using Nancy.Swagger.Services;
using Swagger.ObjectModel;

namespace Nancy.Swagger.Tests.Services
{
    public class SwaggerMetadataProviderForTesting : SwaggerMetadataProvider
    {
        public SwaggerMetadataProviderForTesting()
        {
            RouteDataAccessor = new Dictionary<string, PathItem>();
        }

        public IDictionary<string, PathItem> RouteDataAccessor { get; set; }

        protected override IDictionary<string, PathItem> RetrieveSwaggerRouteData()
        {
            return RouteDataAccessor.ToDictionary(x => x.Key, x => x.Value);
        }
    }
}