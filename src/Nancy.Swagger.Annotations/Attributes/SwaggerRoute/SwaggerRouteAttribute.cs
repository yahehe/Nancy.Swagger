using System;

namespace Nancy.Swagger.Annotations.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class SwaggerRouteAttribute : Attribute
    {
        public SwaggerRouteAttribute(string method, string path)
        {
            Method = method;
            Path = path;
        }

        public string Method { get; private set; }

        public string Notes { get; set; }

        public string Path { get; private set; }

        public string Summary { get; set; }

        public Type Type { get; set; }
    }
}