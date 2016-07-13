using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Swagger.ObjectModel;
using Swagger.ObjectModel.Builders;

namespace Nancy.Swagger.Services
{
    [SwaggerApi]
    public abstract class SwaggerMetadataProvider : ISwaggerMetadataProvider
    {
        private static Info info = new Info()
        {
            Title = "No title set",
            Version = "0.1",
            Description = ""
        };

        public static void SetInfo(string title, string version, string desc)
        {
            info = new Info()
            {
                Title = title,
                Version = version,
                Description = desc
            };
        }

        public SwaggerRoot GetSwaggerJson()
        {
            var builder = new SwaggerRootBuilder();

            foreach (var pathItem in this.RetrieveSwaggerPaths())
            {
                builder.Path(pathItem.Key, pathItem.Value.PathItem);
            }

            //foreach (var model in this.RetrieveSwaggerModels())
            //{
            //    builder.Definition(model.ModelType.Name, model.);
            //}

            builder.Info(info);
            
            foreach (var model in RetrieveSwaggerModels())
            {
                Type t = GetType(model.ModelType);
                String name = model.ModelType.Name;
                if (t != model.ModelType) name = t.Name + "[]";
                builder.Definition(name, model.GetSchema());
            }

            return builder.Build();
        }

        protected abstract IDictionary<string, SwaggerRouteData> RetrieveSwaggerPaths();

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