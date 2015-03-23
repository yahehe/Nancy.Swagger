using System;
using Swagger.ObjectModel;

namespace Nancy.Swagger.Annotations.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class SwaggerRouteAttribute : Attribute
    {
        public SwaggerRouteAttribute()
            : this(default(HttpMethod), null)
        {
        }

        public SwaggerRouteAttribute(string name)
        {
            Name = name;
        }

        public SwaggerRouteAttribute(HttpMethod method, string path)
        {
            Method = method;
            Path = path;
        }

        public HttpMethod Method { get; private set; }

        public string Name { get; private set; }

        public string Notes { get; set; }

        public string Path { get; private set; }

        public Type Response { get; set; }

        public string Summary { get; set; }

        public string[] Produces { get; set; }

        public string[] Consumes { get; set; }
    }
}