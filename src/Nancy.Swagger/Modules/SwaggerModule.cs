using Nancy.Swagger.Services;

namespace Nancy.Swagger.Modules
{
    [SwaggerApi]
    public class SwaggerModule : NancyModule
    {
        public SwaggerModule(ISwaggerMetadataProvider converter)
            : base(SwaggerConfig.ResourceListingPath)
        {
            Get["/api-docs"] = _ => converter.GetSwaggerJson().ToJson();
            Get["/swagger.json"] = _ => converter.GetSwaggerJson().ToJson();
        }
    }
}