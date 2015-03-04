using Swagger.ObjectModel.SwaggerDocument;
using Swagger.ObjectModel.SwaggerDocument;

namespace Nancy.Swagger.Services
{
    [SwaggerApi]
    public interface ISwaggerMetadataConverter
    {
        Swagger GetResourceListing();

        ApiDeclaration GetApiDeclaration(string resourcePath);
    }
}