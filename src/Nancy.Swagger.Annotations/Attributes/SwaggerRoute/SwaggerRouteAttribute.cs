using System;

namespace Nancy.Swagger.Annotations.Attributes.SwaggerRoute
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public abstract class SwaggerRouteAttribute : Attribute
    {
        protected SwaggerRouteAttribute(string method, string path)
        {
            Method = method;
            Path = path;
        }

        public string Method { get; private set; }

        public string Path { get; private set; }

        public string Notes { get; set; }

        public string Summary { get; set; }

        public Type Type { get; set; }
    }
}