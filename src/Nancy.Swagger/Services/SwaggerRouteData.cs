using System;
using System.Collections.Generic;
using Swagger.ObjectModel;

namespace Nancy.Swagger.Services
{
    public class SwaggerRouteData
    {
        public SwaggerRouteData(string path, PathItem pathItem)
        {
            Path = path;
            PathItem = pathItem;
            Types = new Dictionary<HttpMethod, Type>();
        }

        public string Path { get; private set; }
        public PathItem PathItem { get; private set; }
        public Dictionary<HttpMethod, Type> Types { get; private set; }

        public SwaggerRouteData Combine(SwaggerRouteData data)
        {
            var combined = new SwaggerRouteData(Path, PathItem.Combine(data.PathItem));
            foreach (var kvp in Types)
            {
                combined.Types.Add(kvp.Key, kvp.Value);
            }
            foreach (var kvp in data.Types)
            {
                combined.Types.Add(kvp.Key, kvp.Value);
            }
            return combined;
        }
    }
}