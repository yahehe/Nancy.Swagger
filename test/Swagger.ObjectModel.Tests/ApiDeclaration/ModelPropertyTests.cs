using System.Collections.Generic;
using Should;
using Swagger.ObjectModel.ApiDeclaration;
using Xunit;

namespace Swagger.ObjectModel.Tests.ApiDeclaration
{
    public class ModelPropertyTests
    {
        [Fact]
        public void ToJson_ModelWithSingleProperty_ReturnsJsonString()
        {
            new Model
                {
                    Id = "some-model", 
                    Properties = new Dictionary<string, ModelProperty>
                        {
                            { "some-type", new ModelProperty { Type = "some-type" } }
                        }
                }.ToJson()
                .ShouldEqual("{\"id\":\"some-model\",\"properties\":{\"some-type\":{\"type\":\"some-type\"}}}");
        }
    }
}