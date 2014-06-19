namespace Nancy.Swagger
{
    public static class SwaggerConfig
    {
        static SwaggerConfig()
        {
            ResourceListingPath = "api-docs";
        }

        /// <summary>
        /// Gets or sets the path at which the Swagger resource listing will be served. The default is <c>api-docs</c>.
        /// </summary>
        public static string ResourceListingPath { get; set; }
    }
}
