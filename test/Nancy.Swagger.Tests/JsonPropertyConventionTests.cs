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