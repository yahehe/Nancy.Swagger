using System;
using Nancy.Swagger.Attributes;
using Newtonsoft.Json;

namespace Nancy.Swagger.ResourceListing
{
    /// <summary>
    /// Provides details regarding the OAuth2's Authorization Endpoint.
    /// </summary>
    /// <example>
    /// <code>
    /// {
    ///   "url": "http://petstore.swagger.wordnik.com/oauth/requestToken",
    ///   "clientIdName": "client_id",
    ///   "clientSecretName": "client_secret"
    /// }
    /// </code>
    /// </example>
    [SwaggerData]
    public class TokenRequestEndpoint
    {
        /// <summary>
        /// The URL of the authorization endpoint for the authentication code grant flow.
        /// </summary>
        [Required]
        [JsonProperty("url")]
        public Uri Url { get; set; }

        /// <summary>
        /// An optional alternative name to standard "client_id" OAuth2 parameter.
        /// </summary>
        [JsonProperty("clientIdName")]
        public string ClientIdName { get; set; }

        /// <summary>
        /// An optional alternative name to standard "client_secret" OAuth2 parameter.
        /// </summary>
        [JsonProperty("clientSecretName")]
        public string ClientSecretName { get; set; }
    }
}