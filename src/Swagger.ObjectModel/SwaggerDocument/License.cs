namespace Swagger.ObjectModel.SwaggerDocument
{
    using global::Swagger.ObjectModel.Attributes;

    /// <summary>
    /// The license.
    /// </summary>
    public class License : SwaggerModel
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [SwaggerProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the url.
        /// </summary>
        [SwaggerProperty("url")]
        public string Url { get; set; }
    }
}