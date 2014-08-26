using System.Collections.Generic;

namespace Nancy.Swagger.Annotations.Enrichers
{
    public class DefaultSwaggerDataEnricherCatalog : List<ISwaggerDataEnricher>, ISwaggerDataEnricherCatalog
    {
        public DefaultSwaggerDataEnricherCatalog()
        {
        }

        public DefaultSwaggerDataEnricherCatalog(IEnumerable<ISwaggerDataEnricher> enrichers)
            : base(enrichers)
        {
        }
    }
}