using Swagger.ObjectModel.Builders;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Swagger.ObjectModel.Tests.Builders
{
    public class OperationBuilderTest
    {
        private OperationBuilder builder;
        private string response_description = string.Empty;

        public OperationBuilderTest()
        {
            this.builder = new OperationBuilder();
            this.response_description = "N/A";
        }

        private OperationBuilder GetBasicBuilderWithResponse()
        {
            return new OperationBuilder().Response(x => x.Description(response_description));
        }

        [Fact]
        public void Should_ThrowRequiredFieldException_WhenResponsesIsNotSet()
        {
            Assert.Throws(typeof(RequiredFieldException), () => new OperationBuilder().Build());
        }
      
        [Fact]
        public void Should_AbleToSetResponseWithStringAndAction()
        {
            string httpStatusCode = "200";

            var operation = new OperationBuilder().Response(httpStatusCode, x => x.Description(response_description)).Build();

            Assert.NotNull(operation.Responses);
            Assert.Equal(1, operation.Responses.Count);
            Assert.Equal(response_description, operation.Responses[httpStatusCode].Description);
        }

        [Fact]
        public void Should_AbleToSetResponseWithInt32AndAction()
        {
            int httpStatusCode = 200;

            var operation = new OperationBuilder().Response(httpStatusCode, x => x.Description(response_description)).Build();

            Assert.NotNull(operation.Responses);
            Assert.Equal(1, operation.Responses.Count);
            Assert.Equal(response_description, operation.Responses[httpStatusCode.ToString()].Description);
        }

        [Fact]
        public void Should_AbleToSetResponseWithEnumAndAction()
        {
            var httpStatusCode = System.Net.HttpStatusCode.OK;

            var operation = new OperationBuilder().Response(httpStatusCode, x => x.Description(response_description)).Build();

            Assert.NotNull(operation.Responses);
            Assert.Equal(1, operation.Responses.Count);
            Assert.Equal(response_description, operation.Responses[httpStatusCode.ToString()].Description);
        }

        [Fact]
        public void Should_AbleToSetResponseWithAction()
        {
            var operation = new OperationBuilder().Response(x => x.Description(response_description)).Build();

            Assert.NotNull(operation.Responses);
            Assert.True(operation.Responses.ContainsKey("default"));
            Assert.Equal(response_description, operation.Responses["default"].Description);
        }

        [Fact]
        public void Should_AbleToSetTag()
        {
            string tag = "tag";

            var operation = GetBasicBuilderWithResponse().Tag(tag).Build();

            Assert.NotNull(operation.Tags);
            Assert.Equal(tag, operation.Tags.FirstOrDefault());
        }

        [Fact]
        public void Should_AbleToSetTags()
        {
            var tags = new List<string>() { "tag1", "tag2" };

            var operation = GetBasicBuilderWithResponse().Tags(tags).Build();

            Assert.NotNull(operation.Tags);
            Assert.Equal(tags, operation.Tags);
        }

        [Fact]
        public void Should_AbleToSetSummary()
        {
            string summary = "summary";

            var operation = GetBasicBuilderWithResponse().Summary(summary).Build();

            Assert.NotNull(operation.Summary);
            Assert.Equal(summary, operation.Summary);
        }

        [Fact]
        public void Should_AbleToSetDescription()
        {
            string description = "description";

            var operation = GetBasicBuilderWithResponse().Description(description).Build();

            Assert.NotNull(operation.Description);
            Assert.Equal(description, operation.Description);
        }

        [Fact]
        public void Should_AbleToSetExternalDocumentation()
        {
            var externalDocumentation = new ExternalDocumentation() { Url = "https://github.com/yahehe/Nancy.Swagger" };

            var operation = GetBasicBuilderWithResponse().ExternalDocumentation(externalDocumentation).Build();

            Assert.NotNull(operation.ExternalDocumentation);
            Assert.Equal(externalDocumentation, operation.ExternalDocumentation);
        }

        [Fact]
        public void Should_AbleToSetExternalDocumentationWithBuilder()
        {
            var externalDocumentationBuilder = new ExternalDocumentationBuilder().Url("https://github.com/yahehe/Nancy.Swagger");

            var operation = GetBasicBuilderWithResponse().ExternalDocumentation(externalDocumentationBuilder).Build();

            Assert.NotNull(operation.ExternalDocumentation);
            Assert.Equal(externalDocumentationBuilder.Build().Url, operation.ExternalDocumentation.Url);
        }

        [Fact]
        public void Should_AbleToSetOperationId()
        {
            string operationId = "OperationId";

            var operation = GetBasicBuilderWithResponse().OperationId(operationId).Build();

            Assert.NotNull(operation.OperationId);
            Assert.Equal(operationId, operation.OperationId);
        }

        [Fact]
        public void Should_AbleToSetConsumeMimeType()
        {
            string mimeType = "application/json";

            var operation = GetBasicBuilderWithResponse().ConsumeMimeType(mimeType).Build();

            Assert.NotNull(operation.Consumes);
            Assert.True(operation.Consumes.Contains(mimeType));
        }

        [Fact]
        public void Should_AbleToSetConsumeMimeTypes()
        {
            var mimeTypes = new List<string>() { "application/json", "application/xml" };

            var operation = GetBasicBuilderWithResponse().ConsumeMimeTypes(mimeTypes).Build();

            Assert.NotNull(operation.Consumes);
            Assert.Equal(mimeTypes, operation.Consumes);
        }

        [Fact]
        public void Should_AbleToSetProduceMimeType()
        {
            string mimeType = "application/json";

            var operation = GetBasicBuilderWithResponse().ProduceMimeType(mimeType).Build();

            Assert.NotNull(operation.Produces);
            Assert.True(operation.Produces.Contains(mimeType));
        }

        [Fact]
        public void Should_AbleToSetProduceMimeTypes()
        {
            var mimeTypes = new List<string>() { "application/json", "application/xml" };

            var operation = GetBasicBuilderWithResponse().ProduceMimeTypes(mimeTypes).Build();

            Assert.NotNull(operation.Produces);
            Assert.Equal(mimeTypes, operation.Produces);
        }

        [Fact]
        public void Should_AbleToSetParameter()
        {
            var parameter = new Parameter();

            var operation = GetBasicBuilderWithResponse().Parameter(parameter).Build();

            Assert.NotNull(operation.Parameters);
            Assert.True(operation.Parameters.Contains(parameter));
        }

        [Fact]
        public void Should_AbleToSetParameterWithAction()
        {
            var operation = GetBasicBuilderWithResponse()
                                .Parameter(x => x.Name("name").In(ParameterIn.Query))
                                .Build();

            Assert.NotNull(operation.Parameters);
            Assert.NotNull(operation.Parameters.Where(x => x.In == ParameterIn.Query && x.Name == "name"));
        }

        [Fact]
        public void Should_AbleToSetParameters()
        {
            var parameters = new List<Parameter>();
            parameters.Add(new Parameter { Name = "name1", In = ParameterIn.Query });
            parameters.Add(new Parameter { Name = "name2", In = ParameterIn.Query });

            var operation = GetBasicBuilderWithResponse().Parameters(parameters).Build();

            Assert.NotNull(operation.Parameters);
            Assert.Equal(parameters, operation.Parameters);
        }

        [Fact]
        public void Should_AbleToSetBodyParameter()
        {
            var operation = GetBasicBuilderWithResponse()
                                    .BodyParameter(x => x.Name("name").Schema(new Schema()))
                                    .Build();

            Assert.NotNull(operation.Parameters);            
        }

        [Fact]
        public void Should_AbleToSetScheme()
        {
            var operation = GetBasicBuilderWithResponse().Scheme(Schemes.Http).Build();

            Assert.NotNull(operation.Schemes);
            Assert.True(operation.Schemes.Contains(Schemes.Http));
        }

        [Fact]
        public void Should_AbleToSetIsDeprecated()
        {
            var operation = GetBasicBuilderWithResponse().IsDeprecated().Build();
            
            Assert.True(operation.Deprecated);
        }

        [Fact]
        public void Should_AbleToSetSecurityRequirementWithKeyValuePair()
        {
            var security = new KeyValuePair<SecuritySchemes, IEnumerable<string>>(SecuritySchemes.ApiKey, new List<string> { "string" });
                       
            var operation = GetBasicBuilderWithResponse().SecurityRequirement(security).Build();

            Assert.NotNull(operation.SecurityRequirements);
            Assert.True(operation.SecurityRequirements.Contains(security));
        }

        [Fact]
        public void Should_AbleToSetSecurityRequirementWithBuilder()
        {
            var security = new SecurityRequirementBuilder().SecurityScheme(SecuritySchemes.ApiKey);

            var operation = GetBasicBuilderWithResponse().SecurityRequirement(security).Build();

            Assert.NotNull(operation.SecurityRequirements);
            Assert.True(operation.SecurityRequirements.ContainsKey(SecuritySchemes.ApiKey));
        }

        [Fact]
        public void Should_AbleToSetSecurityRequirement()
        {
            var security = SecuritySchemes.ApiKey;

            var operation = GetBasicBuilderWithResponse().SecurityRequirement(security).Build();

            Assert.NotNull(operation.SecurityRequirements);
            Assert.True(operation.SecurityRequirements.ContainsKey(SecuritySchemes.ApiKey));
        }
    }
}
