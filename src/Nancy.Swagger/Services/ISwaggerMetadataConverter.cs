using System.Collections.Generic;
using Swagger.Model.ApiDeclaration;
using Swagger.Model.ResourceListing;

namespace Nancy.Swagger.Services
{
    public interface ISwaggerMetadataConverter
    {
        ResourceListing GetResourceListing(IEnumerable<SwaggerRouteData> routeData);

        ApiDeclaration GetApiDeclaration(IEnumerable<SwaggerRouteData> routeData);
    }
}