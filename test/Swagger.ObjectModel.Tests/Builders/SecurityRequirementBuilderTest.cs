using Swagger.ObjectModel.Builders;
using Xunit;

namespace Swagger.ObjectModel.Tests.Builders
{
    public class SecurityRequirementBuilderTest
    {
        private SecurityRequirementBuilder builder;

        public SecurityRequirementBuilderTest()
        {
            this.builder = new SecurityRequirementBuilder();
        }

        [Fact]
        public void Should_ThrowRequiredFieldException_WhenSchemeIsNotSet()
        {
            Assert.Throws<RequiredFieldException>(() => builder.Build());
        }

        [Theory]
        [InlineData(SecuritySchemes.ApiKey)]
        [InlineData(SecuritySchemes.Basic)]
        [InlineData(SecuritySchemes.Oauth2)]
        public void Should_AbleToSetSecurityScheme(SecuritySchemes schemes)
        {
            var result = builder.SecurityScheme(schemes).Build();
            Assert.Equal(schemes, result.Key);
        }

        [Theory]
        [InlineData(SecuritySchemes.ApiKey)]
        [InlineData(SecuritySchemes.Basic)]
        public void Should_ReturnEmptyList_WhenSecuritySchemeIsNotOauth2(SecuritySchemes schemes)
        {
            string scopeName = "scopeName";

            var result = builder.SecurityScheme(schemes).SecurityScheme(scopeName).Build();
            Assert.Empty(result.Value);
        }

        [Fact]
        public void ShouldNot_ReturnEmptyList_WhenSecuritySchemeIsOauth2()
        {
            string scopeName = "scopeName";

            var result = builder.SecurityScheme(SecuritySchemes.Oauth2).SecurityScheme(scopeName).Build();

            Assert.NotEmpty(result.Value);
        }
    }
}
