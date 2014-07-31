using Should;
using Xunit;

namespace Nancy.Swagger.Tests
{
    public class SwaggerModelDataBuilderTests
    {
        private readonly SwaggerModelDataBuilder<TestModel> builder;

        public SwaggerModelDataBuilderTests()
        {
            builder = new SwaggerModelDataBuilder<TestModel>();
        }

        [Fact]
        public void ShouldBeAbleToSetDescription()
        {
            // Arrange
            const string Description = "Some description";

            // Act
            builder.Description(Description);

            // Assert
            builder.Data.Description.ShouldEqual(Description);
        }

        [Fact]
        public void ShouldBeAbleToAccessPropertyBuilderForProperty()
        {
            // Act
            var propertyBuilder = builder.Property(x => x.SomeInt);

            // Assert
            propertyBuilder.ShouldNotBeNull();
            propertyBuilder.Data.Name.ShouldEqual("SomeInt");
            propertyBuilder.Data.Type.ShouldEqual(typeof(int));
        }
    }
}