using Should;
using Xunit;

namespace Nancy.Swagger.Tests
{
    public class SwaggerModelPropertyDataBuilderTests
    {
        private readonly SwaggerModelPropertyDataBuilder<int> builder;

        public SwaggerModelPropertyDataBuilderTests()
        {
            var data = new SwaggerModelPropertyData();

            builder = new SwaggerModelPropertyDataBuilder<int>(data);
        }

        [Fact]
        public void ShouldBeAbleToSetDefault()
        {
            // Arrange
            var defaultValue = 42;

            // Act
            builder.Default(defaultValue);

            // Assert
            builder.Data.DefaultValue.ShouldEqual(defaultValue);
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
        public void ShouldBeAbleToSetEnum()
        {
            // Arrange
            var values = new[] { "1", "2", "3" };

            // Act
            builder.Enum(values);

            // Assert
            builder.Data.Enum.ShouldEqual(values);
        }

        [Fact]
        public void ShouldBeAbleToSetMaximum()
        {
            // Arrange
            var maximum = 12;

            // Act
            builder.Maximum(maximum);

            // Assert
            builder.Data.Maximum.ShouldEqual(maximum);
        }

        [Fact]
        public void ShouldBeAbleToSetMinimum()
        {
            // Arrange
            var minimum = 8;

            // Act
            builder.Minimum(minimum);

            // Assert
            builder.Data.Minimum.ShouldEqual(minimum);
        }

        [Fact]
        public void ShouldBeAbleToSetRequired()
        {
            // Act
            builder.Required(true);

            // Assert
            builder.Data.Required.ShouldBeTrue();
        }

        [Fact]
        public void ShouldBeAbleToSetUniqueItems()
        {
            // Act
            builder.UniqueItems(true);

            // Assert
            builder.Data.UniqueItems.ShouldBeTrue();
        }
    }
}