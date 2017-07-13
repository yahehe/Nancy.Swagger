using Nancy.Swagger.Services;
using Nancy.TinyIoc;
using Swagger.ObjectModel;

namespace Nancy.Swagger
{
    public static class Standalone
    {
        public static SwaggerRoot Generate(TinyIoCContainer container, NancyContext context)
        {
            var provider = container.Resolve<ISwaggerMetadataProvider>();
            return provider.GetSwaggerJson(context);
        }
    }
}
