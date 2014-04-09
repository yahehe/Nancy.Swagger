using System;
using Nancy.Swagger.Attributes;
using Newtonsoft.Json;

namespace Nancy.Swagger.ResourceListing
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
    [SwaggerDto]
    public class TokenEndpoint
    {
        /// <summary>
        /// The URL of the token endpoint for the authentication code grant flow.
        /// </summary>
        [Required]
        [JsonProperty("url")]
        public Uri Url { get; set; }

        /// <summary>
        /// An optional alternative name to standard "access_token" OAuth2 parameter.
        /// </summary>
        [JsonProperty("tokenName")]
        public string TokenName { get; set; }
    }
}