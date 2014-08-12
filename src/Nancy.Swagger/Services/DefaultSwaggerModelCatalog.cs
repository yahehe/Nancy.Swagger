using System.Collections.Generic;
using System.Linq;

namespace Nancy.Swagger.Services
{
    [SwaggerApi]
    public class DefaultSwaggerModelCatalog : List<SwaggerModelData>, ISwaggerModelCatalog
    {
        public DefaultSwaggerModelCatalog(IEnumerable<ISwaggerModelDataProvider> dataProviders)
        {
            AddRange(dataProviders.Select(p => p.GetModelData()));
        }
    }
}
