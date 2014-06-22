using System;
using Swagger.Model.Attributes;

namespace Swagger.Model.ResourceListing
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
    public class LoginEndpoint : SwaggerModel
    {
        /// <summary>
        /// The URL of the authorization endpoint for the implicit grant flow.
        /// </summary>
        [SwaggerProperty("url", true)]
        public Uri Url { get; set; }
    }
}