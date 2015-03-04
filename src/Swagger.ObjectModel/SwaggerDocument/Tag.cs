namespace Swagger.ObjectModel.SwaggerDocument
{
    using global::Swagger.ObjectModel.Attributes;

    /// <summary>
    /// The tag.
    /// </summary>
    public class Tag : SwaggerModel
    {
        /// <summary>
        /// The name of the tag
        /// </summary>
        [SwaggerProperty("name", true)]
        public string Name { get; set; }

        /// <summary>
        /// A short description of the tag
        /// </summary>
        [SwaggerProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Additional external documentation for this tag
        /// </summary>
        [SwaggerProperty("externalDocs")]
        public ExternalDocumentation ExternalDocumentation { get; set; }
    }
}