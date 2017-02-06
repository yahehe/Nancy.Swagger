using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        public SwaggerModelData AddModel<T>()
        {
            SwaggerModelData model = SwaggerModelData.ForType<T>(with => { });
            Add(model);
            return model;
        }

        public void AddModels(params Type[] types)
        {
            foreach (var t in types)
            {
                if (GetModelForType(t, false) == null)
                {
                    SwaggerModelData model = new SwaggerModelData(t);
                    Add(model);
                }
            }
        }

        public SwaggerModelData GetModelForType<T>()
        {
            Type t = typeof(T);
            return GetModelForType(t);
        }

        public SwaggerModelData GetModelForType(Type t, bool addIfNotSet = true)
        {
            if (t.GetTypeInfo().IsPrimitive || t == typeof(string)) return null;

            SwaggerModelData model = this.FirstOrDefault(x => x.ModelType == t);
            if (model == null && addIfNotSet)
            {
                AddModels(t);
                model = this.FirstOrDefault(x => x.ModelType == t);
            }
            return model;
        }
            
    }
}
