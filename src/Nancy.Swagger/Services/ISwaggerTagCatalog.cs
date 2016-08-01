using System;
using System.Collections.Generic;
using Swagger.ObjectModel;

namespace Nancy.Swagger.Services
{
    [SwaggerApi]
    public interface ISwaggerTagCatalog : IEnumerable<Tag>
    {
        void AddTag(Tag tag);
    }
}