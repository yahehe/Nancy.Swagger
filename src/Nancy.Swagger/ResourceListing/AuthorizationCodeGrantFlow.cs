using Nancy.Swagger.Attributes;
using Newtonsoft.Json;

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
    [SwaggerData]
    public class AuthorizationCodeGrantFlow
    {
        /// <summary>
        /// The token request endpoint definition.
        /// </summary>
        [Required]
        [JsonProperty("tokenRequestEndpoint")]
        public TokenRequestEndpoint TokenRequestEndpoint { get; set; }

        /// <summary>
        /// The token endpoint definition.
        /// </summary>
        [Required]
        [JsonProperty("tokenEndpoint")]
        public TokenEndpoint TokenEndpoint { get; set; }
    }
}