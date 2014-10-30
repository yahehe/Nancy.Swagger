using System.Collections.Generic;
using System.Linq;
using Nancy.Swagger.Services;

namespace Nancy.Swagger.Tests.Services
{
    public class SwaggerMetadataProviderForTesting : SwaggerMetadataProvider
    {
        public SwaggerMetadataProviderForTesting()
        {
            RouteDataAccessor = Enumerable.Empty<SwaggerRouteData>();
            ModelDataAccessor = Enumerable.Empty<SwaggerModelData>();
        }

        public IEnumerable<SwaggerRouteData> RouteDataAccessor { get; set; }

        public IEnumerable<SwaggerModelData> ModelDataAccessor { get; set; }

        public override IList<SwaggerRouteData> RetrieveSwaggerRouteData()
        {
            return RouteDataAccessor.ToList();
        }

        public override IList<SwaggerModelData> RetrieveSwaggerModelData()
        {
            return ModelDataAccessor.ToList();
        }
    }
}