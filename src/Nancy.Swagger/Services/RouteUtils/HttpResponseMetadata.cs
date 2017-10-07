using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Swagger.ObjectModel;

namespace Nancy.Swagger.Services.RouteUtils
{
    public class HttpResponseMetadata
    {
        public int Code { get; set; }
        public string Message { get; set; }

        public virtual Schema GetSchema(ISwaggerModelCatalog modelCatalog)
        {
            return null;
        }
    }

    public class HttpResponseMetadata<T> : HttpResponseMetadata
    {
        public override Schema GetSchema(ISwaggerModelCatalog modelCatalog)
        {
            return SwaggerExtensions.GetSchema<T>(modelCatalog, false);
        }
    }
}
