using System.Collections.Generic;
using System.Linq;
using Nancy.Routing;
using Swagger.ObjectModel;

namespace Nancy.Swagger.Services
{
    [SwaggerApi]
    public class DefaultSwaggerMetadataProvider : SwaggerMetadataProvider
    {
        private readonly IRouteCacheProvider routeCacheProvider;

        private readonly ISwaggerModelCatalog modelCatalog;

        public DefaultSwaggerMetadataProvider(IRouteCacheProvider routeCacheProvider, ISwaggerModelCatalog modelCatalog)
        {
            this.routeCacheProvider = routeCacheProvider;
            this.modelCatalog = modelCatalog;
        }

        protected override IDictionary<string, SwaggerRouteData> RetrieveSwaggerPaths()
        {
            var pathItems = new Dictionary<string, SwaggerRouteData>();
            foreach (var routeDescription in this.routeCacheProvider.GetCache()
                                                             .SelectMany(x => x.Value)
                                                             .Select(x => x.Item2))
            {
                var pathItem = routeDescription.Metadata.Retrieve<PathItem>();
                if (pathItem != null)
                {
                    SwaggerRouteData entry;
                    if (pathItems.TryGetValue(routeDescription.Path, out entry))
                    {
                        pathItems[routeDescription.Path] = entry.Combine(new SwaggerRouteData(routeDescription.Path, pathItem));
                    }
                    else
                    {
                        pathItems.Add(routeDescription.Path, new SwaggerRouteData(routeDescription.Path, pathItem));
                    }
                }
            }

            return pathItems;
        }

        protected override IList<SwaggerModelData> RetrieveSwaggerModels()
        {
            return this.modelCatalog.ToList();
        }
    }
}