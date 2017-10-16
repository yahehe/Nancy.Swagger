using Swagger.ObjectModel.Builders;
using System;
using Xunit;

namespace Swagger.ObjectModel.Tests.Builders
{
    public class ParameterBuilderTest
    {
        private readonly ParameterBuilder builder;
        private readonly string name = string.Empty;

        public ParameterBuilderTest()
        {
            this.builder = new ParameterBuilder();
            this.name= "name";
        }

        [Fact]
        public void Should_ThrowRequiredFieldException_WhenNameIsNotSet()
        {
            Assert.Throws(typeof(RequiredFieldException), () => builder.Build());         
        }

        [Fact]
        public void Should_ThrowRequiredFieldException_WhenNameIsNullOrWhiteSpace()
        {            
            Assert.Throws(typeof(RequiredFieldException), () => builder.Name("").Build());
            Assert.Throws(typeof(RequiredFieldException), () => builder.Name(null).Build());
        }

        [Fact]
        public void Should_ThrowRequiredFieldException_WhenParameterInIsNotSet()
        {
            Assert.Throws(typeof(RequiredFieldException), () => builder.Name(name).Build());
        }

        [Fact]
        public void Should_Required_WhenParameterInIsPath()
        {
            var parameter = builder.Name(name).In(ParameterIn.Path).Build();

            Assert.Equal(true, parameter.Required);
        }

        [Fact]
        public void Should_Required_WhenSetIsRequired()
        {
            var parameter = builder.Name(name).In(ParameterIn.Query).IsRequired().Build();

            Assert.Equal(true, parameter.Required);
        }

        [Theory]
        [InlineData(ParameterIn.Form)]
        [InlineData(ParameterIn.Header)]
        [InlineData(ParameterIn.Query)]
        public void ShouldNot_Required_WhenNotSetIsRequiredAndParameterInIsNotPathOrBody(ParameterIn parameterIn)
        {
            var parameter = builder.Name(name).In(parameterIn).Build();

            Assert.Equal(false, parameter.Required.HasValue);
        }

        [Fact]
        public void Should_ThrowInvalidOperationException_WhenParameterInIsBody()
        {
            Assert.Throws(typeof(InvalidOperationException), () => builder.Name(name).In(ParameterIn.Body).Build());
        }

        [Fact]
        public void Should_AbleToSetDescription()
        {
            string description = "description";

            var parameter = builder.Name(name).In(ParameterIn.Query).Description(description).Build();

            Assert.Equal(description, parameter.Description);
        }
    }
}
