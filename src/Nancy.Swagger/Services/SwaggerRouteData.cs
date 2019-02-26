using System;
using System.Collections.Generic;
using Swagger.ObjectModel;
using System.Text.RegularExpressions;

namespace Nancy.Swagger.Services
{
    public class SwaggerRouteData
    {
        public SwaggerRouteData(string path, PathItem pathItem)
        {
            Path = RemovePathParameterTypes(path);
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

        /// <summary>
        /// Removes parameter types from Nancy routes - Swagger doesn't expect them.
        /// Examples: "/service/customers/{name:guid}" becomes "/service/customers/{name}"
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private string RemovePathParameterTypes(string path)
        {
            return Regex.Replace(path, @":\w+}", "}");
        }
    }
}