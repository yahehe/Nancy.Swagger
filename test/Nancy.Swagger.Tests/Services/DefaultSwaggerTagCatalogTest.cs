using FakeItEasy;
using Nancy.Swagger.Services;
using Swagger.ObjectModel;
using System.Collections.Generic;
using Xunit;

namespace Nancy.Swagger.Tests.Services
{
    public class DefaultSwaggerTagCatalogTest
    {
        private DefaultSwaggerTagCatalog _defaultSwaggerTagCatalog;

        public DefaultSwaggerTagCatalogTest()
        {
            var swaggerTagProvider = A.Fake<IEnumerable<ISwaggerTagProvider>>();

            _defaultSwaggerTagCatalog = new DefaultSwaggerTagCatalog(swaggerTagProvider);
        }

        [Fact]
        public void Add_Single_Tag_Should_Succeed()
        {
            var tag = new Tag() { Name = "tag" };

            _defaultSwaggerTagCatalog.AddTag(tag);

            Assert.Contains(tag, _defaultSwaggerTagCatalog);
        }

        [Fact]
        public void Add_Multi_Tags_With_Same_Name_Should_Be_Single()
        {
            var tag1 = new Tag() { Name = "tag" };
            var tag2 = new Tag() { Name = "tag" };

            _defaultSwaggerTagCatalog.AddTag(tag1);
            _defaultSwaggerTagCatalog.AddTag(tag2);

            Assert.Single(_defaultSwaggerTagCatalog);
        }

        [Fact]
        public void Add_Multi_Tags_With_Different_Name_Should_Not_Be_Single()
        {
            var tag1 = new Tag() { Name = "tag1" };
            var tag2 = new Tag() { Name = "tag2" };

            _defaultSwaggerTagCatalog.AddTag(tag1);
            _defaultSwaggerTagCatalog.AddTag(tag2);
            
            Assert.Equal(2, _defaultSwaggerTagCatalog.Count);
        }
    }
}
