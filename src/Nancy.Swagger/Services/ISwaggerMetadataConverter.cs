using System.Collections.Generic;
using Swagger.Model.ApiDeclaration;
using Swagger.Model.ResourceListing;

namespace Nancy.Swagger.Services
{
    public interface ISwaggerMetadataConverter
    {
        ResourceListing GetResourceListing();

        ApiDeclaration GetApiDeclaration(string resourcePath);
    }
}