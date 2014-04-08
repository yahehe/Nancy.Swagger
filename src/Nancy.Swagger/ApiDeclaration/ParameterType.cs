using JetBrains.Annotations;

namespace Nancy.Swagger.ApiDeclaration
{
    [PublicAPI]
    public enum ParameterType
    {
        Path,

        Query,

        Body,

        Header,

        Form
    }
}