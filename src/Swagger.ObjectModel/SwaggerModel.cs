using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;

using Swagger.ObjectModel.Reflection;

namespace Swagger.ObjectModel
{
    using Swagger.ObjectModel.Attributes;

    /// <summary>
    /// The base class for all Swagger models with logic to serialize it according to the Swagger schema.
    /// </summary>
    [SwaggerData]
    public class SwaggerModel
    {
        private static readonly IJsonSerializerStrategy SerializerStrategy = new SwaggerSerializerStrategy();

        /// <summary>
        /// Gets or sets the references to a globally defined object
        /// </summary>
        /// <remarks>
        /// The value MUST be a model's id.
        /// </remarks>
        [SwaggerProperty("$ref")]
        public string Ref { get; set; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return this.ToJson();
        }

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
                var member = @enum.GetType().GetMember(@enum.ToString()).Single();

                var attribute = member.GetCustomAttribute<SwaggerEnumValueAttribute>();
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

                var getters = GetCache[input.GetType()].Where(x => x.Value != null);

                foreach (var getter in getters)
                {
                    var value = getter.Value(input);
                    if (value == null)
                    {
                        continue;
                    }

                    var dictionary = value as IDictionary;
                    if (dictionary != null)
                    {
                        if (dictionary is IDictionary<SecuritySchemes, IEnumerable<string>> security)
                        {
                            value = ToArray(security);
                        }
                        else
                        {
                            value = ToObject(dictionary);
                        }
                    }

                    var fieldName = MapClrMemberNameToJsonFieldName(getter.Key);

                    result.Add(fieldName, value);
                }

                output = result;
                return true;
            }

            // Serialized properties should use name from SwaggerPropertyAttribute if it exists.
            internal override IDictionary<string, ReflectionUtils.GetDelegate> GetterValueFactory(Type type)
            {
                if (!type.GetTypeInfo().IsDefined<SwaggerDataAttribute>())
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
                if (!type.GetTypeInfo().IsDefined<SwaggerDataAttribute>())
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
                var expando = new ExpandoObject();
                var expandoCollection = (ICollection<KeyValuePair<string, object>>)expando;

                foreach (var key in source.Keys)
                {
                    expandoCollection.Add(new KeyValuePair<string, object>(key.ToString(), source[key]));
                }

                return expando;
            }

            private static IEnumerable<dynamic> ToArray(IDictionary<SecuritySchemes, IEnumerable<string>> source)
            {
                foreach (SecuritySchemes key in source.Keys)
                {
                    var expando = new ExpandoObject();
                    var expandoCollection = (ICollection<KeyValuePair<string, object>>)expando;
                    expandoCollection.Add(new KeyValuePair<string, object>(key.ToString(), source[key]));

                    yield return expando;
                }
            }

            private static KeyValuePair<Type, ReflectionUtils.SetDelegate> GetPropertyKeyValuePair(PropertyInfo x)
            {
                return new KeyValuePair<Type, ReflectionUtils.SetDelegate>(x.PropertyType, ReflectionUtils.GetSetMethod(x));
            }

            private static string GetMemberName(MemberInfo member)
            {
                var attribute = member.GetCustomAttribute<SwaggerPropertyAttribute>();
                if (attribute != null && !string.IsNullOrEmpty(attribute.Name))
                {
                    return attribute.Name;
                }

                return member.Name;
            }
        }
    }
}
