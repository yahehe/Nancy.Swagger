using System.Runtime.Serialization;
using Swagger.Model.Attributes;

namespace Swagger.Model.ApiDeclaration
{
    [SwaggerData]
    public enum ParameterType
    {
        [EnumMember(Value = "path")] Path,

        [EnumMember(Value = "query")] Query,

        [EnumMember(Value = "body")] Body,

        [EnumMember(Value = "header")] Header,

        [EnumMember(Value = "form")] Form
    }
}