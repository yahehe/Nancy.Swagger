using Swagger.Model.ApiDeclaration;
using System;
using System.Reflection;

namespace Nancy.Swagger.Annotations
{
    internal static class Extensions
    {
        public static string EnsureForwardSlash(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return "/";
            }

            return value.StartsWith("/") ? value : "/" + value;
        }

        public static T GetAttribute<T>(this MemberInfo member)
            where T : Attribute
        {
            return Attribute.GetCustomAttribute(member, typeof(T)) as T;
        }

        public static HttpMethod ToHttpMethod(this string method)
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
    }
}