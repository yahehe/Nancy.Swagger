

namespace Swagger.ObjectModel
{
    using Swagger.ObjectModel.Attributes;

    /// <summary>
    /// An enum representing standard HTTP methods.
    /// </summary>
    [SwaggerData]
    public enum HttpMethod
    {
        /// <summary>
        /// The GET HTTP method.
        /// </summary>
        [SwaggerEnumValue("get")] Get,

        /// <summary>
        /// The POST HTTP method.
        /// </summary>
        [SwaggerEnumValue("post")] Post,

        /// <summary>
        /// The PUT HTTP method.
        /// </summary>
        [SwaggerEnumValue("put")] Put,

        /// <summary>
        /// The PATCH HTTP method.
        /// </summary>
        [SwaggerEnumValue("patch")] Patch,

        /// <summary>
        /// The DELETE HTTP method.
        /// </summary>
        [SwaggerEnumValue("delete")] Delete,

        /// <summary>
        /// The OPTIONS HTTP method.
        /// </summary>
        [SwaggerEnumValue("options")] Options,

        /// <summary>
        /// The HEAD HTTP method.
        /// </summary>
        [SwaggerEnumValue("head")]
        Head,
    }
}