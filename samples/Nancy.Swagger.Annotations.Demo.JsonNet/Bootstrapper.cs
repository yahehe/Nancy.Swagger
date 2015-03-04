using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.Swagger.Services;
using Nancy.TinyIoc;

namespace Nancy.Swagger.Annotations.Demo.JsonNet
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            // Register our custom JsonNetAnnotationsProvider, this overrules the default metadata provder
            container.Register<ISwaggerMetadataProvider, JsonNetAnnotationsProvider>();
        }

        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            nancyConventions.StaticContentsConventions.AddDirectory("docs", "swagger-ui");
        }
    }
}