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

        protected override IList<SwaggerRouteData> RetrieveSwaggerRouteData()
        {
            return _routeCacheProvider
                .GetCache()
                .RetrieveMetadata<SwaggerRouteData>()
                .OfType<SwaggerRouteData>()
                .ToList(); // filter nulls
        }

        protected override IList<SwaggerModelData> RetrieveSwaggerModelData()
        {
            return _modelCatalog.ToList();
        }
    }
}