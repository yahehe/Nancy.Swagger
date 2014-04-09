using System.Reflection;
using Newtonsoft.Json;
using Should;

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
    }
}