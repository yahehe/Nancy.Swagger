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
        }

        /// <summary>
        /// Gets or sets the path at which the Swagger resource listing will be served. 
        /// Default value is <see cref="DefaultResourceListingPath"/>.
        /// </summary>
        public static string ResourceListingPath { get; set; }
    }
}
