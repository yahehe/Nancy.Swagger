using JetBrains.Annotations;

namespace Swagger.Model.ResourceListing
{
    [PublicAPI]
    public enum AuthorizationType
    {
        BasicAuth,

        ApiKey,

        OAuth2
    }
}