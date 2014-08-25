using System;
namespace Nancy.Swagger
{
    [SwaggerApi]
    public static class SwaggerConfig
    {
        /// <summary>
        /// The default resource listing path, <c>api-docs</c>.
        /// </summary>
        public const string DefaultResourceListingPath = "api-docs";

        static SwaggerConfig()
        {
            ResourceListingPath = DefaultResourceListingPath;
            NicknameConvention = DefaultNicknameConvention;
        }

        /// <summary>
        /// Get or sets a function which returns a unique id for the given 
        /// <see cref="SwaggerRouteData"/> that can be used by tools reading
        /// the output for further and easier manipulation.
        /// </summary>
        public static Func<SwaggerRouteData, string> NicknameConvention { get; set; }

        /// <summary>
        /// Gets or sets the path at which the Swagger resource listing will be served. 
        /// Default value is <see cref="DefaultResourceListingPath"/>.
        /// </summary>
        public static string ResourceListingPath { get; set; }

        /// <summary>
        /// Returns a unique id for the given <paramref name="SwaggerRouteData"/>
        /// that can be used by tools reading the output for further and easier
        /// manipulation.
        /// </summary>
        /// <param name="route">The route for which a nickname is returned.</param>
        /// <returns>a unique id for the given <paramref name="Route"/> that can
        /// be used by tools reading the output for further and easier 
        /// manipulation.</returns>        
        public static string DefaultNicknameConvention(SwaggerRouteData route)
        {
            return string.IsNullOrEmpty(route.OperationNickname)
                    ? string.Format("{0}/{1}", route.OperationMethod.ToString().ToLower(), route.ApiPath).ToCamelCase()
                    : route.OperationNickname;
        }
    }
}
