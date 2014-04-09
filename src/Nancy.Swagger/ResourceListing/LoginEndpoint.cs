using System;
using Nancy.Swagger.Attributes;

namespace Nancy.Swagger.ResourceListing
{
    /// <summary>
    /// Provides details regarding the Implicit Grant's authorization endpoint.
    /// </summary>
    /// <example>
    /// <code>
    /// {
    ///   "url": "http://petstore.swagger.wordnik.com/oauth/dialog"
    /// }
    /// </code>
    /// </example>
    [SwaggerDto]
    public class LoginEndpoint
    {
        /// <summary>
        /// The URL of the authorization endpoint for the implicit grant flow.
        /// </summary>
        [Required]
        public Uri Url { get; set; }
    }
}