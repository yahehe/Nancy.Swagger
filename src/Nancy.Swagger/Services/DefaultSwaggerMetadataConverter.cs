using System.Collections.Generic;
using System.Linq;
using Nancy.Routing;
using Nancy.Swagger.Builders;
using Swagger.ObjectModel;

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

        protected override IDictionary<string, PathItem> RetrieveSwaggerRouteData()
        {
            return _routeCacheProvider.GetCache()
                               .RetrieveMetadata<SwaggerRouteDataBuilder>()
                               .OfType<SwaggerRouteDataBuilder>()
                               .SelectMany(x => x.PathItems)
                               .ToDictionary(x => x.Key, x => x.Value);
        }
    }
}