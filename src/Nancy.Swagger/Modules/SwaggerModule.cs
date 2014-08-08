using Nancy.Swagger.Services;

namespace Nancy.Swagger.Modules
{
    [SwaggerApi]
    public class SwaggerModule : NancyModule
    {
        public SwaggerModule(ISwaggerMetadataConverter converter)
            : base(SwaggerConfig.ResourceListingPath)
        {
            Get["GetResourceListing", "/"] = _ => converter.GetResourceListing().ToJson();

            Get["GetApiDeclaration", "/{resourcePath*}"] = _ => converter.GetApiDeclaration("/" + _.resourcePath).ToJson();
        }
    }
}