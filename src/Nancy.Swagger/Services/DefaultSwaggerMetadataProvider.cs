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

        private readonly ISwaggerTagCatalog tagCatalog;

        public DefaultSwaggerMetadataProvider(IRouteCacheProvider routeCacheProvider, ISwaggerModelCatalog modelCatalog, ISwaggerTagCatalog tagCatalog)
        {
            this.routeCacheProvider = routeCacheProvider;
            this.modelCatalog = modelCatalog;
            this.tagCatalog = tagCatalog;
        }

        protected override IDictionary<string, SwaggerRouteData> RetrieveSwaggerPaths(NancyContext context)
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

        protected override IList<Tag> RetrieveSwaggerTags()
        {
            return this.tagCatalog.ToList();
        }
    }
}