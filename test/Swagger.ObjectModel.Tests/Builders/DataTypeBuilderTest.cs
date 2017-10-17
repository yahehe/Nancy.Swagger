using System.Collections.Generic;
using System.Linq;
using Swagger.ObjectModel.Builders;
using Xunit;

namespace Swagger.ObjectModel.Tests.Builders
{
    public class DataTypeBuilderTest
    {
        private readonly CustomDataTypeBuilder builder;

        internal class CustomDataTypeBuilder : DataTypeBuilder<CustomDataTypeBuilder, DataType>
        {
            public CustomDataTypeBuilder() : base()
            {
            }

            public override DataType Build()
            {
                return this.BuildBase();
            }
        }

        public DataTypeBuilderTest()
        {
            this.builder = new CustomDataTypeBuilder();
        }

        [Fact]
        public void Maximum_ShouldDefaultToNull()
        {
            var result = builder.Build();

            Assert.NotNull(result);
            Assert.Equal(null, result.Maximum);
        }

        [Fact]
        public void Maximum_ShouldBeSettable()
        {            
            var maxValue = int.MaxValue;
        
            var result = builder.Maximum(maxValue).Build();

            Assert.NotNull(result);
            Assert.Equal(maxValue, result.Maximum);
        }

        [Fact]
        public void Minimum_ShouldDefaultToNull()
        {
            var result = builder.Build();

            Assert.NotNull(result);
            Assert.Equal(null, result.Minimum);
        }

        [Fact]
        public void Minimum_ShouldBeSettable()
        {            
            var minValue = int.MinValue;
        
            var result = builder.Minimum(minValue).Build();

            Assert.NotNull(result);
            Assert.Equal(minValue, result.Minimum);
        }

        [Fact]
        public void Type_ShouldDefaultToNull()
        {
            var result = builder.Build();

            Assert.NotNull(result);
            Assert.Equal(null, result.Type);
        }

        [Fact]
        public void Type_ShouldBeSettable()
        {            
            var type = "int";
        
            var result = builder.Type(type).Build();

            Assert.NotNull(result);
            Assert.Equal(type, result.Type);
        }

        [Fact]
        public void CollectionFormat_ShouldDefaultToNull()
        {
            var result = builder.Build();

            Assert.NotNull(result);
            Assert.Equal(null, result.CollectionFormat);
        }

        [Fact]
        public void CollectionFormat_ShouldBeSettable()
        {
            var formats = new CollectionFormats();

            var result = builder.CollectionFormat(formats).Build();

            Assert.NotNull(result);
            Assert.Equal(formats, result.CollectionFormat);
        }

        [Fact]
        public void Default_ShouldDefaultToNull()
        {
            var result = builder.Build();

            Assert.NotNull(result);
            Assert.Equal(null, result.Default);
        }

        [Fact]
        public void Default_ShouldBeSettable()
        {
            var obj = new object();
            
            var result = builder.Default(obj).Build();

            Assert.NotNull(result);
            Assert.Equal(obj, result.Default);
        }

        [Fact]
        public void Enum_ShouldDefaultToNull()
        {
            var result = builder.Build();

            Assert.NotNull(result);
            Assert.Equal(null, result.Enum);
        }

        [Fact]
        public void Enum_ShouldBeSettable()
        {
            var enumValue = "enum";
            
            var result = builder.Enum(enumValue).Build();

            Assert.NotNull(result);
            Assert.Equal(enumValue, result.Enum.First());
        }

        [Fact]
        public void ExclusiveMax_ShouldDefaultToNull()
        {
            var result = builder.Build();

            Assert.NotNull(result);
            Assert.Equal(null, result.ExclusiveMaximum);
        }

        [Fact]
        public void ExclusiveMax_ShouldBeSettable()
        {
            var result = builder.IsExclusiveMaximum().Build();

            Assert.NotNull(result);
            Assert.Equal(true, result.ExclusiveMaximum);
        }

        [Fact]
        public void ExclusiveMin_ShouldDefaultToNull()
        {
            var result = builder.Build();

            Assert.NotNull(result);
            Assert.Equal(null, result.ExclusiveMinimum);
        }

