using JetBrains.Annotations;

namespace Nancy.Swagger.ApiDeclaration
{
    [PublicAPI]
    public enum ParameterType
    {
        path,

        query,

        body,

        header,

        form
    }
}