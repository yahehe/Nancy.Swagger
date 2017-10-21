using Swagger.ObjectModel.Builders;
using Xunit;

namespace Swagger.ObjectModel.Tests.Builders
{
    public class ApiKeySecuritySchemeBuilderTest
    {
        private ApiKeySecuritySchemeBuilder builder;
        private string name = string.Empty;

        public ApiKeySecuritySchemeBuilderTest()
        {
            this.builder = new ApiKeySecuritySchemeBuilder();
            this.name = "name";
        }

        [Fact]
        public void Should_ThrowRequiredFieldException_WhenNothingSet()
        {
            Assert.Throws<RequiredFieldException>(() => builder.Build());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Should_ThrowRequiredFieldException_WhenNameIsNullOrWhiteSpace(string name)
        {
            Assert.Throws<RequiredFieldException>(() => builder.Name(name).Build());
        }

        [Fact]
        public void Should_AbleToSetNameAndApiKeyInHeader()
        {
            var securityScheme = builder.Name(name).IsInHeader().Build();

            Assert.Equal(name, securityScheme.Name);
            Assert.Equal(ApiKeyLocations.Header, securityScheme.In);
        }

        [Fact]
        public void Should_AbleToSetNameAndApiKeyInQuery()
        {
            var securityScheme = builder.Name(name).IsInQuery().Build();

            Assert.Equal(name, securityScheme.Name);
            Assert.Equal(ApiKeyLocations.Query, securityScheme.In);
        }

        [Fact]
        public void Should_BeApiKey_WhenBuildSuccess()
        {
            var securityScheme = builder.Name(name).IsInHeader().Build();

            Assert.Equal(SecuritySchemes.ApiKey, securityScheme.Type);
        }

        [Fact]
        public void Should_AbleToSetDescription()
        {
            string description = "desc";

            var securityScheme = builder.Name(name).IsInHeader().Description(description).Build();

            Assert.Equal(description, securityScheme.Description);
        }

    }
}
