using Nancy.Conventions;
using Nancy.Swagger.Services;
using Swagger.ObjectModel;

namespace Nancy.Swagger.Demo
{
    using Nancy.Bootstrapper;
    using Nancy.TinyIoc;

    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            SwaggerMetadataProvider.SetInfo("Nancy Swagger Example", "v0", "Our awesome service", new Contact()
            {
                EmailAddress = "exampleEmail@example.com"
            });
            base.ApplicationStartup(container, pipelines);
        }

        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            nancyConventions.StaticContentsConventions.AddDirectory("docs", "swagger-ui");
        }

        /// <summary>
        /// Initialise the request - can be used for adding pre/post hooks and
        ///             any other per-request initialisation tasks that aren't specifically container setup
        ///             related
        /// </summary>
        /// <param name="container">Container</param><param name="pipelines">Current pipelines</param><param name="context">Current context</param>
        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {
            pipelines.AfterRequest.AddItemToEndOfPipeline(x => x.Response.Headers.Add("Access-Control-Allow-Origin", "*"));
        }
    }
}