using Swagger.ObjectModel;

namespace Nancy.Swagger.Services
{
    public interface ISwaggerTagProvider
    {
        Tag GetTag();
    }
}