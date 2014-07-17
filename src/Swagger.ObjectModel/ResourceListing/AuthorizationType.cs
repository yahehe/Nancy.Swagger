using Swagger.ObjectModel.Attributes;

namespace Swagger.ObjectModel.ResourceListing
{
    [SwaggerData]
    public enum AuthorizationType
    {
        [SwaggerEnumValue("basicAuth")] BasicAuth,

        [SwaggerEnumValue("apiKey")] ApiKey,

        [SwaggerEnumValue("oauth2")] OAuth2
    }
}