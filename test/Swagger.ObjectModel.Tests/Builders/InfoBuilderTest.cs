using Swagger.ObjectModel.Builders;
using Xunit;

namespace Swagger.ObjectModel.Tests.Builders
{
    public class InfoBuilderTest
    {
        [Theory]
        [InlineData("", "")]
        [InlineData("", "1.0")]
        [InlineData("title", "")]
        [InlineData(null, null)]
        [InlineData(null, "1.0")]
        [InlineData("title", null)]
        public void Should_ThrowRequiredFieldException_WhenTitleOrVersionIsEmptyOrWhiteSpace(string title, string version)
        {
            Assert.Throws<RequiredFieldException>(() => new InfoBuilder(title, version).Build());
        }

        [Theory]
        [InlineData("title", "1.0", "description")]
        [InlineData("title", "1.0", "")]
        public void Should_AbleToSetDescription(string title, string version, string descrption)
        {
            var info = new InfoBuilder(title, version).Description(descrption).Build();

            Assert.Equal(title, info.Title);
            Assert.Equal(version, info.Version);
            Assert.Equal(descrption, info.Description);
        }

        [Theory]
        [InlineData("title", "1.0", "terms of service")]
        [InlineData("title", "1.0", "")]
        public void Should_AbleToSetTermsOfService(string title, string version, string termsOfService)
        {
            var info = new InfoBuilder(title, version).TermsOfService(termsOfService).Build();

            Assert.Equal(title, info.Title);
            Assert.Equal(version, info.Version);
            Assert.Equal(termsOfService, info.TermsOfService);
        }

        [Fact]
        public void Should_AbleToSetContact()
        {
            string title = "title";
            string version = "1.0";
            var contact = new Contact();
            var contactWithBuilder = new ContactBuilder().Build();

            var info = new InfoBuilder(title, version).Contact(contact).Build();
            var infoWithBuilder = new InfoBuilder(title, version).Contact(contactWithBuilder).Build();

            Assert.Equal(title, info.Title);
            Assert.Equal(version, info.Version);
            Assert.Equal(contact, info.Contact);
            Assert.Equal(contactWithBuilder, infoWithBuilder.Contact);
        }

        [Fact]
        public void Should_AbleToSetLicense()
        {
            string title = "title";
            string version = "1.0";
            var license = new License();
            var licenseWithBuilder = new LicenseBuilder("name").Build();

            var info = new InfoBuilder(title, version).License(license).Build();
            var infoWithBuilder = new InfoBuilder(title, version).License(licenseWithBuilder).Build();

            Assert.Equal(title, info.Title);
            Assert.Equal(version, info.Version);
            Assert.Equal(license, info.License);
            Assert.Equal(licenseWithBuilder, infoWithBuilder.License);
        }
    }
}
