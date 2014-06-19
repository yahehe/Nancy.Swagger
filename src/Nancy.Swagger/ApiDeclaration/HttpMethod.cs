using Nancy.Swagger.Attributes;

namespace Nancy.Swagger.ApiDeclaration
{
    using System.Runtime.Serialization;

    [SwaggerData]
    public enum HttpMethod
    {
        [EnumMember(Value = "GET")]
        Get,

        [EnumMember(Value = "POST")]
        Post,

        [EnumMember(Value = "PUT")]
        Put,

        [EnumMember(Value = "PATCH")]
        Patch,

        [EnumMember(Value = "DELETE")]
        Delete,

        [EnumMember(Value = "OPTIONS")]
        Options
    }
}