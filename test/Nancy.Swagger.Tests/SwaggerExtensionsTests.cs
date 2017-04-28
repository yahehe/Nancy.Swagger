using Should;
using Swagger.ObjectModel;

using System.Linq;
using Xunit;

namespace Nancy.Swagger.Tests
{
    public class SwaggerExtensionsTests
    {
        [Fact]
        public void ToModelProperty_NonPrimitive_ShouldHaveRefSet()
        {
            new SwaggerModelPropertyData
            {
                Type = typeof(TestModel)
            }.ToModelProperty().ShouldEqual(
                new ModelProperty
                {
                    Ref = SwaggerConfig.ModelIdConvention(typeof(TestModel))
                }
            );
        }

        [Fact]
        public void ToModelProperty_Primitive_ShouldHaveTypeSet()
        {
            new SwaggerModelPropertyData
            {
                Type = typeof(string)
            }.ToModelProperty().ShouldEqual(
                new ModelProperty
                {
                    Type = "string"
                }
            );
        }

        [Fact]
        public void ToModelProperty_PrimitiveCollection_ShouldHaveTypeArrayAndItemsTypeSet()
        {
            new SwaggerModelPropertyData
            {
                Type = typeof(string[])
            }.ToModelProperty().ShouldEqual(
                new ModelProperty
                {
                    Type = "array",
                    Items = new Item { Type = "string" }
                }
            );
        }

        [Fact]
        public void ToModelPropertyNonPrimitiveCollection_ShouldHaveTypeArrayAndItemsRefSet()
        {
            new SwaggerModelPropertyData
            {
                Type = typeof(TestModel[])
            }.ToModelProperty().ShouldEqual(
                new ModelProperty
                {
                    Type = "array",
                    Items = new Item { Ref = SwaggerConfig.ModelIdConvention(typeof(TestModel)) }
                },
                "String return type"
            );
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData("a", "a")]
        [InlineData("ab", "ab")]
        [InlineData("aB", "aB")]
        [InlineData("AB", "aB")]
        [InlineData("Ab", "ab")]
        [InlineData(" a", "a")]
        [InlineData(" A", "a")]
        [InlineData("a b", "aB")]
        [InlineData("1", "_1")]
        [InlineData("a1b", "a1B")]
        public void ToCamelCase_TestCases(string value, string expected)
        {
            var valueString = value == null ? "null" : string.Format("\"{0}\"", value);
            var expectedString = expected == null ? "null" : string.Format("\"{0}\"", expected);
            value.ToCamelCase().ShouldEqual(expected, string.Format("{0}.ToCamelCase() should equal {1}", valueString, expectedString));
        }
    }
}