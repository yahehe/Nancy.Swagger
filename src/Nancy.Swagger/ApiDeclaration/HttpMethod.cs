using JetBrains.Annotations;

namespace Nancy.Swagger.ApiDeclaration
{
    using System.Runtime.Serialization;

    [PublicAPI]
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