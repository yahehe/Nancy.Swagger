using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nancy.Swagger.Services
{
    public abstract class SwaggerMetadataProvider : ISwaggerMetadataProvider
    {
        public abstract IList<SwaggerRouteData> RetrieveSwaggerRouteData();

        public abstract IList<SwaggerModelData> RetrieveSwaggerModelData();
    }
}
