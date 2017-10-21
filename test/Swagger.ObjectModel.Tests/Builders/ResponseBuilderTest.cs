using Swagger.ObjectModel.Builders;
using Xunit;

namespace Swagger.ObjectModel.Tests.Builders
{
    public class ResponseBuilderTest
    {
        private readonly ResponseBuilder builder;
        private readonly string description = string.Empty;

        public ResponseBuilderTest()
        {
            this.builder = new ResponseBuilder();
            this.description = "description";
        }

        [Fact]
        public void Should_ThrowRequiredFieldException_WhenDescriptionIsNotSet()
        {
            Assert.Throws<RequiredFieldException>(() => builder.Build());
        }

        [Fact]
        public void Should_ThrowRequiredFieldException_WhenDescriptionIsNullOrWhiteSpace()
        {
            Assert.Throws<RequiredFieldException>(() => builder.Description("").Build());
            Assert.Throws<RequiredFieldException>(() => builder.Description(null).Build());
        }

        [Fact]
        public void Should_AbleToSetDescription()
        {
            var response = builder.Description(description).Build();

            Assert.Equal(description, response.Description);
        }

        [Fact]
        public void Should_AbleToSetSchema()
        {
            var schema = new Schema();

            var response = builder.Description(description)
                                  .Schema(schema)
                                  .Build();

            Assert.Equal(schema, response.Schema);
        }

        [Fact]
        public void Should_AbleToSetSchemaWithSchemaBuilder()
        {
            var response = builder.Description(description)
                                .Schema<int>()
                                .Build();

            Assert.Equal(new SchemaBuilder<int>().Build().Type, response.Schema.Type);
        }

        [Fact]
        public void Should_AbleToSetSchemaWithSchemaBuilderAndAction()
        {
            var response = builder.Description(description)
                                .Schema<int>(x => new SchemaBuilder<int>().Build())
                                .Build();

            Assert.Equal(new SchemaBuilder<int>().Build().Type, response.Schema.Type);
        }

        [Fact]
        public void Should_AbleToSetHeader()
        {
            string headerName = "headerName";
            var header = new Header();

            var response = builder.Description(description)
                                .Header(headerName, header)
                                .Build();

            Assert.Equal(1, response.Headers.Count);
            Assert.Equal(header, response.Headers[headerName]);
        }

        [Fact]
        public void Should_AbleToSetHeaderWithHeaderBuilder()
        {
            string headerName = "headerName";
            var hBuilder = new HeaderBuilder();

            var response = builder.Description(description)
                                  .Header(headerName, hBuilder)
                                  .Build();

            Assert.Equal(1, response.Headers.Count);
            Assert.Equal(hBuilder.Build().Default, response.Headers[headerName].Default);
        }

        [Fact]
        public void Should_AbleToSetExample()
        {
            string mimeType = "mimeType";
            object obj = new object();

            var response = builder.Description(description)
                                  .Example(mimeType, obj)
                                  .Build();

            Assert.Equal(1, response.Examples.Count);
            Assert.Equal(obj, response.Examples[mimeType]);
        }
    }
}
