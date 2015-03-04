

namespace Swagger.ObjectModel
{
    using global::Swagger.ObjectModel.Attributes;

    /// <summary>
    /// The Resource object describes a resource API endpoint in the application.
    /// </summary>
    /// <example>
    /// <code>
    /// {
    ///     "path": "/pets",
    ///     "description": "Operations about pets."
    /// }
    /// </code>
    /// </example>
    public class Resource : SwaggerModel
    {
        /// <summary>
        /// A relative path to the API declaration from the path used to retrieve this Resource Listing.
        /// This path does not necessarily have to correspond to the URL which actually serves this
        /// resource in the API but rather where the resource listing itself is served.
        /// </summary>
        /// <remarks>
        /// The value SHOULD be in a relative (URL) path format.
        /// </remarks>
        [SwaggerProperty("path", true)]
        public string Path { get; set; }

        /// <summary>
        /// A short description of the resource.
        /// </summary>
        [SwaggerProperty("description")]
        public string Description { get; set; }
    }
}