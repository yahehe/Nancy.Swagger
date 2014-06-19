using System;

using Nancy.Routing;
using Nancy.Swagger.ApiDeclaration;

namespace Nancy.Swagger
{
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
    }
}