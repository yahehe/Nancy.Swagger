using Swagger.ObjectModel.Builders;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Swagger.ObjectModel.Tests.Builders
{
    public class PathItemBuilderTest
    {
        [Fact]
        public void Should_AbleToSetOperation()
        {
            var operationBuilder = new OperationBuilder().Response(r => r.Description("N/A"));

            var pathItem = new PathItemBuilder(HttpMethod.Get).Operation(operationBuilder).Build();

            Assert.Equal(operationBuilder.Build().Responses, pathItem.Get.Responses);
        }

        [Fact]
        public void Should_AbleToSetOperationWithAction()
        {
            var pathItem = new PathItemBuilder(HttpMethod.Get)
                                    .Operation(x => x.Response(r => r.Description("N/A")))
                                    .Build();

            Assert.Equal("N/A", pathItem.Get.Responses["default"].Description);
        }

        [Fact]
        public void Should_AbleToSetOneParameter()
        {
            var parameter = new Parameter();

            var pathItem = new PathItemBuilder(HttpMethod.Get)
                                    .Parameter(parameter)
                                    .Build();

            Assert.Single(pathItem.Parameters);
            Assert.Equal(parameter, pathItem.Parameters.FirstOrDefault());
        }

        [Fact]
        public void Should_AbleToSetOneParameterWithBuilder()
        {
            var pBuilder = new ParameterBuilder().Name("name").In(ParameterIn.Query);

            var pathItem = new PathItemBuilder(HttpMethod.Get)
                                    .Parameter(pBuilder)
                                    .Build();

            Assert.Single(pathItem.Parameters);
            Assert.Equal(pBuilder.Build().Name, pathItem.Parameters.FirstOrDefault().Name);
            Assert.Equal(pBuilder.Build().In, pathItem.Parameters.FirstOrDefault().In);
        }

        [Fact]
        public void Should_AbleToSetMultiParameters()
        {
            var parameters = new List<Parameter>();
            parameters.Add(new Parameter { Name = "first parameter" });
            parameters.Add(new Parameter { Name = "second parameter" });

            var pathItem = new PathItemBuilder(HttpMethod.Get)
                                    .Parameters(parameters)
                                    .Build();

            Assert.Equal(parameters.Count, pathItem.Parameters.Count());
            Assert.Equal(parameters, pathItem.Parameters);
        }

        [Fact]
        public void Should_SetOpeationToGet_WhenHttpMethodIsGet()
        {
            var pathItem = new PathItemBuilder(HttpMethod.Get).Build();

            Assert.NotNull(pathItem.Get);
            Assert.Null(pathItem.Post);
            Assert.Null(pathItem.Put);
            Assert.Null(pathItem.Delete);
            Assert.Null(pathItem.Head);
            Assert.Null(pathItem.Options);
            Assert.Null(pathItem.Patch);
        }

        [Fact]
        public void Should_SetOpeationToPost_WhenHttpMethodIsPost()
        {
            var pathItem = new PathItemBuilder(HttpMethod.Post).Build();

            Assert.NotNull(pathItem.Post);
            Assert.Null(pathItem.Get);
            Assert.Null(pathItem.Put);
            Assert.Null(pathItem.Delete);
            Assert.Null(pathItem.Head);
            Assert.Null(pathItem.Options);
            Assert.Null(pathItem.Patch);
        }

        [Fact]
        public void Should_SetOpeationToPut_WhenHttpMethodIsPut()
        {
            var pathItem = new PathItemBuilder(HttpMethod.Put).Build();

            Assert.NotNull(pathItem.Put);
            Assert.Null(pathItem.Get);
            Assert.Null(pathItem.Post);
            Assert.Null(pathItem.Delete);
            Assert.Null(pathItem.Head);
            Assert.Null(pathItem.Options);
            Assert.Null(pathItem.Patch);
        }

        [Fact]
        public void Should_SetOpeationToDelete_WhenHttpMethodIsDelete()
        {
            var pathItem = new PathItemBuilder(HttpMethod.Delete).Build();

            Assert.NotNull(pathItem.Delete);
            Assert.Null(pathItem.Get);
            Assert.Null(pathItem.Post);
            Assert.Null(pathItem.Put);
            Assert.Null(pathItem.Head);
            Assert.Null(pathItem.Options);
            Assert.Null(pathItem.Patch);
        }

        [Fact]
        public void Should_SetOpeationToHead_WhenHttpMethodIsHead()
        {
            var pathItem = new PathItemBuilder(HttpMethod.Head).Build();

            Assert.NotNull(pathItem.Head);
            Assert.Null(pathItem.Get);
            Assert.Null(pathItem.Post);
            Assert.Null(pathItem.Put);
            Assert.Null(pathItem.Delete);
            Assert.Null(pathItem.Options);
            Assert.Null(pathItem.Patch);
        }

        [Fact]
        public void Should_SetOpeationToOptions_WhenHttpMethodIsOptions()
        {
            var pathItem = new PathItemBuilder(HttpMethod.Options).Build();

            Assert.NotNull(pathItem.Options);
            Assert.Null(pathItem.Get);
            Assert.Null(pathItem.Post);
            Assert.Null(pathItem.Put);
            Assert.Null(pathItem.Delete);
            Assert.Null(pathItem.Head);
            Assert.Null(pathItem.Patch);
        }

        [Fact]
        public void Should_SetOpeationToPatch_WhenHttpMethodIsPatch()
        {
            var pathItem = new PathItemBuilder(HttpMethod.Patch).Build();

            Assert.NotNull(pathItem.Patch);
            Assert.Null(pathItem.Get);
            Assert.Null(pathItem.Post);
            Assert.Null(pathItem.Put);
            Assert.Null(pathItem.Delete);
            Assert.Null(pathItem.Head);
            Assert.Null(pathItem.Options);
        }
    }
}
