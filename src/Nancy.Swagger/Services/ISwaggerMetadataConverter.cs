using Swagger.Model.ApiDeclaration;
using Swagger.Model.ResourceListing;

namespace Nancy.Swagger.Services
{
    [SwaggerApi]
    public interface ISwaggerMetadataConverter
    {
        ResourceListing GetResourceListing();

        ApiDeclaration GetApiDeclaration(string resourcePath);
    }
}