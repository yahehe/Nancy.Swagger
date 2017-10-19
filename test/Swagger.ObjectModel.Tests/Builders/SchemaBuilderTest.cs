using Swagger.ObjectModel.Builders;
using Xunit;

namespace Swagger.ObjectModel.Tests.Builders
{
    public class SchemaBuilderTest
    {
        private SchemaBuilder<int> GetInt32SchemaBuilder()
        {
            return new SchemaBuilder<int>();
        }

        private SchemaBuilder<string> GetStringSchemaBuilder()
        {
            return new SchemaBuilder<string>();
        }

        [Fact]
        public void Type_ShouldDefaultToInt32()
        {
            var result = GetInt32SchemaBuilder().Build();

            Assert.Equal("Int32", result.Type);
        }

        [Fact]
        public void Type_ShouldDefaultToString()
        {
            var result = GetStringSchemaBuilder().Build();

            Assert.Equal("String", result.Type);
        }

        [Fact]
        public void Ref_ShouldDefaultToNull()
        {
            var result = GetInt32SchemaBuilder().Build();

            Assert.Null(result.Ref);
        }

        [Fact]
        public void Discriminator_ShouldBeSettable()
        {
            string discriminator = "discriminator";

            var result = GetInt32SchemaBuilder().Discriminator(discriminator).Build();

            Assert.Equal(discriminator, result.Discriminator);
        }

        [Fact]
        public void ExternalDocumentation_ShouldBeSettable()
        {
            var externalDocumentation = new ExternalDocumentation() { Url = "url" };

            var result = GetInt32SchemaBuilder().ExternalDocumentation(externalDocumentation).Build();

            Assert.Equal(externalDocumentation.Url, result.ExternalDocumentation.Url);
        }

        [Fact]
        public void ExternalDocumentationWithBuilder_ShouldBeSettable()
        {
            var exBuilder = new ExternalDocumentationBuilder().Url("url");

            var result = GetInt32SchemaBuilder().ExternalDocumentation(exBuilder).Build();

            Assert.Equal(exBuilder.Build().Url, result.ExternalDocumentation.Url);
        }

        [Fact]
        public void Example_ShouldBeSettable()
        {
            object example = new object();

            var result = GetInt32SchemaBuilder().Example(example).Build();

            Assert.Equal(example, result.Example);
        }
    }
}
