using System.Collections.Generic;
using System.Linq;

namespace Nancy.Swagger.Services
{
    public class DefaultSwaggerModelCatalog : List<SwaggerModelData>, ISwaggerModelCatalog
    {
        public DefaultSwaggerModelCatalog(IEnumerable<ISwaggerModelDataProvider> dataProviders)
        {
            this.AddRange(dataProviders.Select(p => p.GetModelData()));
        }
    }
}
