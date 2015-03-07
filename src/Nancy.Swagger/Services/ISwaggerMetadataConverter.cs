using Swagger.ObjectModel;

namespace Nancy.Swagger.Services
{
    [SwaggerApi]
    public interface ISwaggerMetadataConverter
    {
        SwaggerRoot GetSwaggerJson();
    }
}