using Nancy.Metadata.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nancy.Swagger.Modules
{
    public class SwaggerMetadataModule : MetadataModule<SwaggerRouteData>
    {
        public SwaggerMetadataModule()
        {
            Describe["GetResourceListing"] = description => description.AsSwagger(with =>
            {
                with.ResourcePath("/");
                with.Summary("The Resource Listing serves as the root document for the API description.");
                with.Notes(
                    string.Join(
                        Environment.NewLine,
                        "The Resource Listing serves as the root document for the API description.",
                        "It contains general information about the API and an inventory of the available resources."
                    )
                );
            });
        } 
    }
}
