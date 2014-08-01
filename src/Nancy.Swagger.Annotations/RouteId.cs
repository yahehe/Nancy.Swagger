using Nancy.Routing;
using Nancy.Swagger.Annotations.Attributes;
using Swagger.ObjectModel.ApiDeclaration;
using System.Diagnostics;

namespace Nancy.Swagger.Annotations
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    internal struct RouteId
    {
        public bool IsValid
        {
            get
            {
                return Module != null
                    && (!string.IsNullOrEmpty(Name) || !string.IsNullOrEmpty(Path));
            }
        }

        public HttpMethod Method { get; private set; }

        public INancyModule Module { get; private set; }

        public string Name { get; private set; }

        public string Path { get; private set; }

        private string DebuggerDisplay
        {
            get
            {
                if (!IsValid)
                {
                    return "Invalid";
                }

                if (!string.IsNullOrEmpty(Name))
                {
                    return Name;
                }

                return string.Format("{0} {1}", Method, Path);
            }
        }

        public static RouteId Create(INancyModule module, Route route)
        {
            var routeId = new RouteId { Module = module };

            if (!string.IsNullOrEmpty(route.Description.Name))
            {
                routeId.Name = route.Description.Name;
            }
            else
            {
                routeId.Method = route.Description.Method.ToHttpMethod();
                routeId.Path = route.Description.Path.EnsureForwardSlash();
            }

            return routeId;
        }

        public static RouteId Create(INancyModule module, SwaggerRouteAttribute swaggerRouteAttribute)
        {
            var routeId = new RouteId { Module = module };

            if (!string.IsNullOrEmpty(swaggerRouteAttribute.Name))
            {
                routeId.Name = swaggerRouteAttribute.Name;
            }
            else if (swaggerRouteAttribute.Path != null)
            {
                routeId.Method = swaggerRouteAttribute.Method;
                routeId.Path = module.ModulePath.EnsureForwardSlash() + swaggerRouteAttribute.Path;
            }

            return routeId;
        }
    }
}