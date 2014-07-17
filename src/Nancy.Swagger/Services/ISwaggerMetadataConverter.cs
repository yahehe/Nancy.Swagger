using Swagger.ObjectModel.ApiDeclaration;
using Swagger.ObjectModel.ResourceListing;

namespace Nancy.Swagger.Services
{
    [SwaggerApi]
    public interface ISwaggerMetadataConverter
    {
        ResourceListing GetResourceListing();

        ApiDeclaration GetApiDeclaration(string resourcePath);
    }
}