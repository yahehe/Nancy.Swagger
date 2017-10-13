using Swagger.ObjectModel.Builders;
using Xunit;

namespace Swagger.ObjectModel.Tests.Builders
{
    public class TagBuilderTest
    {
        private readonly TagBuilder builder;
        private readonly string name = string.Empty;

        public TagBuilderTest()
        {
            this.builder = new TagBuilder();
            this.name = "name";
        }

        [Fact]
        public void Should_ThrowRequiredFieldException_When_NameIsNotSet()
        {
            Assert.Throws(typeof(RequiredFieldException), () => builder.Build());            
        }


        [Fact]
        public void Should_ThrowRequiredFieldException_When_NameIsNullOrWhiteSpace()
        {
            Assert.Throws(typeof(RequiredFieldException), () => builder.Name(null).Build());
            Assert.Throws(typeof(RequiredFieldException), () => builder.Name("").Build());
        }

        [Fact]
        public void Should_AbleToSetName()
        {            
            var tag = builder.Name(name).Build();

            Assert.Equal(name, tag.Name);
        }

        [Fact]
        public void Should_AbleToSetDescription()
        {
            string description = "description";

            var tag = builder.Name(name).Description(description).Build();

            Assert.Equal(description, tag.Description);
        }

        [Fact]
        public void Should_AbleToSetExternalDocumentation()
        {
            var externalDocumentation = new ExternalDocumentation();        

            var tag = builder.Name(name)
                             .ExternalDocumentation(externalDocumentation)
                             .Build();
            
            Assert.Equal(externalDocumentation, tag.ExternalDocumentation);            
        }

        [Fact]
        public void Should_AbleToSetExternalDocumentationWithBuilder()
        {
            var exBuilder = new ExternalDocumentationBuilder().Url("url") ;

            var tag = builder.Name(name)
                             .ExternalDocumentation(exBuilder)
                             .Build();

            Assert.Equal("url", tag.ExternalDocumentation.Url);
        }
    }
}