using JetBrains.Annotations;

namespace Nancy.Swagger.ResourceListing
{
    [PublicAPI]
    public enum AuthorizationType
    {
        BasicAuth,

        ApiKey,

        OAuth2
    }
}