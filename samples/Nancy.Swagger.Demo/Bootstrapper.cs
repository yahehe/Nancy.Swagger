namespace Nancy.Swagger.Demo
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoc.TinyIoCContainer container, Nancy.Bootstrapper.IPipelines pipelines)
        {
            SwaggerConfig.SwaggerUIPath = "docs";

            base.ApplicationStartup(container, pipelines);
        }
    }
}