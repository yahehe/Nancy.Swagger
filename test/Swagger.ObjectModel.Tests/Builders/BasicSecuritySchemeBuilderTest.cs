using Swagger.ObjectModel.Builders;
using Xunit;

namespace Swagger.ObjectModel.Tests.Builders
{
    public class BasicSecuritySchemeBuilderTest
    {
        private BasicSecuritySchemeBuilder builder;

        public BasicSecuritySchemeBuilderTest()
        {
            this.builder = new BasicSecuritySchemeBuilder();
        }

        [Fact]
        public void Type_ShouldBeBasic()
        {
            var securityScheme = builder.Build();

            Assert.Equal(SecuritySchemes.Basic, securityScheme.Type);
        }

        [Fact]
        public void Description_ShouldBeSettable()
        {
            string description = "desc";

            var securityScheme = builder.Description(description).Build();

            Assert.Equal(description, securityScheme.Description);
        }
    }
}
