using Swagger.ObjectModel.Builders;
using Xunit;

namespace Swagger.ObjectModel.Tests.Builders
{
    public class BodyParameterBuilderTest
    {
        private readonly BodyParameterBuilder builder;
        private readonly string name;
        private readonly Schema schema;

        public BodyParameterBuilderTest()
        {
            this.builder = new BodyParameterBuilder();

            this.name = "name";
            this.schema = new Schema
            {
                Description = "some description"
            };
        }

        [Fact]
        public void Should_ThrowRequiredFieldException_WhenNameOrSchemaIsNull()
        {
            Assert.Throws<RequiredFieldException>(() => builder.Build());
            Assert.Throws<RequiredFieldException>(() => builder.Name(name).Build());
            Assert.Throws<RequiredFieldException>(() => builder.Name(string.Empty).Schema(schema).Build());
        }

        [Fact]
        public void Should_AbleToSetNameAndSchema()
        {
            var bodyParameter = builder.Name(name)
                                       .Schema(schema)
                                       .Build();

            Assert.NotNull(bodyParameter);
            Assert.Equal(name, bodyParameter.Name);
            Assert.Same(schema, bodyParameter.Schema);
        }

        [Fact]
        public void Should_AbleToSetDescription()
        {
            string description = "description";

            var bodyParameter = builder.Name(name)
                                       .Schema(schema)
                                       .Description(description)
                                       .Build();

            Assert.NotNull(bodyParameter);
            Assert.Equal(description, bodyParameter.Description);
        }
    }
}
