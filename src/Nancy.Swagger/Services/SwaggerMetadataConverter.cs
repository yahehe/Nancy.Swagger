using System;
using System.Collections.Generic;
using System.Linq;
using Swagger.ObjectModel;

namespace Nancy.Swagger.Services
{
    [SwaggerApi]
    public abstract class SwaggerMetadataConverter : ISwaggerMetadataConverter
    {
        public SwaggerRoot GetSwaggerJson()
        {
            return new SwaggerRoot { Paths = RetrieveSwaggerRouteData() };
        }

        protected abstract IDictionary<string, PathItem> RetrieveSwaggerRouteData();


        private static Type GetType(Type type)
        {
            if (type.IsContainer())
            {
                return type.GetElementType() ?? type.GetGenericArguments().First();
            }

            return type;
        }

        private SwaggerModelData EnsureModelData(Type type, IList<SwaggerModelData> modelData)
        {
            return modelData.FirstOrDefault(x => x.ModelType == type) ?? new SwaggerModelData(type);
        }
    }
}