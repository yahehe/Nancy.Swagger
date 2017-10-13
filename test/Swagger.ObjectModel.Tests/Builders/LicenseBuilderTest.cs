using Swagger.ObjectModel.Builders;
using Xunit;

namespace Swagger.ObjectModel.Tests.Builders
{
    public class LicenseBuilderTest
    {                     
        [Theory]
        [InlineData("")]        
        [InlineData(null)]
        public void Should_ThrowRequiredFieldException_When_NameIsNullOrWhiteSpace(string name)
        {            
            Assert.Throws(typeof(RequiredFieldException), () => new LicenseBuilder(name).Build());
        }

        [Theory]
        [InlineData("")]
        [InlineData("https://github.com/yahehe/Nancy.Swagger")]
        public void Should_AbleToSetUrl(string url)
        {
            string name = "MIT";
            var license = new LicenseBuilder(name).Url(url).Build();

            Assert.Equal(url, license.Url);
        }        
    }
}
