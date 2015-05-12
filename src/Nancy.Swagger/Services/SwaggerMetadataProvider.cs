using System;
using System.Collections.Generic;
using System.Linq;
using Swagger.ObjectModel;
using Swagger.ObjectModel.Builders;

namespace Nancy.Swagger.Services
{
    [SwaggerApi]
    public abstract class SwaggerMetadataProvider : ISwaggerMetadataProvider
    {
        public SwaggerRoot GetSwaggerJson()
        {
            var builder = new SwaggerRootBuilder();

            foreach (var pathItem in this.RetrieveSwaggerPaths())
            {
                builder.Path(pathItem.Key, pathItem.Value);
            }

            foreach (var model in this.RetrieveSwaggerModels())
            {
                //builder.Definition(model.ModelType.Name, model.);
            }

            builder.Info(new Info()
                         {
                             Title = "No title set",
                             Version = "0.1"
                         });

            return builder.Build();
        }

        protected abstract IDictionary<string, PathItem> RetrieveSwaggerPaths();

        protected abstract IList<SwaggerModelData> RetrieveSwaggerModels();

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