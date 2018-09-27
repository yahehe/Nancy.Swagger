using Xunit;

namespace Nancy.Swagger.Tests
{
    public class SwaggerTypeMappingTests
    {
        class OriginalType { }

        class AnotherOriginalType { }

        class MappedType { }

        [Fact]
        public void ShouldAdd_AndGet_TypeMappings()
        {
            SwaggerTypeMapping.ResetMappedTypes();

            Assert.False(SwaggerTypeMapping.IsMappedType(typeof(OriginalType)));

            SwaggerTypeMapping.AddTypeMapping(typeof(OriginalType), typeof(MappedType));

            Assert.True(SwaggerTypeMapping.IsMappedType(typeof(OriginalType)));
            Assert.Equal(typeof(MappedType), SwaggerTypeMapping.GetMappedType(typeof(OriginalType)));
        }

        [Fact]
        public void ShouldAdd_AndGet_NestedTypeMappings()
        {
            SwaggerTypeMapping.ResetMappedTypes();

            Assert.False(SwaggerTypeMapping.IsMappedType(typeof(OriginalType)));

            SwaggerTypeMapping.AddTypeMapping(typeof(AnotherOriginalType), typeof(OriginalType));
            SwaggerTypeMapping.AddTypeMapping(typeof(OriginalType), typeof(MappedType));

            Assert.True(SwaggerTypeMapping.IsMappedType(typeof(AnotherOriginalType)));
            Assert.True(SwaggerTypeMapping.IsMappedType(typeof(OriginalType)));

            Assert.Equal(typeof(MappedType), SwaggerTypeMapping.GetMappedType(typeof(AnotherOriginalType)));
        }
    }
}