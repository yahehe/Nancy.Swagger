namespace Swagger.ObjectModel.SwaggerDocument
{
    using global::Swagger.ObjectModel.Attributes;

    

    /// <summary>
    /// The external documentation.
    /// </summary>
    public class ExternalDocumentation
    {
        /// <summary>
        /// A short description of the target documentation. GFM syntax can be used for rich text representation.
        /// </summary>
        [SwaggerProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// The URL for the target documentation. Value MUST be in the format of a URL.
        /// </summary>
        [SwaggerProperty("url", true)]
        public string Url { get; set; }
    }
}