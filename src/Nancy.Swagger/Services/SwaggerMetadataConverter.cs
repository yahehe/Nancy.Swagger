using System;
using System.Collections.Generic;
using System.Linq;
using Swagger.ObjectModel;
using Swagger.ObjectModel.Builders;

namespace Nancy.Swagger.Services
{
    [SwaggerApi]
    public abstract class SwaggerMetadataConverter : ISwaggerMetadataConverter
    {
        public SwaggerRoot GetSwaggerJson()
        {
            var builder = new SwaggerRootBuilder();
            foreach (var kvp in RetrieveSwaggerRouteData())
            {
                builder.Path(kvp.Key, kvp.Value);
            }
            builder.Info(new Info()
                         {
                             Title = "No title set",
                             Version = "0.1"
                         });
            return builder.Build();
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