        [Fact]
        public void ExclusiveMin_ShouldBeSettable()
        {
            var result = builder.IsExclusiveMinimum().Build();

            Assert.NotNull(result);
            Assert.Equal(true, result.ExclusiveMinimum);
        }

        [Fact]
        public void Format_ShouldDefaultToNull()
        {
            var result = builder.Build();

            Assert.NotNull(result);
            Assert.Equal(null, result.Format);
        }

        [Fact]
        public void Format_ShouldBeSettable()
        {
            var format = "mm-dd-yyyy";
            
            var result = builder.Format(format).Build();

            Assert.NotNull(result);
            Assert.Equal(format, result.Format);
        }

        [Fact]
        public void Items_ShouldDefaultToNull()
        {
            var result = builder.Build();

            Assert.NotNull(result);
            Assert.Equal(null, result.Items);
        }

        [Fact]
        public void Items_ShouldBeSettable()
        {
            var item = new Item();
            
            var result = builder.Items(item).Build();

            Assert.NotNull(result);
            Assert.Equal(item, result.Items);
        }

        [Fact]
        public void MaxItems_ShouldDefaultToNull()
        {
            var result = builder.Build();

            Assert.NotNull(result);
            Assert.Equal(null, result.MaxItems);
        }

        [Fact]
        public void MaxItems_ShouldBeSettable()
        {
            var maxItems = 2;
            
            var result = builder.MaxItems(maxItems).Build();

            Assert.NotNull(result);
            Assert.Equal(maxItems, result.MaxItems);
        }

        [Fact]
        public void MaxLength_ShouldDefaultToNull()
        {
            var result = builder.Build();

            Assert.NotNull(result);
            Assert.Equal(null, result.MaxLength);
        }

        [Fact]
        public void MaxLength_ShouldBeSettable()
        {
            var maxLength = 2;
            
            var result = builder.MaxLength(maxLength).Build();

            Assert.NotNull(result);
            Assert.Equal(maxLength, result.MaxLength);
        }

        [Fact]
        public void MinItems_ShouldDefaultToNull()
        {
            var result = builder.Build();

            Assert.NotNull(result);
            Assert.Equal(null, result.MinItems);
        }

        [Fact]
        public void MinItems_ShouldBeSettable()
        {
            var minItems = 2;
            
            var result = builder.MinItems(minItems).Build();

            Assert.NotNull(result);
            Assert.Equal(minItems, result.MinItems);
        }

        [Fact]
        public void MultipleOf_ShouldDefaultToNull()
        {
            var result = builder.Build();

            Assert.NotNull(result);
            Assert.Equal(null, result.MultipleOf);
        }

        [Fact]
        public void MultipleOf_ShouldBeSettable()
        {
            var multipleOf = 2;
            
            var result = builder.MultipleOf(multipleOf).Build();

            Assert.NotNull(result);
            Assert.Equal(multipleOf, result.MultipleOf);
        }

        [Fact]
        public void Pattern_ShouldDefaultToNull()
        {
            var result = builder.Build();

            Assert.NotNull(result);
            Assert.Equal(null, result.Pattern);
        }

        [Fact]
        public void Pattern_ShouldBeSettable()
        {
            var pattern = "mm-dd-yyyy";
            
            var result = builder.Pattern(pattern).Build();

            Assert.NotNull(result);
            Assert.Equal(pattern, result.Pattern);
        }

        [Fact]
        public void Reference_ShouldDefaultToNull()
        {
            var result = builder.Build();

            Assert.NotNull(result);
            Assert.Equal(null, result.Ref);
        }

        [Fact]
        public void Reference_ShouldBeSettable()
        {
            var reference = "#ref";
            
            var result = builder.Reference(reference).Build();

            Assert.NotNull(result);
            Assert.Equal(reference, result.Ref);
        }

        [Fact]
        public void UniqueItems_ShouldDefaultToNull()
        {
            var result = builder.Build();

            Assert.NotNull(result);
            Assert.Equal(null, result.UniqueItems);
        }

        [Fact]
        public void UniqueItems_ShouldBeSettable()
        {            
            var result = builder.IsUniqueItems().Build();

            Assert.NotNull(result);
            Assert.Equal(true, result.UniqueItems);
        }
    }
}
