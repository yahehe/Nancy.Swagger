using System.Collections.Generic;

namespace Nancy.Swagger
{
    [SwaggerApi]
    public interface ISwaggerModelCatalog : IEnumerable<SwaggerModelData>
    {
    }
}