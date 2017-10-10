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
            // Arrange              
            // Act            
            var header = builder.Build();
            
            // Assert
            Assert.NotNull(header);            
            Assert.Null(header.Description);
            Assert.Null(header.Default);
        }

        [Fact]
        public void Should_AbleToSetDescription()
        {
            // Arrange       
            string description = "description";

            // Act
            var header = builder.Description(description).Build();

            // Assert
            Assert.NotNull(header);
            Assert.Equal(description,header.Description);
        }

        [Fact]
        public void Should_AbleToSetDefaultOfDataType()
        {
            // Arrange       
            object defaultValue = new object();

            // Act
            var header = builder.Default(defaultValue).Build();

            // Assert
            Assert.NotNull(header);
            Assert.Equal(defaultValue, header.Default);
        }
    }
}
