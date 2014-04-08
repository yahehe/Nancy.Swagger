using JetBrains.Annotations;
using Nancy.Swagger.Attributes;

namespace Nancy.Swagger.ResourceListing
{
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
    [PublicAPI]
    public class Resource
    {
        /// <summary>
        /// A relative path to the API declaration from the path used to retrieve this Resource Listing.
        /// This path does not necessarily have to correspond to the URL which actually serves this 
        /// resource in the API but rather where the resource listing itself is served.
        /// </summary>
        /// <remarks>
        /// The value SHOULD be in a relative (URL) path format.
        /// </remarks>
        [Required]
        public string Path { get; set; }

        /// <summary>
        /// A short description of the resource.
        /// </summary>
        public string Description { get; set; }
    }
}