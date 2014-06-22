using Swagger.Model.Attributes;

namespace Swagger.Model.ApiDeclaration
{
    [SwaggerData]
    public enum HttpMethod
    {
        [SwaggerEnumValue("GET")] Get,

        [SwaggerEnumValue("POST")] Post,

        [SwaggerEnumValue("PUT")] Put,

        [SwaggerEnumValue("PATCH")] Patch,

        [SwaggerEnumValue("DELETE")] Delete,

        [SwaggerEnumValue("OPTIONS")] Options
    }
}