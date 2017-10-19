using Swagger.ObjectModel.Builders;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Swagger.ObjectModel.Tests.Builders
{
    public class SwaggerRootBuilderTest
    {
        private SwaggerRootBuilder builder;

        public SwaggerRootBuilderTest()
        {
            this.builder = new SwaggerRootBuilder();
        }

        private SwaggerRootBuilder GetBasicSwaggerRootBuilder()
        {
            var info = new Info { Version = "1.0" };
            var pathItem = new PathItem { Get = new Operation() };
            return builder.Info(info).Path("/endpoint", pathItem);
        }

        [Fact]
        public void Should_ThrowRequiredFieldException_WhenNotSetAnythings()
        {
            Assert.Throws<RequiredFieldException>(() => builder.Build());
        }

        [Fact]
        public void Should_ThrowRequiredFieldException_WhenInfoIsNull()
        {
            Info info = null;

            Assert.Throws<RequiredFieldException>(() => builder.Info(info).Build());
        }

        [Fact]
        public void Should_AbleToSetInfo()
        {
            var info = new Info { Version = "1.0" };
            var pathItem = new PathItem { Get = new Operation() };

            var swaggerRoot = builder.Info(info)
                                     .Path("/endpoint", pathItem)
                                     .Build();

            Assert.Equal(info, swaggerRoot.Info);
        }

        [Fact]
        public void Should_AbleToSetPath()
        {
            var info = new Info { Version = "1.0" };
            var pathItem = new PathItem { Get = new Operation() };

            var swaggerRoot = builder.Info(info)
                                     .Path("/endpoint", pathItem)
                                     .Build();

            Assert.True(swaggerRoot.Paths.Keys.Contains("/endpoint"));
            Assert.Equal(pathItem, swaggerRoot.Paths["/endpoint"]);
        }

        [Fact]
        public void Should_AbleToSetPathWithBuilder()
        {
            var info = new Info { Version = "1.0" };
            var pathItem = new PathItemBuilder(HttpMethod.Get);

            var swaggerRoot = builder.Info(info)
                                     .Path("/endpoint", pathItem)
                                     .Build();

            Assert.Equal(pathItem.Build().Get, swaggerRoot.Paths["/endpoint"].Get);
        }

        [Fact]
        public void Should_AbleToSetPathWithEndPointName()
        {
            var info = new Info { Version = "1.0" };

            var swaggerRoot = builder.Info(info)
                                     .Path("/endpoint")
                                     .Build();

            Assert.Same(new PathItem().Parameters, swaggerRoot.Paths["/endpoint"].Parameters);
        }

        [Fact]
        public void Should_StartWithSlash_WhenEndPointNameNotStartWithSlash()
        {
            var info = new Info { Version = "1.0" };
            var pathItem = new PathItem { Get = new Operation() };

            var swaggerRoot = builder.Info(info)
                                     .Path("endpoint", pathItem)
                                     .Build();

            Assert.True(swaggerRoot.Paths.Keys.Contains("/endpoint"));
        }

        [Fact]
        public void Should_AbleToSetHost()
        {
            string host = "host";

            var swaggerRoot = GetBasicSwaggerRootBuilder().Host(host).Build();

            Assert.Equal(host, swaggerRoot.Host);
        }

        [Fact]
        public void Should_AbleToSetBasePath()
        {
            string basePath = "/basePath";

            var swaggerRoot = GetBasicSwaggerRootBuilder().BasePath(basePath).Build();

            Assert.Equal(basePath, swaggerRoot.BasePath);
        }

        [Fact]
        public void Should_AbleToSetBasePathWhenBasePathNotStartWithSlash()
        {
            string basePath = "basePath";

            var swaggerRoot = GetBasicSwaggerRootBuilder().BasePath(basePath).Build();

            Assert.Equal(string.Concat("/", basePath), swaggerRoot.BasePath);
        }

        [Fact]
        public void Should_AbleToSetScheme()
        {
            var swaggerRoot = GetBasicSwaggerRootBuilder().Scheme(Schemes.Http).Build();

            Assert.Contains(Schemes.Http, swaggerRoot.Schemes);
        }

        [Fact]
        public void Should_AbleToSetConsumeMimeType()
        {
            string mimeType = "application/json";

            var swaggerRoot = GetBasicSwaggerRootBuilder().ConsumeMimeType(mimeType).Build();

            Assert.Contains(mimeType, swaggerRoot.Consumes);
        }

        [Fact]
        public void Should_AbleToSetConsumeMimeTypes()
        {
            var mimeTypes = new List<string>() { "application/json", "application/xml" };

            var swaggerRoot = GetBasicSwaggerRootBuilder().ConsumeMimeTypes(mimeTypes).Build();

            Assert.Equal(mimeTypes, swaggerRoot.Consumes);
        }

        [Fact]
        public void Should_AbleToSetProduceMimeType()
        {
            string mimeType = "application/json";

            var swaggerRoot = GetBasicSwaggerRootBuilder().ProduceMimeType(mimeType).Build();

            Assert.Contains(mimeType, swaggerRoot.Produces);
        }

        [Fact]
        public void Should_AbleToSetProduceMimeTypes()
        {
            var mimeTypes = new List<string>() { "application/json", "application/xml" };

            var swaggerRoot = GetBasicSwaggerRootBuilder().ProduceMimeTypes(mimeTypes).Build();

            Assert.Equal(mimeTypes, swaggerRoot.Produces);
        }

        [Fact]
        public void Should_AbleToSetParameter()
        {
            var parameter = new Parameter() { Name = "para", In = ParameterIn.Query };

            var swaggerRoot = GetBasicSwaggerRootBuilder().Parameter(parameter).Build();

            Assert.True(swaggerRoot.Parameters.ContainsKey(parameter.Name));
        }

        [Fact]
        public void Should_AbleToSetParameterWithBuilder()
        {
            var parameter = new ParameterBuilder().Name("para").In(ParameterIn.Query);

            var swaggerRoot = GetBasicSwaggerRootBuilder().Parameter(parameter).Build();

            Assert.True(swaggerRoot.Parameters.ContainsKey(parameter.Build().Name));
        }

        [Fact]
        public void Should_AbleToSetBodyParameter()
        {
            var parameter = new BodyParameterBuilder().Name("para").Schema(new Schema());

            var swaggerRoot = GetBasicSwaggerRootBuilder().Parameter(parameter).Build();

            Assert.True(swaggerRoot.Parameters.ContainsKey(parameter.Build().Name));
        }

        [Fact]
        public void Should_AbleToSetResponse()
        {
            var response = new Response() { Description = "desc" };

            var swaggerRoot = GetBasicSwaggerRootBuilder().Response("name", response).Build();

            Assert.True(swaggerRoot.Responses.ContainsKey("name"));
            Assert.Equal(response, swaggerRoot.Responses["name"]);
        }

        [Fact]
        public void Should_AbleToSetResponseWithBuilder()
        {
            var response = new ResponseBuilder().Description("desc");

            var swaggerRoot = GetBasicSwaggerRootBuilder().Response("name", response).Build();

            Assert.True(swaggerRoot.Responses.ContainsKey("name"));
            Assert.Equal(response.Build().Description, swaggerRoot.Responses["name"].Description);
        }

        [Fact]
        public void Should_AbleToSetSecurityDefinition()
        {
            var securityScheme = new SecurityScheme();

            var swaggerRoot = GetBasicSwaggerRootBuilder().SecurityDefinition("name", securityScheme).Build();

            Assert.True(swaggerRoot.SecurityDefinitions.ContainsKey("name"));
            Assert.Equal(securityScheme, swaggerRoot.SecurityDefinitions["name"]);
        }

        [Fact]
        public void Should_AbleToSetSecurityRequirementWithBuilder()
        {
            var srBuilder = new SecurityRequirementBuilder().SecurityScheme(SecuritySchemes.ApiKey);

            var swaggerRoot = GetBasicSwaggerRootBuilder().SecurityRequirement(srBuilder).Build();

            Assert.True(swaggerRoot.Security.ContainsKey(SecuritySchemes.ApiKey));
            Assert.Equal(srBuilder.Build().Value, swaggerRoot.Security[SecuritySchemes.ApiKey]);
        }

        [Fact]
        public void Should_AbleToSetSecurityRequirement()
        {
            var swaggerRoot = GetBasicSwaggerRootBuilder().SecurityRequirement(SecuritySchemes.ApiKey).Build();

            Assert.True(swaggerRoot.Security.ContainsKey(SecuritySchemes.ApiKey));
            Assert.Empty(swaggerRoot.Security[SecuritySchemes.ApiKey]);
        }

        [Fact]
        public void Should_AbleToSetExternalDocumentation()
        {
            var externalDocumentation = new ExternalDocumentation() { Url = "url" };

            var swaggerRoot = GetBasicSwaggerRootBuilder().ExternalDocumentation(externalDocumentation).Build();

            Assert.Equal(externalDocumentation, swaggerRoot.ExternalDocumentation);
        }

        [Fact]
        public void Should_AbleToSetExternalDocumentationWithBuilder()
        {
            var edBuilder = new ExternalDocumentationBuilder().Url("url");

            var swaggerRoot = GetBasicSwaggerRootBuilder().ExternalDocumentation(edBuilder).Build();

            Assert.Equal(edBuilder.Build().Url, swaggerRoot.ExternalDocumentation.Url);
        }

        [Fact]
        public void Should_AbleToSetTag()
        {
            var tag = new Tag() { Name = "name" };

            var swaggerRoot = GetBasicSwaggerRootBuilder().Tag(tag).Build();

            Assert.Contains(tag, swaggerRoot.Tags);
        }

        [Fact]
        public void Should_AbleToSetTagWithBuilder()
        {
            var tag = new TagBuilder().Name("name");

            var swaggerRoot = GetBasicSwaggerRootBuilder().Tag(tag).Build();

            Assert.Equal(tag.Build().Name, swaggerRoot.Tags.FirstOrDefault().Name);
        }

        [Fact]
        public void Should_AbleToSetDefinition()
        {
            string name = "name";
            var schema = new Schema();

            var swaggerRoot = GetBasicSwaggerRootBuilder().Definition(name, schema).Build();

            Assert.Equal(schema, swaggerRoot.Definitions[name]);
        }

        [Fact]
        public void Should_AbleToSetDefinitionWithBuilder()
        {
            string name = "name";
            var schema = new SchemaBuilder<int>();

            var swaggerRoot = GetBasicSwaggerRootBuilder().Definition(name, schema).Build();

            Assert.Equal(schema.Build(), swaggerRoot.Definitions[name]);
        }
    }
}
