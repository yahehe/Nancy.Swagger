using Swagger.ObjectModel;

namespace Nancy.Swagger.Services
{
    [SwaggerApi]
    public interface ISwaggerMetadataConverter
    {
        SwaggerRoot GetResourceListing();

        ApiDeclaration GetApiDeclaration(string resourcePath);
    }
}