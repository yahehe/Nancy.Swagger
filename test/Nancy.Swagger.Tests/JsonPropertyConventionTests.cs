using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Nancy.Swagger.Attributes;
using Nancy.Swagger.Tests.Exceptions;
using Newtonsoft.Json;
using Xunit;

namespace Nancy.Swagger.Tests
{
    public class JsonPropertyConventionTests
    {
        [Fact]
        public void SwaggerDtoPropertiesShouldHaveJsonPropertyAttribute()
        {
            var typesWithProperties = new Dictionary<Type, List<PropertyInfo>>();

            var types = typeof(SwaggerDtoAttribute).Assembly.GetTypes().Where(IsSwaggerDto);

            foreach (var type in types)
            {
                var properties = type.GetProperties().Where(IsMissingJsonPropertyAttribute).ToList();
                if (properties.Any())
                {
                    typesWithProperties.Add(type, properties);
                }
            }

            if (!typesWithProperties.Any())
            {
                return;
            }

            var builder = new StringBuilder();

            builder.AppendFormat("{0} DTOs have properties with missing JsonProperty attribute:\n", typesWithProperties.Count);
            builder.AppendLine();

            foreach (var type in typesWithProperties)
            {
                builder.AppendFormat("{0} ({1} properties):\n", type.Key.Name, type.Value.Count);
                builder.AppendLine(string.Join(Environment.NewLine, type.Value.Select(x => x.Name)));
                builder.AppendLine();
            }

            throw new MissingJsonPropertyAttributeException(builder.ToString());
        }

        private static bool IsMissingJsonPropertyAttribute(PropertyInfo property)
        {
            return !property.GetCustomAttributes(typeof(JsonPropertyAttribute), false).Any();
        }

        private static bool IsSwaggerDto(Type type)
        {
            return type.GetCustomAttributes(typeof(SwaggerDtoAttribute), false).Any();
        }
    }
}