using Swagger.Model.Attributes;

namespace Swagger.Model.ApiDeclaration
{
    [SwaggerData]
    public enum ParameterType
    {
        [SwaggerEnumValue("path")] Path,

        [SwaggerEnumValue("query")] Query,

        [SwaggerEnumValue("body")] Body,

        [SwaggerEnumValue("header")] Header,

        [SwaggerEnumValue("form")] Form
    }
}