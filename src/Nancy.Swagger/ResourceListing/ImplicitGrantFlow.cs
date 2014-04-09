using Nancy.Swagger.Attributes;

namespace Nancy.Swagger.ResourceListing
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
    [SwaggerDto]
    public class ImplicitGrantFlow
    {
        /// <summary>
        /// The login endpoint definition.
        /// </summary>
        [Required]
        public LoginEndpoint LoginEndpoint { get; set; }

        /// <summary>
        /// An optional alternative name to standard "access_token" OAuth2 parameter.
        /// </summary>
        public string TokenName { get; set; }
    }
}