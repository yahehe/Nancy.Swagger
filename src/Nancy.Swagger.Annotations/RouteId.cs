using System.Diagnostics;
using Nancy.Routing;
using Nancy.Swagger.Annotations.Attributes.SwaggerRoute;
using Swagger.ObjectModel.ApiDeclaration;

namespace Nancy.Swagger.Annotations
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    internal struct RouteId
    {
        public HttpMethod Method { get; private set; }

        public string Path { get; private set; }

        private string DebuggerDisplay
        {
            get { return string.Format("{0} {1}", Method, Path); }
        }

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