using Swagger.ObjectModel.Builders;
using Xunit;

namespace Swagger.ObjectModel.Tests.Builders
{
    public class ExternalDocumentationBuilderTest
    {
        private readonly ExternalDocumentationBuilder builder;
        private readonly string url = string.Empty;

        public ExternalDocumentationBuilderTest()
        {
            this.builder = new ExternalDocumentationBuilder();
            this.url = "https://github.com/yahehe/Nancy.Swagger";
        }

        [Fact]
        public void Should_ThrowRequiredFieldException_WhenUrlIsNullOrEmpty()
        {
            Assert.Throws<RequiredFieldException>(() => builder.Url("").Build());
            Assert.Throws<RequiredFieldException>(() => builder.Url(null).Build());
        }

        [Fact]
        public void Should_ThrowRequiredFieldException_WhenUrlIsNotSet()
        {
            Assert.Throws<RequiredFieldException>(() => builder.Build());
            Assert.Throws<RequiredFieldException>(() => builder.Description("desc").Build());
        }

        [Fact]
        public void Should_AbleToSetUrl()
        {
            var externalDocumentation = builder.Url(url).Build();

            Assert.NotNull(externalDocumentation);
            Assert.Equal(url, externalDocumentation.Url);
        }

        [Fact]
        public void Should_AbleToSetDescription()
        {
            string description = "description";

            var externalDocumentation = builder.Url(url)
                                               .Description(description)
                                               .Build();

            Assert.NotNull(externalDocumentation);
            Assert.Equal(url, externalDocumentation.Url);
            Assert.Equal(description, externalDocumentation.Description);
        }

        [Fact]
        public void Should_AssignableFromSwaggerModel()
        {         
            var externalDocumentation = builder.Url(url)
                                               .Build();

            Assert.NotNull(externalDocumentation);
            Assert.IsAssignableFrom<SwaggerModel>(externalDocumentation);
        }
    }
}
