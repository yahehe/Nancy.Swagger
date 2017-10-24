using FakeItEasy;
using Nancy.Swagger.Services;
using Swagger.ObjectModel;
using System;
using System.Collections.Generic;
using Xunit;

namespace Nancy.Swagger.Tests.Services
{
    public class DefaultSwaggerModelCatalogTest
    {
        private DefaultSwaggerModelCatalog _defaultSwaggerModelCatalog;

        public DefaultSwaggerModelCatalogTest()
        {
            var fakeDataProviders = A.Fake<IEnumerable<ISwaggerModelDataProvider>>();

            this._defaultSwaggerModelCatalog = new DefaultSwaggerModelCatalog(fakeDataProviders);
        }  

        [Theory]
        [InlineData(typeof(int))]        
        [InlineData(typeof(float))]
        [InlineData(typeof(string))]
        [InlineData(typeof(SwaggerFile))]
        public void GetModelForType_Should_Return_Null_When_Type_Is_Primitive(Type type)
        {
            var swaggerModelData = _defaultSwaggerModelCatalog.GetModelForType(type);

            Assert.Null(swaggerModelData);
        }

        [Fact]        
        public void GetModelForType_Should_Return_SwaggerModelData_When_Type_Is_Not_Primitive()
        {
            var swaggerModelData = _defaultSwaggerModelCatalog.GetModelForType(typeof(TestModel));

            Assert.NotNull(swaggerModelData);
        }

        [Fact]        
        public void GetModelForType_Should_Return_Null_When_addIfNotSet_Is_False()
        {
            var swaggerModelData = _defaultSwaggerModelCatalog.GetModelForType(typeof(TestModel), false);

            Assert.Null(swaggerModelData);
        }

        [Fact]        
        public void GetModelForType_Of_T_Should_Return_SwaggerModelData_When_addIfNotSet_Is_Default()
        {            
            var swaggerModelData = _defaultSwaggerModelCatalog.GetModelForType<TestModel>();

            Assert.NotNull(swaggerModelData);
            Assert.Contains(swaggerModelData, _defaultSwaggerModelCatalog);
        }

        [Fact]        
        public void GetModelForType_Of_T_Should_Return_Null_When_addIfNotSet_Is_False()
        {
            var swaggerModelData = _defaultSwaggerModelCatalog.GetModelForType<TestModel>(false);

            Assert.Null(swaggerModelData);            
        }

        [Fact]        
        public void AddModel_Of_T_Should_Return_SwaggerModelData()
        {
            var swaggerModelData = _defaultSwaggerModelCatalog.AddModel<TestModel>();

            Assert.NotNull(swaggerModelData);
            Assert.Contains(swaggerModelData, _defaultSwaggerModelCatalog);
        }

        [Fact]
        public void AddModels_Should_Succeed()
        {
            var types = new List<Type>() { typeof(int) , typeof(TestModel) };

            _defaultSwaggerModelCatalog.AddModels(types.ToArray());

            Assert.Equal(2, _defaultSwaggerModelCatalog.Count);            
        }
    }
}
