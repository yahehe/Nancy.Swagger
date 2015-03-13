using System;
using System.Collections.Generic;
using Swagger.ObjectModel;
using Swagger.ObjectModel.Builders;

namespace Nancy.Swagger.Builders
{
    public class SwaggerRouteDataBuilder
    {
        private readonly Dictionary<string, PathItem> pathItems = new Dictionary<string, PathItem>();

        public SwaggerRouteDataBuilder AddPath(string path, Action<PathItemBuilder> pathItemAction)
        {
            var pathItemBuilder = new PathItemBuilder();
            pathItemAction(pathItemBuilder);
            pathItems.Add(path, pathItemBuilder.Build());
            return this;
        }

        public IEnumerable<KeyValuePair<string, PathItem>> PathItems
        {
            get
            {
                return pathItems;
            }
        }
    }
}
