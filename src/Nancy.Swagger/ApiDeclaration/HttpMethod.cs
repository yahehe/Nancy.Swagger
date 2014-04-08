using JetBrains.Annotations;

namespace Nancy.Swagger.ApiDeclaration
{
    [PublicAPI]
    public enum HttpMethod
    {
        Get,

        Post,

        Put,

        Patch,

        Delete,

        Options
    }
}