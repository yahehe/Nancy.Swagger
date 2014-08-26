using System.Collections.Generic;

namespace Nancy.Swagger.Annotations.Enrichers
{
    public interface ISwaggerDataEnricherCatalog : IEnumerable<ISwaggerDataEnricher>
    {
    }
}