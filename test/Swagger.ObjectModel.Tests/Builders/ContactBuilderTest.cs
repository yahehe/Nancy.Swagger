using Swagger.ObjectModel.Builders;
using Xunit;

namespace Swagger.ObjectModel.Tests.Builders
{
    public class ContactBuilderTest
    {
        private readonly ContactBuilder builder;
        public ContactBuilderTest()
        {
            this.builder = new ContactBuilder();
        }

        [Fact]
        public void Should_AbleToSetName()
        {
            // Arrange   
            string name = "name";

            // Act
            var contact = builder.Name(name).Build();

            // Assert
            Assert.NotNull(contact);
            Assert.Equal(name, contact.Name);
        }

        [Fact]
        public void Should_AbleToSetEmailAddress()
        {
            // Arrange   
            string email = "youemail@yourcompany.com";

            // Act
            var contact = builder.EmailAddress(email).Build();

            // Assert
            Assert.NotNull(contact);
            Assert.Equal(email, contact.EmailAddress);            
        }

        [Fact]
        public void Should_AbleToSetUrl()
        {
            // Arrange   
            string url = "https://github.com/yahehe/Nancy.Swagger";

            // Act
            var contact = builder.Url(url).Build();

            // Assert
            Assert.NotNull(contact);
            Assert.Equal(url, contact.Url);
        }

        [Fact]
        public void Should_AbleToSetAllProperties()
        {
            // Arrange   
            string name = "name";
            string email = "youemail@yourcompany.com";
            string url = "https://github.com/yahehe/Nancy.Swagger";

            // Act
            var contact = builder.Name(name)
                                 .EmailAddress(email)
                                 .Url(url)
                                 .Build();

            // Assert
            Assert.NotNull(contact);
            Assert.Equal(name, contact.Name);
            Assert.Equal(email, contact.EmailAddress);
            Assert.Equal(url, contact.Url);
        }

    }
}
