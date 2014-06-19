using System.Runtime.Serialization;
using Swagger.Model.Attributes;

namespace Swagger.Model.ApiDeclaration
{
    [SwaggerData]
    public enum HttpMethod
    {
        [EnumMember(Value = "GET")] Get,

        [EnumMember(Value = "POST")] Post,

        [EnumMember(Value = "PUT")] Put,

        [EnumMember(Value = "PATCH")] Patch,

        [EnumMember(Value = "DELETE")] Delete,

        [EnumMember(Value = "OPTIONS")] Options
    }
}