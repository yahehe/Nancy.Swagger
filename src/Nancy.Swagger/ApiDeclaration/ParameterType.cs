using Nancy.Swagger.Attributes;

namespace Nancy.Swagger.ApiDeclaration
{
    using System.Runtime.Serialization;

    [SwaggerData]
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