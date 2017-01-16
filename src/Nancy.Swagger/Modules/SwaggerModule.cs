using Nancy.Swagger.Services;

namespace Nancy.Swagger.Modules
{
    [SwaggerApi]
    public class SwaggerModule : NancyModule
    {
        public SwaggerModule(ISwaggerMetadataProvider converter)
            : base(SwaggerConfig.ResourceListingPath)
        {
            Get["/"] = _ => converter.GetSwaggerJson().ToJson();
        }
    }
}