using Should;
using Xunit;

namespace Nancy.Swagger.Tests
{
    public class SwaggerModelPropertyDataTests
    {
        private SwaggerModelPropertyData propertyData;

        public SwaggerModelPropertyDataTests()
        {
            propertyData = new SwaggerModelPropertyData();
        }

        [Fact]
        public void ShouldSetEnumToNullByDefault()
        {
            // Assert
            propertyData.Enum.ShouldBeNull();
        }
    }
}