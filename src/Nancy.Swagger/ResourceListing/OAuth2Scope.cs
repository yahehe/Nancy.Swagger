using Nancy.Swagger.Attributes;
using Newtonsoft.Json;

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
    [SwaggerData]
    public class OAuth2Scope
    {
        /// <summary>
        /// The name of the scope.
        /// </summary>
        [Required]
        [JsonProperty("scope")]
        public string Scope { get; set; }

        /// <summary>
        /// A short description of the scope.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}