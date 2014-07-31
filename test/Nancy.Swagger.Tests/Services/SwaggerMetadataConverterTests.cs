using Should;
using Xunit;

namespace Nancy.Swagger.Tests.Services
{
    public class SwaggerMetadataConverterTests
    {
        private readonly SwaggerMetadataConverterForTesting converter;

        public SwaggerMetadataConverterTests()
        {
            converter = new SwaggerMetadataConverterForTesting();
        }

        [Fact]
        public void ShouldExcludePrimitiveTypesFromApiDeclarationModels()
        {
            // Arrange
            converter.RouteDataAccessor = new[]
                {
                    new SwaggerRouteData { ResourcePath = "/test", OperationModel = typeof(string) }
                };

            // Act
            var declaration = converter.GetApiDeclaration("/test");

            // Assert
            declaration.Models.Count.ShouldEqual(0);
        }

        [Fact]
        public void ShouldIncludComplexTypesInApiDeclarationModels()
        {
            // Arrange
            converter.RouteDataAccessor = new[]
                {
                    new SwaggerRouteData { ResourcePath = "/test", OperationModel = typeof(TestModel) }
                };

            // Act
            var declaration = converter.GetApiDeclaration("/test");

            // Assert
            declaration.Models.Keys.ShouldContain("TestModel");
        }
    }
}
