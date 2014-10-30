using System.Collections.Generic;

namespace Nancy.Swagger.Services
{
    public interface ISwaggerMetadataProvider
    {
        IList<SwaggerModelData> RetrieveSwaggerModelData();

        IList<SwaggerRouteData> RetrieveSwaggerRouteData();
    }
}