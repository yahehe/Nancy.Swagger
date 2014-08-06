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

                return dataType;
            }

            if (Primitive.IsPrimitive(type))
            {
                var primitive = Primitive.FromType(type);

                dataType.Format = primitive.Format;
                dataType.Type = primitive.Type;

                return dataType;
            }

            if (type.IsContainer())
            {
                dataType.Type = "array";

                var itemsType = type.GetElementType() ?? type.GetGenericArguments().FirstOrDefault();
                if (Primitive.IsPrimitive(itemsType))
                {
                    var primitive = Primitive.FromType(itemsType);

                    dataType.Items = new Items
                    {
                        Type = primitive.Type,
                        Format = primitive.Format
                    };

                    return dataType;
                }

                dataType.Items = new Items { Ref = itemsType.DefaultModelId() };

                return dataType;
            }

            dataType.Ref = type.DefaultModelId();

            return dataType;
        }

        public static Parameter ToParameter(this SwaggerParameterData parameterData)
        {
            var parameter = parameterData.ParameterModel.ToDataType<Parameter>();

            parameter.Name = parameterData.Name;
            parameter.ParamType = parameterData.ParamType;
            parameter.Description = parameterData.Description;
            parameter.Required = parameterData.Required || parameterData.ParameterModel.IsImplicitlyRequired();
            parameter.AllowMultiple = parameterData.ParameterModel.IsContainer();
            parameter.DefaultValue = parameterData.DefaultValue;

            // Ensure when ParamType equals "body" name also equals "body" 
            // See https://github.com/wordnik/swagger-spec/blob/master/versions/1.2.md#524-parameter-object
            if (parameter.ParamType == ParameterType.Body)
            {
                parameter.Name = "body";
            }

            // "5.2.4 Parameter Object
            // ...type field MUST be used to link to other models."
            // See https://github.com/wordnik/swagger-spec/blob/master/versions/1.2.md#524-parameter-object
            if (parameterData.ParameterModel.IsContainer()) 
            {
                parameter.Type = parameter.Items.Type;
                parameter.Format = parameter.Items.Format;
                parameter.Items = null;
            }
            else 
            {
                parameter.Type = parameter.Type ?? parameter.Ref;
                parameter.Ref = null;
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

        internal static bool IsImplicitlyRequired(this Type type)
        {
            return type.IsValueType && !IsNullable(type);
        }

        private static bool IsNullable(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
    }
}