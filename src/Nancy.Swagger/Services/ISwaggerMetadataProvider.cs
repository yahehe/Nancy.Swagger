using Swagger.ObjectModel;

namespace Nancy.Swagger.Services
{
    [SwaggerApi]
    public interface ISwaggerMetadataProvider
    {
        SwaggerRoot GetSwaggerJson();
    }
}