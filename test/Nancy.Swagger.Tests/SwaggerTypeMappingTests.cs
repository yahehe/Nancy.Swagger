using Should;
using Xunit;

namespace Nancy.Swagger.Tests
{
    public class SwaggerTypeMappingTests
    {
        [Fact]
        public void ShouldAdd_AndGet_TypeMappings()
        {
            SwaggerTypeMapping.ResetMappedTypes();

            Assert.False(SwaggerTypeMapping.IsMappedType(typeof(long)));

            SwaggerTypeMapping.AddTypeMapping(typeof(long), typeof(double));

            Assert.True(SwaggerTypeMapping.IsMappedType(typeof(long)));
            Assert.Equal(typeof(double), SwaggerTypeMapping.GetMappedType(typeof(long)));
        }

        [Fact]
        public void ShouldAdd_AndGet_NestedTypeMappings()
        {
            SwaggerTypeMapping.ResetMappedTypes();

            Assert.False(SwaggerTypeMapping.IsMappedType(typeof(long)));

            SwaggerTypeMapping.AddTypeMapping(typeof(int), typeof(long));
            SwaggerTypeMapping.AddTypeMapping(typeof(long), typeof(double));

            Assert.True(SwaggerTypeMapping.IsMappedType(typeof(int)));
            Assert.True(SwaggerTypeMapping.IsMappedType(typeof(long)));

            Assert.Equal(typeof(double), SwaggerTypeMapping.GetMappedType(typeof(int)));
        }
    }
}