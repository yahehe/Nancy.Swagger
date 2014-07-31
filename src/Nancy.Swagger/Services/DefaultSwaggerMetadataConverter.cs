using System.Collections.Generic;
using System.Linq;
using Nancy.Routing;

namespace Nancy.Swagger.Services
{
    [SwaggerApi]
    public class DefaultSwaggerMetadataConverter : SwaggerMetadataConverter
    {
        private readonly IRouteCacheProvider _routeCacheProvider;

        private readonly ISwaggerModelCatalog _modelCatalog;

        public DefaultSwaggerMetadataConverter(
            IRouteCacheProvider routeCacheProvider,
            ISwaggerModelCatalog modelCatalog)
        {
            _routeCacheProvider = routeCacheProvider;
            _modelCatalog = modelCatalog;
        }

        protected override IEnumerable<SwaggerRouteData> RetrieveSwaggerRouteData()
        {
            return _routeCacheProvider
                .GetCache()
                .RetrieveMetadata<SwaggerRouteData>()
                .OfType<SwaggerRouteData>(); // filter nulls
        }

        protected override IEnumerable<SwaggerModelData> RetrieveSwaggerModelData()
        {
            return this._modelCatalog;
        }
    }
}