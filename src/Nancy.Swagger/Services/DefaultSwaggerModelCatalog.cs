using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Swagger.ObjectModel;

namespace Nancy.Swagger.Services
{
    [SwaggerApi]
    public class DefaultSwaggerModelCatalog : List<SwaggerModelData>, ISwaggerModelCatalog
    {
        public DefaultSwaggerModelCatalog(IEnumerable<ISwaggerModelDataProvider> dataProviders)
        {
            AddRange(dataProviders.Select(p => p.GetModelData()));
        }

        public SwaggerModelData GetModelForType<T>()
        {
            Type t = typeof(T);
            SwaggerModelData model = this.FirstOrDefault(x => x.ModelType == t);
            return model;
        }
    }
}
