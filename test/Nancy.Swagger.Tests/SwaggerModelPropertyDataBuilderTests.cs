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
            var defaultValue = 42;

            builder.Default(defaultValue);

            builder.Data.DefaultValue.ShouldEqual(defaultValue);
        }

        [Fact]
        public void ShouldBeAbleToSetDescription()
        {
            const string Description = "Some description";

            builder.Description(Description);

            builder.Data.Description.ShouldEqual(Description);
        }

        [Fact]
        public void ShouldBeAbleToSetEnum()
        {
            var values = new[] { "1", "2", "3" };

            builder.Enum(values);

            builder.Data.Enum.ShouldEqual(values);
        }

        [Fact]
        public void ShouldBeAbleToSetMaximum()
        {
            var maximum = 12;

            builder.Maximum(maximum);

            builder.Data.Maximum.ShouldEqual(maximum);
        }

        [Fact]
        public void ShouldBeAbleToSetMinimum()
        {
            var minimum = 8;

            builder.Minimum(minimum);

            builder.Data.Minimum.ShouldEqual(minimum);
        }

        [Fact]
        public void ShouldBeAbleToSetRequired()
        {
            builder.Required(true);

            builder.Data.Required.ShouldBeTrue();
        }

        [Fact]
        public void ShouldBeAbleToSetUniqueItems()
        {
            builder.UniqueItems(true);

            builder.Data.UniqueItems.ShouldBeTrue();
        }
    }
}