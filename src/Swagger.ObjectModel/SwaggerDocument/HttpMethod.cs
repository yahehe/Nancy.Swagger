

namespace Swagger.ObjectModel.SwaggerDocument
{
    using global::Swagger.ObjectModel.Attributes;

    /// <summary>
    /// An enum representing standard HTTP methods.
    /// </summary>
    [SwaggerData]
    public enum HttpMethod
    {
        /// <summary>
        /// The GET HTTP method.
        /// </summary>
        [SwaggerEnumValue("GET")] Get,

        /// <summary>
        /// The POST HTTP method.
        /// </summary>
        [SwaggerEnumValue("POST")] Post,

        /// <summary>
        /// The PUT HTTP method.
        /// </summary>
        [SwaggerEnumValue("PUT")] Put,

        /// <summary>
        /// The PATCH HTTP method.
        /// </summary>
        [SwaggerEnumValue("PATCH")] Patch,

        /// <summary>
        /// The DELETE HTTP method.
        /// </summary>
        [SwaggerEnumValue("DELETE")] Delete,

        /// <summary>
        /// The OPTIONS HTTP method.
        /// </summary>
        [SwaggerEnumValue("OPTIONS")] Options
    }
}