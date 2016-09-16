using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Nancy.Swagger.Services;

namespace Nancy.Swagger.Annotations.Demo.JsonNet
{
    public class JsonNetAnnotationsProvider : ISwaggerMetadataProvider
    {
        private readonly SwaggerAnnotationsProvider _internalProvider;

        public JsonNetAnnotationsProvider(SwaggerAnnotationsProvider internalProvider)
        {
            _internalProvider = internalProvider;
        }

        public IList<SwaggerModelData> RetrieveSwaggerModelData()
        {
            var result = _internalProvider.RetrieveSwaggerModelData();
            result.ToList().ForEach(DecorateModel);
            return result;
        }

        public IList<SwaggerRouteData> RetrieveSwaggerRouteData()
        {
            return _internalProvider.RetrieveSwaggerRouteData();
        }

        private static void DecorateModel(SwaggerModelData model)
        {
            foreach (var attr in GetAttributes(model.ModelType, "Newtonsoft.Json.JsonObjectAttribute"))
            {
                var type = attr.GetType();
                model.Description = GetPropertyValue<string>(type, attr, "Description") ?? model.Description;
            }

            model.Properties
                .ToList()
                .ForEach(p => DecorateModelProperty(model, p));
        }

        private static void DecorateModelProperty(SwaggerModelData model, SwaggerModelPropertyData property)
        {
            var propertyInfo = GetPropertyInfo(model, property);
            foreach (var attr in GetAttributes(propertyInfo, "Newtonsoft.Json.JsonPropertyAttribute"))
            {
                var type = attr.GetType();

                property.Name = GetPropertyValue<string>(type, attr, "PropertyName") ?? property.Name;

                var required = GetPropertyValue<object>(type, attr, "Required");
                if (required != null && required.ToString() == "Always")
                {
                    property.Required = true;
                }
            }
        }

        private static PropertyInfo GetPropertyInfo(SwaggerModelData model, SwaggerModelPropertyData property)
        {
            return model.ModelType.GetProperty(property.Name);
        }

        private static T GetPropertyValue<T>(Type type, object obj, string propertyName)
        {
            var value = type.GetProperty(propertyName).GetValue(obj, null);

            return value == null ? default(T) : (T) value;
        }

        private static IList<Attribute> GetAttributes(MemberInfo memberInfo, string attrFullTypeName)
        {
            return memberInfo.GetCustomAttributes(true)
                .Cast<Attribute>()
                .Where(attr => attr.GetType().FullName == attrFullTypeName)
                .ToList();
        }
    }
}