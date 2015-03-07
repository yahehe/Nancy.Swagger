using System;


namespace Swagger.ObjectModel
{
    using Swagger.ObjectModel.Attributes;

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
    public class TokenRequestEndpoint : SwaggerModel
    {
        /// <summary>
        /// The URL of the authorization endpoint for the authentication code grant flow.
        /// </summary>
        [SwaggerProperty("url", true)]
        public Uri Url { get; set; }

        /// <summary>
        /// An optional alternative name to standard "client_id" OAuth2 parameter.
        /// </summary>
        [SwaggerProperty("clientIdName")]
        public string ClientIdName { get; set; }

        /// <summary>
        /// An optional alternative name to standard "client_secret" OAuth2 parameter.
        /// </summary>
        [SwaggerProperty("clientSecretName")]
        public string ClientSecretName { get; set; }
    }
}