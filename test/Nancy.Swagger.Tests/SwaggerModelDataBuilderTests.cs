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
            const string Description = "Some description";

            builder.Description(Description);

            builder.Data.Description.ShouldEqual(Description);
        }

        [Fact]
        public void ShouldBeAbleToAccessPropertyBuilderForProperty()
        {
            var propertyBuilder = builder.Property(x => x.SomeInt);

            propertyBuilder.ShouldNotBeNull();
            propertyBuilder.Data.Name.ShouldEqual("SomeInt");
            propertyBuilder.Data.Type.ShouldEqual(typeof(int));
        }
    }
}