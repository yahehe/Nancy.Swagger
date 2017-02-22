using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        private static IDictionary<string, SecuritySchemeBuilder> _securitySchemes = new Dictionary<string, SecuritySchemeBuilder>();

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

        public static void AddSecuritySchemeBuilder(SecuritySchemeBuilder builder, string name)
        {
            if (_securitySchemes == null)
            {
                _securitySchemes = new Dictionary<string, SecuritySchemeBuilder>();
            }

            if (_securitySchemes.ContainsKey(name))
            {
                _securitySchemes.Remove(name);
            }

            _securitySchemes.Add(name, builder);
        }

        public static void SetSecuritySchemeBuilder(SecuritySchemeBuilder builder, string name)
        {
            _securitySchemes = null;
            AddSecuritySchemeBuilder(builder, name);
        }

        public SwaggerRoot GetSwaggerJson(NancyContext context)
        {
            var builder = new SwaggerRootBuilder();

            foreach (var pathItem in this.RetrieveSwaggerPaths(context))
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

            foreach (var tag in RetrieveSwaggerTags())
            {
                builder.Tag(tag);
            }


            foreach (var securityScheme in _securitySchemes)
            {
                builder.SecurityDefinition(securityScheme.Key, securityScheme.Value.Build());
            }

            return builder.Build();
        }

        protected abstract IDictionary<string, SwaggerRouteData> RetrieveSwaggerPaths(NancyContext context);

        protected abstract IList<SwaggerModelData> RetrieveSwaggerModels();

        protected abstract IList<Tag> RetrieveSwaggerTags();

        private static Type GetType(Type type)
        {
            if (type.IsContainer())
            {
                return type.GetElementType() ?? type.GetTypeInfo().GetGenericArguments().First();
            }

            return type;
        }

        private SwaggerModelData EnsureModelData(Type type, IList<SwaggerModelData> modelData)
        {
            return modelData.FirstOrDefault(x => x.ModelType == type) ?? new SwaggerModelData(type);
        }
    }
}