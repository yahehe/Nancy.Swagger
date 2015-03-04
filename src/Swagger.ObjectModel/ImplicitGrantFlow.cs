

namespace Swagger.ObjectModel
{
    using global::Swagger.ObjectModel.Attributes;

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
    public class ImplicitGrantFlow : SwaggerModel
    {
        /// <summary>
        /// The login endpoint definition.
        /// </summary>
        [SwaggerProperty("loginEndpoint", true)]
        public LoginEndpoint LoginEndpoint { get; set; }

        /// <summary>
        /// An optional alternative name to standard "access_token" OAuth2 parameter.
        /// </summary>
        [SwaggerProperty("tokenName")]
        public string TokenName { get; set; }
    }
}