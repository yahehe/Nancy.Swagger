using Swagger.ObjectModel.Builders;
using Xunit;

namespace Swagger.ObjectModel.Tests.Builders
{
    public class HeaderBuilderTest
    {
        private readonly HeaderBuilder builder;

        public HeaderBuilderTest()
        {
            this.builder = new HeaderBuilder();
        }

        [Fact]
        public void Should_ReturnEmptyHeader_WhenSetNothing()
        {
            var header = builder.Build();
                     
            Assert.NotNull(header);            
            Assert.Null(header.Description);
            Assert.Null(header.Default);
        }

        [Fact]
        public void Should_AbleToSetDescription()
        {            
            string description = "description";
         
            var header = builder.Description(description).Build();

            Assert.NotNull(header);
            Assert.Equal(description,header.Description);
        }

        [Fact]
        public void Should_AbleToSetDefaultOfDataType()
        {             
            object defaultValue = new object();
         
            var header = builder.Default(defaultValue).Build();

            Assert.NotNull(header);
            Assert.Equal(defaultValue, header.Default);
        }
    }
}
