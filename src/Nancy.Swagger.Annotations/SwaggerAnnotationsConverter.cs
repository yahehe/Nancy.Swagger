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
    public class SwaggerAnnotationsConverter : SwaggerMetadataConverter
    {
        private NancyContext _context;
        private INancyModuleCatalog _moduleCatalog;

        public SwaggerAnnotationsConverter(INancyModuleCatalog moduleCatalog, NancyContext context)
        {
            _moduleCatalog = moduleCatalog;
            _context = context;
        }

        protected override IDictionary<string, PathItem> RetrieveSwaggerRouteData()
        {
            var pathItems = new Dictionary<string, PathItem>();
            foreach (var pair in _moduleCatalog
                .GetAllModules(_context)
                .SelectMany(ToSwaggerRouteData))
            {
                PathItem entry;
                if (pathItems.TryGetValue(pair.Item1, out entry))
                {
                    pathItems[pair.Item1] = entry.Combine(pair.Item2);
                }
                else
                {
                    pathItems.Add(pair.Item1, pair.Item2);
                }
            }
            return pathItems;
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

            var modelAttr = type.GetCustomAttribute<SwaggerModelAttribute>();
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

            foreach (var attr in pi.GetCustomAttributes<SwaggerModelPropertyAttribute>())
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

            var paramAttrs = pi.GetCustomAttributes<SwaggerRouteParamAttribute>();
            if (!paramAttrs.Any())
            {
                parameter.Description = "Warning: no annotation found for this parameter";
                parameter.In = ParameterIn.Query; // Required, so use query as fallback

                return parameter;
            }

            foreach (var attr in paramAttrs)
            {
                parameter.Name = attr.Name ?? parameter.Name;
                parameter.In = attr.GetNullableParamType() ?? parameter.In;
                parameter.Required = attr.GetNullableRequired() ?? parameter.Required;
                parameter.Description = attr.Description ?? parameter.Description;
            }

            return parameter;
        }

        private Tuple<string, PathItem> CreateSwaggerRouteData(INancyModule module, Route route, Dictionary<RouteId, MethodInfo> routeHandlers)
        {
            var operation = new Operation()
                            {
                                OperationId = route.Description.Name
                            };


            var data = Tuple.Create(route.Description.Path, new PathItem());

            switch (route.Description.Method.ToLowerInvariant())
            {
                case "get":
                    data.Item2.Get = operation;
                    break;
                case "post":
                    data.Item2.Post = operation;
                    break;
                case "patch":
                    data.Item2.Patch = operation;
                    break;
                case "delete":
                    data.Item2.Delete = operation;
                    break;
                case "put":
                    data.Item2.Put = operation;
                    break;
                case "head":
                    data.Item2.Head = operation;
                    break;
                case "options":
                    data.Item2.Options = operation;
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

            foreach (var attr in handler.GetCustomAttributes<SwaggerRouteAttribute>())
            {
                operation.Summary = attr.Summary ?? operation.Summary;
                operation.Description = attr.Notes ?? operation.Description;
                // data.OperationModel = attr.Response ?? data.OperationModel;
                operation.Consumes = attr.Consumes ?? operation.Consumes;
                operation.Consumes = attr.Produces ?? operation.Produces;
            }

            operation.Responses = handler.GetCustomAttributes<SwaggerResponseAttribute>()
                .Select(attr =>
                {
                    var msg = new global::Swagger.ObjectModel.Response()
                    {
                        Description = attr.Message
                    };

                    //if (attr.Model != null)
                    //{
                    //    msg.ResponseModel = Primitive.IsPrimitive(attr.Model)
                    //                            ? Primitive.FromType(attr.Model).Type
                    //                            : SwaggerConfig.ModelIdConvention(attr.Model);
                    //}

                    return Tuple.Create((int)attr.Code, msg);
                })
                .ToDictionary(x => x.Item1.ToString(), x => x.Item2);


            operation.Parameters = handler.GetParameters()
                .Select(CreateSwaggerParameterData)
                .ToList();

            return data;
        }

        private IEnumerable<Tuple<string, PathItem>> ToSwaggerRouteData(INancyModule module)
        {
            Func<IEnumerable<SwaggerRouteAttribute>, RouteId> getRouteId = (attrs) =>
            {
                return attrs.Select(attr => RouteId.Create(module, attr))
                            .FirstOrDefault(routeId => routeId.IsValid);
            };

            // Discover route handlers and put them in a Dictionary<RouteId, MethodInfo>
            var routeHandlers =
                module.GetType().GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
                    .Select(methodInfo => new
                    {
                        RouteId = getRouteId(methodInfo.GetCustomAttributes<SwaggerRouteAttribute>()),
                        MethodInfo = methodInfo
                    })
                    .Where(x => x.RouteId.IsValid)
                    .ToDictionary(
                        x => x.RouteId,
                        x => x.MethodInfo
                    );

            return module.Routes
                .Select(route => CreateSwaggerRouteData(module, route, routeHandlers));
        }
    }
}