using Swagger.ObjectModel;

namespace Nancy.Swagger.Services
{
    [SwaggerApi]
    public interface ISwaggerMetadataConverter
    {
        Swagger GetResourceListing();

        ApiDeclaration GetApiDeclaration(string resourcePath);
    }
}