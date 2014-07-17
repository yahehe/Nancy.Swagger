using System;
using Swagger.ObjectModel.Attributes;

namespace Swagger.ObjectModel.ResourceListing
{
    /// <summary>
    /// Provides details regarding the OAuth2's Token Endpoint.
    /// </summary>
    /// <example>
    /// <code>
    /// {
    ///   "url": "http://petstore.swagger.wordnik.com/oauth/token",
    ///   "tokenName": "access_code"
    /// }
    /// </code>
    /// </example>
    public class TokenEndpoint : SwaggerModel
    {
        /// <summary>
        /// The URL of the token endpoint for the authentication code grant flow.
        /// </summary>
        [SwaggerProperty("url", true)]
        public Uri Url { get; set; }

        /// <summary>
        /// An optional alternative name to standard "access_token" OAuth2 parameter.
        /// </summary>
        [SwaggerProperty("tokenName")]
        public string TokenName { get; set; }
    }
}