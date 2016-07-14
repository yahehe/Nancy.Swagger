using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nancy.Metadata.Modules;
using Nancy.Swagger.Services.RouteUtils;
using Swagger.ObjectModel;

namespace Nancy.Swagger.Modules
{

    /// <summary>
    /// This class is used as a wrapper for MetadataModule<PathItem> with additional route describing capacities.
    /// It is optional and you can use the base class to still receive swagger metadata.
    /// </summary>
    public class SwaggerMetadataModule : MetadataModule<PathItem>
    {
        protected SwaggerRouteDescriber RouteDescriber;

        public SwaggerMetadataModule(ISwaggerModelCatalog modelCatalog)
        {
            RouteDescriber = new SwaggerRouteDescriber(Describe, modelCatalog);
        }

    }
}
