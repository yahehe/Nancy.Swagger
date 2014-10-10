using Swagger.ObjectModel.Attributes;

namespace Swagger.ObjectModel.ResourceListing
{
    /// <summary>
    /// Provides details regarding the OAuth2's Authorization Code Grant flow type.
    /// </summary>
    /// <example>
    /// <code>
    /// {
    ///   "tokenRequestEndpoint": {
    ///     "url": "http://petstore.swagger.wordnik.com/oauth/requestToken",
    ///     "clientIdName": "client_id",
    ///     "clientSecretName": "client_secret"
    ///   },
    ///   "tokenEndpoint": {
    ///     "url": "http://petstore.swagger.wordnik.com/oauth/token",
    ///     "tokenName": "access_code"
    ///   }
    /// }
    /// </code>
    /// </example>
    public class AuthorizationCodeGrantFlow : SwaggerModel
    {
        /// <summary>
        /// The token request endpoint definition.
        /// </summary>
        [SwaggerProperty("tokenRequestEndpoint", true)]
        public TokenRequestEndpoint TokenRequestEndpoint { get; set; }

        /// <summary>
        /// The token endpoint definition.
        /// </summary>
        [SwaggerProperty("tokenEndpoint", true)]
        public TokenEndpoint TokenEndpoint { get; set; }
    }
}