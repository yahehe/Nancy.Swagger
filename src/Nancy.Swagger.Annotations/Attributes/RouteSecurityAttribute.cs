using System;
using Swagger.ObjectModel;

namespace Nancy.Swagger.Annotations.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class RouteSecurityAttribute : Attribute
    {
        public RouteSecurityAttribute()
        {
            
        }

        public RouteSecurityAttribute(SecuritySchemes scheme)
        {
            Scheme = scheme;
        }

        public SecuritySchemes Scheme { get; set; }

        public string[] Scopes { get; set; }
    }
}