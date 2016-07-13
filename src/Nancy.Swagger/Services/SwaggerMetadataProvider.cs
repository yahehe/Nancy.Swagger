using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Swagger.ObjectModel;
using Swagger.ObjectModel.Builders;

namespace Nancy.Swagger.Services
{
    [SwaggerApi]
    public abstract class SwaggerMetadataProvider : ISwaggerMetadataProvider
    {
        private static Info _info = new Info()
        {
            Title = "No title set",
            Version = "0.1",
            Description = ""
        };

        private static SecuritySchemeBuilder _securitySchemeBuilder = null;
        private static string _securitySchemaType = string.Empty;

        public static void SetInfo(string title, string version, string desc, Contact contact = null, string termsOfService = null)
        {
            _info = new Info()
            {
                Title = title,
                Version = version,
                Description = desc,
                Contact = contact,
                TermsOfService = termsOfService
            };
        }

        public static void SetSecuritySchemeBuilder(SecuritySchemeBuilder builder, string type)
        {
            _securitySchemeBuilder = builder;
            _securitySchemaType = type;
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

            builder.Info(_info);
            
            foreach (var model in RetrieveSwaggerModels())
            {
                Type t = GetType(model.ModelType);
                String name = model.ModelType.Name;
                if (t != model.ModelType) name = t.Name + "[]";
                builder.Definition(name, model.GetSchema());
            }

            if (_securitySchemeBuilder != null)
            {
                builder.SecurityDefinition(_securitySchemaType, _securitySchemeBuilder.Build());
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