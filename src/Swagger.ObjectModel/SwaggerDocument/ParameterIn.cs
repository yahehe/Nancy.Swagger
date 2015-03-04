namespace Swagger.ObjectModel.SwaggerDocument
{
    using global::Swagger.ObjectModel.Attributes;

    /// <summary>
    /// An enum representing the type of an operation parameter.
    /// </summary>
    [SwaggerData]
    public enum ParameterIn
    {
        /// <summary>
        /// Denotes a path parameter.
        /// </summary>
        [SwaggerEnumValue("path")]
        Path,

        /// <summary>
        /// Denotes a query parameter.
        /// </summary>
        [SwaggerEnumValue("query")]
        Query,

        /// <summary>
        /// Denotes a body parameter.
        /// </summary>
        [SwaggerEnumValue("body")]
        Body,

        /// <summary>
        /// Denotes a header parameter.
        /// </summary>
        [SwaggerEnumValue("header")]
        Header,

        /// <summary>
        /// Denotes a form parameter.
        /// </summary>
        [SwaggerEnumValue("form")]
        Form
    }
}