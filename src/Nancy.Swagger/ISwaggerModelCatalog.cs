using System;
using System.Collections.Generic;
using Swagger.ObjectModel;

namespace Nancy.Swagger
{
    [SwaggerApi]
    public interface ISwaggerModelCatalog : IEnumerable<SwaggerModelData>
    {
        SwaggerModelData GetModelForType<T>();
    }
}