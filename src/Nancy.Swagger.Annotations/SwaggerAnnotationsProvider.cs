using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Nancy.Routing;
using Nancy.Swagger.Annotations.Attributes;
using Nancy.Swagger.Annotations.SwaggerObjects;
using Nancy.Swagger.Services;
using Swagger.ObjectModel;

namespace Nancy.Swagger.Annotations
{
    public class SwaggerAnnotationsProvider : SwaggerMetadataProvider
    {
        private NancyContext _context;
        private INancyModuleCatalog _moduleCatalog;
        private ISwaggerModelCatalog _modelCatalog;
        private ISwaggerTagCatalog _tagCatalog;

        public SwaggerAnnotationsProvider(INancyModuleCatalog moduleCatalog, NancyContext context, ISwaggerModelCatalog modelCatalog, ISwaggerTagCatalog tagCatalog)
        {
            _moduleCatalog = moduleCatalog;
            _context = context;
            _modelCatalog = modelCatalog;
            _tagCatalog = tagCatalog;
        }

        protected override IDictionary<string, SwaggerRouteData> RetrieveSwaggerPaths()
        {
            var pathItems = new Dictionary<string, SwaggerRouteData>();
            foreach (var pair in _moduleCatalog
                .GetAllModules(_context)
                .SelectMany(ToSwaggerRouteData))
            {
                SwaggerRouteData entry;
                if (pathItems.TryGetValue(pair.Path, out entry))
                {
                    pathItems[pair.Path] = entry.Combine(pair);
                }
                else
                {
                    pathItems.Add(pair.Path, pair);
                }
            }
            return pathItems;
        }


        protected override IList<SwaggerModelData> RetrieveSwaggerModels()
        {
            return _modelCatalog.Select(AnnotateSwaggerModelData).ToList();
        }

        protected override IList<Tag> RetrieveSwaggerTags()
        {
            return _tagCatalog.ToList();
        }

        private SwaggerModelData AnnotateSwaggerModelData(SwaggerModelData originalModel)
        {
            var type = originalModel.ModelType;

            var modelAttr = type.GetTypeInfo().GetCustomAttribute<ModelAttribute>();

            //If the type is not annotated, use the default model.
            return modelAttr == null ? originalModel : new AnnotatedModel(originalModel.ModelType, modelAttr);
        }

        private SwaggerRouteData CreateSwaggerRouteData(INancyModule module, Route route, Dictionary<RouteId, MethodInfo> routeHandlers)
        {
            var data = new SwaggerRouteData(route.Description.Path, new PathItem());

            var routeId = RouteId.Create(module, route);
            var handler = routeHandlers.ContainsKey(routeId) ? routeHandlers[routeId] : null;
            var operation = new AnnotatedOperation(route.Description.Name, handler, _modelCatalog);

            var method = route.Description.Method.ToHttpMethod();
            switch (route.Description.Method.ToLowerInvariant())
            {
                case "get":
                    data.PathItem.Get = operation;
                    break;
                case "post":
                    data.PathItem.Post = operation;
                    break;
                case "patch":
                    data.PathItem.Patch = operation;
                    break;
                case "delete":
                    data.PathItem.Delete = operation;
                    break;
                case "put":
                    data.PathItem.Put = operation;
                    break;
                case "head":
                    data.PathItem.Head = operation;
                    break;
                case "options":
                    data.PathItem.Options = operation;
                    break;
            }

            if (operation.ResponseType != null)
            {
                data.Types.Add(method, operation.ResponseType);
            }

            return data;
        }

        private bool ShowRoute(INancyModule module, Route route, Dictionary<RouteId, MethodInfo> routeHandlers)
        {
            var routeId = RouteId.Create(module, route);
            return routeHandlers.ContainsKey(routeId) || !SwaggerAnnotationsConfig.ShowOnlyAnnotatedRoutes;
        }

        private IEnumerable<SwaggerRouteData> ToSwaggerRouteData(INancyModule module)
        {
            Func<IEnumerable<RouteAttribute>, RouteId> getRouteId = (attrs) =>
            {
                return attrs.Select(attr => RouteId.Create(module, attr))
                            .FirstOrDefault(routeId => routeId.IsValid);
            };

            // Discover route handlers and put them in a Dictionary<RouteId, MethodInfo>
            var routeHandlers =
                module.GetType().GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
                    .Select(methodInfo => new
                    {
                        RouteId = getRouteId(methodInfo.GetCustomAttributes<RouteAttribute>()),
                        MethodInfo = methodInfo
                    })
                    .Where(x => x.RouteId.IsValid)
                    .ToDictionary(
                        x => x.RouteId,
                        x => x.MethodInfo
                    );

            return module.Routes.Where(route => ShowRoute(module, route, routeHandlers))
                .Select(route => CreateSwaggerRouteData(module, route, routeHandlers));
        }
    }
}