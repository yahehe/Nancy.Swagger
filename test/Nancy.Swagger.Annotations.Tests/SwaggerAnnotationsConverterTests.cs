using ApprovalTests;
using ApprovalTests.Reporters;
using Nancy.Swagger.Annotations.Tests.Testdata;
using Nancy.Swagger.Annotations.Tests.Testdata.JsonNetEnricher;
using Nancy.Swagger.Modules;
using Nancy.Swagger.Services;
using Nancy.Testing;
using Xunit;

namespace Nancy.Swagger.Annotations.Tests
{
    [UseReporter(typeof(BeyondCompareReporter))]
    public class SwaggerAnnotationsConverterTests
    {
        private readonly Browser _browser;

        public SwaggerAnnotationsConverterTests()
        {
            var bootstrapper = new ConfigurableBootstrapper(with =>
            {
                with.ApplicationStartup((container, pipelines) =>
                    container.Register<ISwaggerMetadataConverter, SwaggerAnnotationsConverter>());

                with.Module<SwaggerModule>();
                with.Module<TestRoutesModule>();
                with.Module<JsonNetEnricherRoutesModule>();
            });

            _browser = new Browser(bootstrapper);
        }

        [Fact]
        public void Get_ApiDocsPath_ReturnsApiDeclaration()
        {
            ApproveJsonResponse(_browser.Get("/api-docs/api-docs"));
        }

        [Fact]
        public void Get_ApiDocsRootpath_ReturnsResourceListing()
        {
            ApproveJsonResponse(_browser.Get("/api-docs"));
        }

        [Fact]
        public void Get_TestModulePath_ReturnsApiDeclaration()
        {
            ApproveJsonResponse(_browser.Get("/api-docs/testroutes"));
        }

        [Fact]
        public void Get_JsonNetEnricherModulePath_ReturnsApiDeclaration()
        {
            ApproveJsonResponse(_browser.Get("/api-docs/json-net-enricher-routes"));
        }

        private static void ApproveJsonResponse(BrowserResponse response)
        {
            Approvals.VerifyJson(response.Body.AsString());
        }
    }
}