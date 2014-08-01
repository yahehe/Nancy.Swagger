using System;
using System.Linq;
using Nancy.Routing;
using System.Collections;
using Swagger.ObjectModel;
using Swagger.ObjectModel.ApiDeclaration;

namespace Nancy.Swagger
{
    [SwaggerApi]
    public static class SwaggerExtensions
    {
        /// <summary>
        /// Returns an instance of <see cref="SwaggerRouteData"/> representing this route.
        /// </summary>
        /// <param name="desc">The <see cref="RouteDescription"/>.</param>
        /// <param name="action">An <see cref="Action{SwaggerRouteDataBuilder}"/> for building the <see cref="SwaggerRouteData"/>.</param>
        /// <returns>An instance of <see cref="SwaggerRouteData"/> constructed using <paramref name="desc"/> and by invoking <paramref name="action"/>.</returns>
        public static SwaggerRouteData AsSwagger(this RouteDescription desc, Action<SwaggerRouteDataBuilder> action)
        {
            var builder = new SwaggerRouteDataBuilder(desc.Name, Convert(desc.Method), desc.Path);

            action.Invoke(builder);

            return builder.Data;
        }

        public static Operation ToOperation(this SwaggerRouteData routeData)
        {
            var operation = routeData.OperationModel.ToDataType<Operation>();
            
            operation.Nickname = routeData.OperationNickname;
            operation.Summary = routeData.OperationSummary;
            operation.Method = routeData.OperationMethod;
            operation.Notes = routeData.OperationNotes;
            operation.Parameters = routeData.OperationParameters.Select(p => p.ToParameter());
            operation.ResponseMessages = routeData.OperationResponseMessages;
            operation.Produces = routeData.OperationProduces;
            operation.Consumes = routeData.OperationConsumes;

            return operation;
        }

        public static T ToDataType<T>(this Type type)
            where T : DataType, new()
        {
            var dataType = new T();

            if (type == null) 
            {
                dataType.Type = "void";
            }
            else if (Primitive.IsPrimitive(type))
            {
                var primitive = Primitive.FromType(type);
                dataType.Format = primitive.Format;
                dataType.Type = primitive.Type;
            }
            else if (type.IsContainer())
            {
                dataType.Type = "array";

                var itemsType = type.GetElementType() ?? type.GetGenericArguments().FirstOrDefault();
                if (Primitive.IsPrimitive(itemsType))
                {
                    var itemsPrimitive = Primitive.FromType(itemsType);
                    dataType.Items = new Items
                    {
                        Type = itemsPrimitive.Type,
                        Format = itemsPrimitive.Format
                    };
                }
                else
                {
                    dataType.Items = new Items { Ref = itemsType.DefaultModelId() };
                }
            }
            else
            {
                dataType.Ref = type.DefaultModelId();
            }

            return dataType;
        }

        public static Parameter ToParameter(this SwaggerParameterData parameterData)
        {
            var parameter = new Parameter
            {
                Name = parameterData.Name,
                ParamType = parameterData.ParamType,
                Description = parameterData.Description,
                Required = parameterData.Required,
                AllowMultiple = parameterData.AllowMultiple,
                DefaultValue = parameterData.DefaultValue
            };

            if (Primitive.IsPrimitive(parameterData.ParameterModel))
            {
                var primitive = Primitive.FromType(parameterData.ParameterModel);

                parameter.Type = primitive.Type;
                parameter.Format = primitive.Format;
            }
            else
            {
                parameter.Type = parameterData.ParameterModel.DefaultModelId();
            }

            return parameter;
        }

        public static string DefaultModelId(this Type type)
        {
            // TODO: This won't scale as you'd get collisions between types with the same 
            // name but different namespace. Could use FullName, but that's a bit fugly. 
            // perhaps this is a reasonable default but the DSL could provide a facility for 
            // overriding a given model's ID?
            return type.Name;
        }

        private static HttpMethod Convert(string method)
        {
            switch (method)
            {
                case "DELETE":
                    return HttpMethod.Delete;
                case "GET":
                    return HttpMethod.Get;
                case "OPTIONS":
                    return HttpMethod.Options;
                case "PATCH":
                    return HttpMethod.Patch;
                case "POST":
                    return HttpMethod.Post;
                case "PUT":
                    return HttpMethod.Put;
                default:
                    throw new NotSupportedException(string.Format("HTTP method '{0}' is not supported.", method));
            }
        }

        public static bool IsContainer(this Type type)
        {
            return typeof(IEnumerable).IsAssignableFrom(type)
                && !typeof(String).IsAssignableFrom(type);
        }
    }
}