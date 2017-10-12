using Should;
using Xunit;

namespace Swagger.ObjectModel.Tests
{
    public class UtilitiesTest
    {
        [Fact]
        public void ToCamelCase_EmptyOrNull_ShouldReturnItself()
        {            
            string emptyString = string.Empty;
            string nullString = null;
            string whiteSpaceString = "";
            
            var actual_empty = Utilities.ToCamelCase(emptyString);
            var actual_null = Utilities.ToCamelCase(nullString);
            var actual_whitespace = Utilities.ToCamelCase(whiteSpaceString);
            
            actual_empty.ShouldEqual(emptyString);
            actual_null.ShouldEqual(actual_null);
            actual_whitespace.ShouldEqual(whiteSpaceString);            
        }

        [Fact]
        public void ToCamelCase_StartWithDigit_ShouldReturnStartWith_()
        {            
            string val = "123";
                     
            var actual = Utilities.ToCamelCase(val);
            
            actual.ShouldEqual("_123");
        }

        [Fact]
        public void ToCamelCase_StartWithUpperLetter_ShouldReturnStartWithLowerLetter()
        {            
            string val = "Abc";
            
            var actual = Utilities.ToCamelCase(val);

            actual.ShouldEqual("abc");
        }

        [Fact]
        public void ToCamelCase__ShouldReturnCamelString()
        {            
            string val = "AbC123dEf";
         
            var actual = Utilities.ToCamelCase(val);
            
            actual.ShouldEqual("abC123DEf");
        }


        [Fact]
        public void ToCamelCase_WithSpecialSymbol__ShouldReturnCamelString()
        {            
            string val = string.Format("{0}/{1}","get", "values");
         
            var actual = Utilities.ToCamelCase(val);

            actual.ShouldEqual("getValues");
        }
    }
}
