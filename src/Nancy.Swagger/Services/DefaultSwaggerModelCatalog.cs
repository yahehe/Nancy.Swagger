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
            var t = typeof (T);
            var model = (SwaggerModelData) null;
            if (GetModelForType(t, false) == null)
            {
                model = new SwaggerModelData(t);
                Add(model);
            }
            return model;
        }

        public void AddModels(params Type[] types)
        {
            foreach (var t in types)
            {
                if (GetModelForType(t, false) == null)
                {
                    var model = new SwaggerModelData(t);
                    Add(model);
                }
            }
        }

        public SwaggerModelData GetModelForType<T>(bool addIfNotSet = true)
        {
            var t = typeof(T);
            return GetModelForType(t, addIfNotSet);
        }

        public SwaggerModelData GetModelForType(Type t, bool addIfNotSet = true)
        {
            if (t.GetTypeInfo().IsPrimitive || t == typeof(string)) return null;

            var model = this.FirstOrDefault(x => x.ModelType == t);
            if (model == null && addIfNotSet)
            {
                AddModels(t);
                model = this.FirstOrDefault(x => x.ModelType == t);
            }
            return model;
        }
            
    }
}
