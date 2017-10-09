using Should;
using Xunit;

namespace Swagger.ObjectModel.Tests
{
    public class UtilitiesTest
    {
        [Fact]
        public void ToCamelCase_EmptyOrNull_ShouldReturnItself()
        {
            // Arrange
            string emptyString = string.Empty;
            string nullString = null;
            string whiteSpaceString = "";

            // Act
            var actual_empty = Utilities.ToCamelCase(emptyString);
            var actual_null = Utilities.ToCamelCase(nullString);
            var actual_whitespace = Utilities.ToCamelCase(whiteSpaceString);

            // Assert
            actual_empty.ShouldEqual(emptyString);
            actual_null.ShouldEqual(actual_null);
            actual_whitespace.ShouldEqual(whiteSpaceString);            
        }

        [Fact]
        public void ToCamelCase_StartWithDigit_ShouldReturnStartWith_()
        {
            // Arrange
            string val = "123";
            
            // Act
            var actual = Utilities.ToCamelCase(val);

            // Assert
            actual.ShouldEqual("_123");
        }

        [Fact]
        public void ToCamelCase_StartWithUpperLetter_ShouldReturnStartWithLowerLetter()
        {
            // Arrange
            string val = "Abc";

            // Act
            var actual = Utilities.ToCamelCase(val);

            // Assert
            actual.ShouldEqual("abc");
        }

        [Fact]
        public void ToCamelCase__ShouldReturnCamelString()
        {
            // Arrange
            string val = "AbC123dEf";

            // Act
            var actual = Utilities.ToCamelCase(val);

            // Assert
            actual.ShouldEqual("abC123DEf");
        }


        [Fact]
        public void ToCamelCase_WithSpecialSymbol__ShouldReturnCamelString()
        {
            // Arrange
            string val = string.Format("{0}/{1}","get", "values");

            // Act
            var actual = Utilities.ToCamelCase(val);

            // Assert
            actual.ShouldEqual("getValues");
        }
    }
}
