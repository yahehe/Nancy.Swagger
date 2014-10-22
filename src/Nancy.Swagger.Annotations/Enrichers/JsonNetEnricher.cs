using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Nancy.Swagger.Annotations.Enrichers
{
    public class JsonNetEnricher : ISwaggerDataEnricher
    {
        public void Enrich(SwaggerModelData data)
        {
            foreach (var attr in GetAttributes(data.ModelType, "Newtonsoft.Json.JsonObjectAttribute"))
            {
                var type = attr.GetType();
                data.Description = GetPropertyValue<string>(type, attr, "Description") ?? data.Description;
            }
        }

        public void Enrich(SwaggerRouteData data)
        {
        }

        public void Enrich(SwaggerModelPropertyData data, PropertyInfo propertyInfo)
        {
            foreach (var attr in GetAttributes(propertyInfo, "Newtonsoft.Json.JsonPropertyAttribute"))
            {
                var type = attr.GetType();

                data.Name = GetPropertyValue<string>(type, attr, "PropertyName") ?? data.Name;

                var required = GetPropertyValue<object>(type, attr, "Required");
                if (required != null && required.ToString() == "Always")
                {
                    data.Required = true;
                }
            }
        }

        public void Enrich(SwaggerParameterData parameterData, ParameterInfo parameterInfo)
        {
        }

        private static T GetPropertyValue<T>(Type type, object obj, string propertyName)
        {
            var value = type.GetProperty(propertyName).GetValue(obj, null);

            return value == null ? default(T) : (T)value;
        }

        private IList<Attribute> GetAttributes(MemberInfo memberInfo, string attrFullTypeName)
        {
            return memberInfo.GetCustomAttributes(true)
                             .Cast<Attribute>()
                             .Where(attr => attr.GetType().FullName == attrFullTypeName)
                             .ToList();
        }
    }
}