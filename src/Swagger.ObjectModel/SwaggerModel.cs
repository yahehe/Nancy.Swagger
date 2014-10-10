using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using Swagger.ObjectModel.Attributes;
using Swagger.ObjectModel.Reflection;

namespace Swagger.ObjectModel
{
    /// <summary>
    /// The base class for all Swagger models with logic
    /// to serialize it according to the Swagger schema.
    /// </summary>
    [SwaggerData]
    public class SwaggerModel
    {
        private static readonly IJsonSerializerStrategy SerializerStrategy = new SwaggerSerializerStrategy();

        /// <summary>
        /// Returns a valid JSON representation of
        /// the model, according to the Swagger schema.
        /// </summary>
        /// <returns>A valid JSON representation of the model.</returns>
        public string ToJson()
        {
            return SimpleJson.SerializeObject(this, SerializerStrategy);
        }

        private class SwaggerSerializerStrategy : PocoJsonSerializerStrategy
        {
            // Enums should use value from SwaggerEnumValueAttribute if it exists.
            protected override object SerializeEnum(Enum @enum)
            {
                var memberInfo = @enum.GetType().GetMember(@enum.ToString()).Single();

                var attribute = (SwaggerEnumValueAttribute) ReflectionUtils.GetAttribute(memberInfo, typeof(SwaggerEnumValueAttribute));
                if (attribute != null && !string.IsNullOrEmpty(attribute.Value))
                {
                    return attribute.Value;
                }

                return @enum.ToString();
            }

            // Strip null values from serialized JSON.
            protected override bool TrySerializeUnknownTypes(object input, out object output)
            {
                var result = new JsonObject();

                foreach (var getter in GetCache[input.GetType()].Where(x => x.Value != null))
                {
                    var value = getter.Value(input);
                    if (value == null)
                    {
                        continue;
                    }

                    var dictionary = value as IDictionary;
                    if (dictionary != null)
                    {
                        value = ToObject(dictionary);
                    }

                    result.Add(MapClrMemberNameToJsonFieldName(getter.Key), value);
                }

                output = result;
                return true;
            }

            // Serialized properties should use name from SwaggerPropertyAttribute if it exists.
            internal override IDictionary<string, ReflectionUtils.GetDelegate> GetterValueFactory(Type type)
            {
                var hasAttribute = ReflectionUtils.GetAttribute(type, typeof (SwaggerDataAttribute)) != null;
                if (!hasAttribute)
                {
                    return base.GetterValueFactory(type);
                }

                return ReflectionUtils.GetProperties(type)
                    .Where(x => x.CanRead)
                    .Where(x => !ReflectionUtils.GetGetterMethodInfo(x).IsStatic)
                    .ToDictionary(GetMemberName, ReflectionUtils.GetGetMethod);
            }

            // Serialized properties should use name from SwaggerPropertyAttribute if it exists.
            internal override IDictionary<string, KeyValuePair<Type, ReflectionUtils.SetDelegate>> SetterValueFactory(Type type)
            {
                var hasAttribute = ReflectionUtils.GetAttribute(type, typeof (SwaggerDataAttribute)) != null;
                if (!hasAttribute)
                {
                    return base.SetterValueFactory(type);
                }

                return ReflectionUtils.GetProperties(type)
                    .Where(x => x.CanWrite)
                    .Where(x => !ReflectionUtils.GetSetterMethodInfo(x).IsStatic)
                    .ToDictionary(GetMemberName, GetPropertyKeyValuePair);
            }

            private static dynamic ToObject(IDictionary source)
            {
                var eo = new ExpandoObject();
                var eoColl = (ICollection<KeyValuePair<string, object>>)eo;

                foreach (string key in source.Keys)
                {
                    eoColl.Add(new KeyValuePair<string, object>(key, source[key]));
                }

                return eo;
            }

            private static KeyValuePair<Type, ReflectionUtils.SetDelegate> GetPropertyKeyValuePair(PropertyInfo x)
            {
                return new KeyValuePair<Type, ReflectionUtils.SetDelegate>(x.PropertyType, ReflectionUtils.GetSetMethod(x));
            }

            private static string GetMemberName(MemberInfo member)
            {
                var attribute = (SwaggerPropertyAttribute) ReflectionUtils.GetAttribute(member, typeof(SwaggerPropertyAttribute));
                if (attribute != null && !string.IsNullOrEmpty(attribute.Name))
                {
                    return attribute.Name;
                }

                return member.Name;
            }
        }
    }
}