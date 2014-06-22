using Swagger.Model.Attributes;

namespace Swagger.Model.ResourceListing
{
    [SwaggerData]
    public enum AuthorizationType
    {
        [SwaggerEnumValue("basicAuth")] BasicAuth,

        [SwaggerEnumValue("apiKey")] ApiKey,

        [SwaggerEnumValue("oauth2")] OAuth2
    }
}