using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swagger.ObjectModel.ResourceListing
{
    using Swagger.ObjectModel.ApiDeclaration;
    using Swagger.ObjectModel.Attributes;

    public class PathItem : SwaggerModel
    {
        [SwaggerProperty("$ref")]
        public string Ref { get; set; }

        [SwaggerProperty("operations")]
        IEnumerable<Operation> Operations { get; set; }

        [SwaggerProperty("parameters")]
        IEnumerable<Parameter> Parameters { get; set; }
    }
}
