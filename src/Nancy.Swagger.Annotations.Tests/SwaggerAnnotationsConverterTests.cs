using ApprovalTests;
using ApprovalTests.Reporters;
using Nancy.Bootstrapper;
using Nancy.Swagger.Modules;
using Nancy.Swagger.Services;
using Nancy.Testing;
using Nancy.TinyIoc;
using System;
using System.Linq;
using Xunit;

namespace Nancy.Swagger.Annotations.Tests
{
    [UseReporter(typeof(XUnitReporter))]
    public class SwaggerAnnotationsConverterTests
    {
        #region Helper Methods

        private static void ApproveJson(string json)
        {
            Approvals.Verify(FormatJson(json));
        }

        private static void ApproveJsonResponse(BrowserResponse response)
        {
            ApproveJson(response.Body.AsString());
        }

        private static string FormatJson(string json)
        {
            string INDENT_STRING = "    ";

            int indentation = 0;
            int quoteCount = 0;
            var result =
                from ch in json
                let quotes = ch == '"' ? quoteCount++ : quoteCount
                let lineBreak = ch == ',' && quotes % 2 == 0 ? ch + Environment.NewLine + String.Concat(Enumerable.Repeat(INDENT_STRING, indentation)) : null
                let openChar = ch == '{' || ch == '[' ? ch + Environment.NewLine + String.Concat(Enumerable.Repeat(INDENT_STRING, ++indentation)) : ch.ToString()
                let closeChar = ch == '}' || ch == ']' ? Environment.NewLine + String.Concat(Enumerable.Repeat(INDENT_STRING, --indentation)) + ch : ch.ToString()
                select lineBreak == null ? openChar.Length > 1 ? openChar : closeChar : lineBreak;

            return String.Concat(result);
        }

        #endregion Helper Methods

        private Browser _browser;

        public SwaggerAnnotationsConverterTests()
        {
            var bootstrapper = new ConfigurableBootstrapper(with =>
            {
                with.ApplicationStartup((TinyIoCContainer container, IPipelines pipelines) =>
                {
                    container.Register<ISwaggerMetadataConverter, SwaggerAnnotationsConverter>();
                });

                with.Module<SwaggerModule>();
                with.Module<TestRoutesModule>();
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
    }
}