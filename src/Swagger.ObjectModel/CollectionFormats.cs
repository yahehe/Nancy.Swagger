namespace Swagger.ObjectModel
{
    using Swagger.ObjectModel.Attributes;

    /// <summary>
    /// The collection formats.
    /// </summary>
    public enum CollectionFormats
    {
        /// <summary>
        /// Comma separated values foo,bar
        /// </summary>
        [SwaggerEnumValue("csv")]
        Csv,

        /// <summary>
        /// Space separated values foo bar
        /// </summary>
        [SwaggerEnumValue("ssv")]
        Ssv,

        /// <summary>
        /// Tab separated values foo\tbar
        /// </summary>
        [SwaggerEnumValue("tsv")]
        Tsv,

        /// <summary>
        /// Pipe separated values foo|bar
        /// </summary>
        [SwaggerEnumValue("pipes")]
        Pipes,

        /// <summary>
        /// corresponds to multiple parameter instances instead of multiple values for a single instance <code>foo=bar&foo=baz</code>
        /// </summary>
        /// <remarks>
        /// This is valid only for parameters in "query" or "formData".
        /// </remarks>
        [SwaggerEnumValue("multi")]
        Multi
    }
}