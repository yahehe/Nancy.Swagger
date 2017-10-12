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
            string name = "name";
        
            var contact = builder.Name(name).Build();

            Assert.NotNull(contact);
            Assert.Equal(name, contact.Name);
        }

        [Fact]
        public void Should_AbleToSetEmailAddress()
        {            
            string email = "youemail@yourcompany.com";
         
            var contact = builder.EmailAddress(email).Build();

            Assert.NotNull(contact);
            Assert.Equal(email, contact.EmailAddress);            
        }

        [Fact]
        public void Should_AbleToSetUrl()
        {            
            string url = "https://github.com/yahehe/Nancy.Swagger";
         
            var contact = builder.Url(url).Build();

            Assert.NotNull(contact);
            Assert.Equal(url, contact.Url);
        }

        [Fact]
        public void Should_AbleToSetAllProperties()
        {            
            string name = "name";
            string email = "youemail@yourcompany.com";
            string url = "https://github.com/yahehe/Nancy.Swagger";
         
            var contact = builder.Name(name)
                                 .EmailAddress(email)
                                 .Url(url)
                                 .Build();

            Assert.NotNull(contact);
            Assert.Equal(name, contact.Name);
            Assert.Equal(email, contact.EmailAddress);
            Assert.Equal(url, contact.Url);
        }

    }
}
