using Swagger.Model.Attributes;

namespace Swagger.Model.ResourceListing
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
    public class OAuth2Scope : SwaggerModel
    {
        /// <summary>
        /// The name of the scope.
        /// </summary>
        [SwaggerProperty("scope", true)]
        public string Scope { get; set; }

        /// <summary>
        /// A short description of the scope.
        /// </summary>
        [SwaggerProperty("description")]
        public string Description { get; set; }
    }
}