using Nancy.Swagger.Attributes;

namespace Nancy.Swagger.ResourceListing
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
    [SwaggerDto]
    public class AuthorizationCodeGrantFlow
    {
        /// <summary>
        /// The token request endpoint definition.
        /// </summary>
        [Required]
        public TokenRequestEndpoint TokenRequestEndpoint { get; set; }

        /// <summary>
        /// The token endpoint definition.
        /// </summary>
        [Required]
        public TokenEndpoint TokenEndpoint { get; set; }
    }
}