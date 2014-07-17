using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Nancy.Routing;
using Nancy.Swagger.Annotations.Attributes.SwaggerRoute;
using Nancy.Swagger.Annotations.Attributes.SwaggerRouteParameter;
using Nancy.Swagger.Services;
using Swagger.Model.ApiDeclaration;

namespace Nancy.Swagger.Annotations
{
    public class SwaggerAnnotationsConverter : SwaggerMetadataConverter
    {
        private NancyContext _context;
        private INancyModuleCatalog _moduleCatalog;

        public SwaggerAnnotationsConverter(INancyModuleCatalog moduleCatalog, NancyContext context)
        {
            _moduleCatalog = moduleCatalog;
            _context = context;
        }

        protected override IEnumerable<SwaggerRouteData> RetrieveSwaggerRouteData()
        {
            return _moduleCatalog
                .GetAllModules(_context)
                .SelectMany(ToSwaggerRouteData)
                .ToList();
        }

        private SwaggerParameterData CreateSwaggerParameterData(ParameterInfo pi)
        {
            var parameter = new SwaggerParameterData
            {
                Name = pi.Name,
                ParameterModel = pi.ParameterType
            };

            var paramAttr = pi.GetCustomAttributes(typeof(SwaggerRouteParameterAttribute), true).FirstOrDefault() as SwaggerRouteParameterAttribute;

            if (paramAttr == null)
            {
                parameter.Description = "Warning: no annotation found for this parameter";
                parameter.ParamType = ParameterType.Query; // Required, so use query as fallback

                return parameter;
            }

            parameter.Name = paramAttr.Name;
            parameter.ParamType = paramAttr.ParamType;
            parameter.Required = paramAttr.Required;
            parameter.Description = paramAttr.Description;

            return parameter;
        }

        private SwaggerRouteData CreateSwaggerRouteData(INancyModule module, Route route, Dictionary<RouteId, MethodInfo> routeHandlers)
        {
            var data = new SwaggerRouteData
            {
                ApiPath = route.Description.Path,
                ResourcePath = module.ModulePath.EnsureForwardSlash(),

                OperationMethod = route.Description.Method.ToHttpMethod(),
                OperationNickname = route.Description.Path,
            };

            var routeId = RouteId.Create(route);
            var handler = routeHandlers.ContainsKey(routeId) ? routeHandlers[routeId] : null;
            if (handler == null)
            {
                data.OperationNotes = "[example]"; // TODO: Insert example how to annotate a route
                data.OperationSummary = "Warning: no annotated method found for this route";

                return data;
            }

            var routeAttr = handler.GetAttribute<SwaggerRouteAttribute>();
            data.OperationSummary = routeAttr.Summary;
            data.OperationNotes = routeAttr.Notes;
            data.OperationModel = routeAttr.Type;

            data.OperationParameters = handler.GetParameters()
                .Select(CreateSwaggerParameterData)
                .ToList();

            return data;
        }

        private IEnumerable<SwaggerRouteData> ToSwaggerRouteData(INancyModule module)
        {
            // Discover route handlers and put them in a Dictionary<RouteId, MethodInfo>
            var routeHandlers =
                module.GetType().GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                    .Where(methodInfo => methodInfo.GetAttribute<SwaggerRouteAttribute>() != null)
                    .ToDictionary(
                        methodInfo => RouteId.Create(module, methodInfo.GetAttribute<SwaggerRouteAttribute>()),
                        methodInfo => methodInfo
                    );

            return module.Routes
                .Select(route => CreateSwaggerRouteData(module, route, routeHandlers));
        }
    }
}