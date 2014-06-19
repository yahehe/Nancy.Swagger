using System.Runtime.Serialization;
using Swagger.Model.Attributes;

namespace Swagger.Model.ResourceListing
{
    /// <summary>
    /// Used in <see cref="Authorization"/> to denote how an API key should be passed.
    /// </summary>
    [SwaggerData]
    public enum PassType
    {
        [EnumMember(Value = "header")] Header,

        [EnumMember(Value = "query")] Query
    }
}