using Swagger.ObjectModel.Attributes;

namespace Swagger.ObjectModel.ResourceListing
{
    /// <summary>
    /// An enum representing the type of the authorization scheme.
    /// </summary>
    [SwaggerData]
    public enum AuthorizationType
    {
        /// <summary>
        /// Basic HTTP authentication.
        /// </summary>
        [SwaggerEnumValue("basicAuth")] BasicAuth,

        /// <summary>
        /// API-key based authentication.
        /// </summary>
        [SwaggerEnumValue("apiKey")] ApiKey,

        /// <summary>
        /// OAuth2 based authentication.
        /// </summary>
        [SwaggerEnumValue("oauth2")] OAuth2
    }
}