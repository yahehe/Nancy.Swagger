using Nancy.Swagger.Attributes;

namespace Nancy.Swagger.ResourceListing
{
    /// <summary>
    /// Describes an OAuth2 authorization scope.
    /// </summary>
    /// <example>
    /// <code>
    /// {
    ///   "scope": "email",
    ///   "description": "Access to your email address"
    /// }
    /// </code>
    /// </example>
    [SwaggerDto]
    public class OAuth2Scope
    {
        /// <summary>
        /// The name of the scope.
        /// </summary>
        [Required]
        public string Scope { get; set; }

        /// <summary>
        /// A short description of the scope.
        /// </summary>
        public string Description { get; set; }
    }
}