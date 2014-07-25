using System.Collections.Generic;
using System.Linq;
using Nancy.Swagger.Services;

namespace Nancy.Swagger.Tests.Services
{
    public class SwaggerMetadataConverterForTesting : SwaggerMetadataConverter
    {
        public SwaggerMetadataConverterForTesting()
        {
            RouteDataAccessor = Enumerable.Empty<SwaggerRouteData>();
            ModelDataAccessor = Enumerable.Empty<SwaggerModelData>();
        }

        public IEnumerable<SwaggerRouteData> RouteDataAccessor { get; set; }

        public IEnumerable<SwaggerModelData> ModelDataAccessor { get; set; }

        protected override IEnumerable<SwaggerRouteData> RetrieveSwaggerRouteData()
        {
            return RouteDataAccessor;
        }

        protected override IEnumerable<SwaggerModelData> RetrieveSwaggerModelData()
        {
            return ModelDataAccessor;
        }
    }
}