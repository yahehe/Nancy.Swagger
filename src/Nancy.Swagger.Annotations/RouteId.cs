using Nancy.Routing;
using Nancy.Swagger.Annotations.Attributes;
using Swagger.Model.ApiDeclaration;
using System.Diagnostics;

namespace Nancy.Swagger.Annotations
{
    [DebuggerDisplay("{Method} {Path}")]
    internal struct RouteId
    {
        public HttpMethod Method { get; set; }

        public string Path { get; set; }

        public static RouteId Create(Route route)
        {
            return new RouteId
            {
                Method = route.Description.Method.ToHttpMethod(),
                Path = route.Description.Path.EnsureForwardSlash()
            };
        }

        public static RouteId Create(INancyModule module, SwaggerRouteAttribute swaggerRouteAttribute)
        {
            return new RouteId
            {
                Method = swaggerRouteAttribute.Method.ToHttpMethod(),
                Path = module.ModulePath.EnsureForwardSlash() + swaggerRouteAttribute.Path
            };
        }
    }
}