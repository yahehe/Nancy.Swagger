using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Nancy.Routing;
using Nancy.Swagger.Annotations.Attributes;
using Nancy.Swagger.Services;
using Swagger.ObjectModel;

namespace Nancy.Swagger.Annotations
{
    public class SwaggerAnnotationsProvider : SwaggerMetadataProvider
    {
        private NancyContext _context;
        private INancyModuleCatalog _moduleCatalog;
        private ISwaggerModelCatalog _modelCatalog;

        public SwaggerAnnotationsProvider(INancyModuleCatalog moduleCatalog, NancyContext context, ISwaggerModelCatalog modelCatalog)
        {
            _moduleCatalog = moduleCatalog;
            _context = context;
            _modelCatalog = modelCatalog;
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
            return _modelCatalog.ToList();
        }

        protected override IList<Tag> RetrieveSwaggerTags()
        {
            //Tag list is empty for now
            return new List<Tag>();
        }

        private SwaggerModelData CreateSwaggerModelData(Type type)
        {
            // Only use properties which have a pulbic getter and setter
            var typeProperties = type.GetProperties()
                                        .Where(pi => pi.CanWrite && pi.GetSetMethod(true).IsPublic)
                                        .Where(pi => pi.CanRead && pi.GetGetMethod(true).IsPublic);

            var modelData = new SwaggerModelData(type)
            {
                Properties = typeProperties.Select(CreateSwaggerModelPropertyData).ToList()
            };

            var modelAttr = type.GetTypeInfo().GetCustomAttribute<ModelAttribute>();
            if (modelAttr != null)
            {
                modelData.Description = modelAttr.Description;
            }

            return modelData;
        }

        private SwaggerModelPropertyData CreateSwaggerModelPropertyData(PropertyInfo pi)
        {
            var modelProperty = new SwaggerModelPropertyData
            {
                Type = pi.PropertyType,
                Name = pi.Name
            };

            foreach (var attr in pi.GetCustomAttributes<ModelPropertyAttribute>())
            {
                modelProperty.Name = attr.Name ?? modelProperty.Name;
                modelProperty.Description = attr.Description ?? modelProperty.Description;
                modelProperty.Minimum = attr.GetNullableMinimum() ?? modelProperty.Minimum;
                modelProperty.Maximum = attr.GetNullableMaximum() ?? modelProperty.Maximum;
                modelProperty.Required = attr.GetNullableRequired() ?? modelProperty.Required;
                modelProperty.UniqueItems = attr.GetNullableUniqueItems() ?? modelProperty.UniqueItems;
                modelProperty.Enum = attr.Enum ?? modelProperty.Enum;
            }

            return modelProperty;
        }

        private Parameter CreateSwaggerParameterData(ParameterInfo pi)
        {
            var parameter = new Parameter
            {
                Name = pi.Name,
                //ParameterModel = pi.ParameterType
            };

            var paramAttrs = pi.GetCustomAttributes<RouteParamAttribute>();
            if (!paramAttrs.Any())
            {
                parameter.Description = "Warning: no annotation found for this parameter";
                parameter.In = ParameterIn.Query; // Required, so use query as fallback

                return parameter;
            }

            var bodyParam = new BodyParameter();

            foreach (var attr in paramAttrs)
            {
                parameter.Name = attr.Name ?? parameter.Name;
                parameter.In = attr.GetNullableParamType() ?? parameter.In;
                parameter.Required = attr.GetNullableRequired() ?? parameter.Required;
                parameter.Description = attr.Description ?? parameter.Description;

                bodyParam.Name = attr.Name ?? parameter.Name;
                bodyParam.In = attr.GetNullableParamType() ?? parameter.In;
                bodyParam.Required = attr.GetNullableRequired() ?? parameter.Required;
                bodyParam.Description = attr.Description ?? parameter.Description;
            }

            if (parameter.In == ParameterIn.Body)
            {
                bodyParam.AddBodySchema(pi.ParameterType, _modelCatalog);
                return bodyParam;
            }

            parameter.Type = Primitive.IsPrimitive(pi.ParameterType) ? Primitive.FromType(pi.ParameterType).Type : "string";

            return parameter;
        }

        private SwaggerRouteData CreateSwaggerRouteData(INancyModule module, Route route, Dictionary<RouteId, MethodInfo> routeHandlers)
        {
            var operation = new Operation()
            {
                OperationId = route.Description.Name
            };


            var data = new SwaggerRouteData(route.Description.Path, new PathItem());

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

            var routeId = RouteId.Create(module, route);
            var handler = routeHandlers.ContainsKey(routeId) ? routeHandlers[routeId] : null;
            if (handler == null)
            {
                operation.Description = "[example]"; // TODO: Insert example how to annotate a route
                operation.Summary = "Warning: no annotated method found for this route";

                return data;
            }

            Type model = null;
            foreach (var attr in handler.GetCustomAttributes<RouteAttribute>())
            {
                operation.Summary = attr.Summary ?? operation.Summary;
                operation.Description = attr.Notes ?? operation.Description;
                model = attr.Response ?? model;
                operation.Consumes = attr.Consumes ?? operation.Consumes;
                operation.Consumes = attr.Produces ?? operation.Produces;
            }

            if (model != null)
            {
                data.Types.Add(method, model);
            }

            operation.Responses = handler.GetCustomAttributes<SwaggerResponseAttribute>()
                .Select(attr =>
                {
                    var msg = new global::Swagger.ObjectModel.Response()
                    {
                        Description = attr.Message
                    };

                    if (attr.Model != null)
                    {
                        msg.Schema = _modelCatalog.GetModelForType(attr.Model)?.GetSchema();
                    }

                    return Tuple.Create((int)attr.Code, msg);
                })
                .ToDictionary(x => x.Item1.ToString(), x => x.Item2);


            operation.Parameters = handler.GetParameters()
                .Select(CreateSwaggerParameterData)
                .ToList();

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