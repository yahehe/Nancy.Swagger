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
            // Assert                      
            Assert.Throws(typeof(RequiredFieldException), () => builder.Url("").Build());
            Assert.Throws(typeof(RequiredFieldException), () => builder.Url(null).Build());            
        }

        [Fact]
        public void Should_ThrowRequiredFieldException_WhenUrlIsNotSet()
        {
            // Assert
            Assert.Throws(typeof(RequiredFieldException), () => builder.Build());
            Assert.Throws(typeof(RequiredFieldException), () => builder.Description("desc").Build());            
        }

        [Fact]
        public void Should_AbleToSetUrl()
        {
            // Arrange              
            // Act
            var externalDocumentation = builder.Url(url).Build();

            // Assert
            Assert.NotNull(externalDocumentation);
            Assert.Equal(url, externalDocumentation.Url);
        }

        [Fact]
        public void Should_AbleToSetDescription()
        {
            // Arrange    
            string description = "description";

            // Act
            var externalDocumentation = builder.Url(url)
                                               .Description(description)
                                               .Build();

            // Assert
            Assert.NotNull(externalDocumentation);
            Assert.Equal(url, externalDocumentation.Url);
            Assert.Equal(description, externalDocumentation.Description);
        }
    }
}
