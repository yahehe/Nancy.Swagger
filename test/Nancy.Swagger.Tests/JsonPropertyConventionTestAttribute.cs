using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Nancy.Swagger.Attributes;
using Xunit.Extensions;
using Xunit.Sdk;

namespace Nancy.Swagger.Tests
{
    public class JsonPropertyConventionTestAttribute : TheoryAttribute
    {
        protected override IEnumerable<ITestCommand> EnumerateTestCommands(IMethodInfo method)
        {
            return typeof(SwaggerDtoAttribute).Assembly.GetTypes()
                .Where(type => type.GetCustomAttributes(typeof(SwaggerDtoAttribute), false).Any())
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