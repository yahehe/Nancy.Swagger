using JetBrains.Annotations;

namespace Nancy.Swagger.ApiDeclaration
{
    using System.Runtime.Serialization;

    [PublicAPI]
    public enum ParameterType
    {
        [EnumMember(Value = "path")]
        Path,

        [EnumMember(Value = "query")]
        Query,

        [EnumMember(Value = "body")]
        Body,

        [EnumMember(Value = "header")]
        Header,

        [EnumMember(Value = "form")]
        Form
    }
}