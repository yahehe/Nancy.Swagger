using System.Collections.Generic;
using System.Linq;
using Nancy.Routing;
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
            var pathItems = new Dictionary<string, PathItem>();
            foreach (var routeDescription in _routeCacheProvider.GetCache()
                                                             .SelectMany(x => x.Value)
                                                             .Select(x => x.Item2))
            {
                var pathItem = routeDescription.Metadata.Retrieve<PathItem>();
                if (pathItem != null)
                {
                    PathItem entry;
                    if (pathItems.TryGetValue(routeDescription.Path, out entry))
                    {
                        pathItems[routeDescription.Path] = entry.Combine(pathItem);
                    }
                    else
                    {
                        pathItems.Add(routeDescription.Path, pathItem);
                    }
                }
            }
            return pathItems;
        }
    }
}