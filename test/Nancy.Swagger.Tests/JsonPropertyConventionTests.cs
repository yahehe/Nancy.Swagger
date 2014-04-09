using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Nancy.Swagger.Attributes;
using Newtonsoft.Json;
using Should;
using Xunit.Extensions;
using Xunit.Sdk;

namespace Nancy.Swagger.Tests
{
    public class JsonPropertyConventionTests
    {
        /// <summary>
        /// Checks that all (public) properties of all types marked with the <see cref="SwaggerDtoAttribute"/>
        /// has explicitly defined a <see cref="JsonPropertyAttribute"/> with <see cref="JsonPropertyAttribute.PropertyName"/>.
        /// This allows us to rename properties on the DTOs without worrying about breaking the Swagger JSON schema.
        /// </summary>
        /// <param name="property">The property to test.</param>
        [JsonPropertyConventionTestAttribute]
        public void SwaggerDtoPropertiesShouldHaveJsonPropertyAttribute(PropertyInfo property)
        {
            var attribute = property.GetCustomAttribute<JsonPropertyAttribute>();

            attribute.ShouldNotBeNull();
            attribute.PropertyName.ShouldNotBeNull();
        }

        private class JsonPropertyConventionTestAttribute : TheoryAttribute
        {
            protected override IEnumerable<ITestCommand> EnumerateTestCommands(IMethodInfo method)
            {
                return typeof(SwaggerDtoAttribute).Assembly.GetTypes()
                    .Where(type => type.IsDefined<SwaggerDtoAttribute>())
                    .SelectMany(type => type.GetProperties())
                    .Select(property => new JsonPropertyTestCommand(method, property));
            }

            private class JsonPropertyTestCommand : TheoryCommand
            {
                public JsonPropertyTestCommand(IMethodInfo testMethod, PropertyInfo property)
                    : base(testMethod, new object[] { property })
                {
                    DisplayName = string.Join(".", property.DeclaringType.Name, property.Name);
                }
            }
        }
    }
}