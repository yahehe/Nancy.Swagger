using Nancy.Swagger.Services;
using Should;
using Xunit;

namespace Nancy.Swagger.Tests.Services
{
    public class SwaggerMetadataProviderTests
    {
        private readonly ISwaggerMetadataConverter converter;
        private readonly SwaggerMetadataProviderForTesting provider;

        public SwaggerMetadataProviderTests()
        {   
            provider = new SwaggerMetadataProviderForTesting();
            converter = new DefaultSwaggerMetadataConverter(provider);
        }

        [Fact]
        public void ShouldExcludePrimitiveTypesFromApiDeclarationModels()
        {
            // Arrange
            provider.RouteDataAccessor = new[]
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
            provider.RouteDataAccessor = new[]
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
