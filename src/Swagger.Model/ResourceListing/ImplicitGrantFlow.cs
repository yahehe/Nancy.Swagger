using Newtonsoft.Json;
using Swagger.Model.Attributes;

namespace Swagger.Model.ResourceListing
{
    /// <summary>
    /// Provides details regarding the OAuth2's Implicit Grant flow type.
    /// </summary>
    /// <example>
    /// <code>
    /// {
    ///   "loginEndpoint": {
    ///     "url": "http://petstore.swagger.wordnik.com/oauth/dialog"
    ///   },
    ///   "tokenName": "access_token"
    /// }
    /// </code>
    /// </example>
    [SwaggerData]
    public class ImplicitGrantFlow
    {
        /// <summary>
        /// The login endpoint definition.
        /// </summary>
        [Required]
        [JsonProperty("loginEndpoint")]
        public LoginEndpoint LoginEndpoint { get; set; }

        /// <summary>
        /// An optional alternative name to standard "access_token" OAuth2 parameter.
        /// </summary>
        [JsonProperty("tokenName")]
        public string TokenName { get; set; }
    }
}