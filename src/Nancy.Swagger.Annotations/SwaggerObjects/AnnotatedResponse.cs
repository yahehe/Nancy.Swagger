using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nancy.Swagger.Annotations.Attributes;
using Nancy.Swagger;

namespace Nancy.Swagger.Annotations.SwaggerObjects
{
    public class AnnotatedResponse : global::Swagger.ObjectModel.Response
    {
        private int StatusCode { get; set; }
        public int GetStatusCode() => StatusCode;

        public AnnotatedResponse(SwaggerResponseAttribute attr, ISwaggerModelCatalog modelCatalog)
        {
            Description = attr.Message;
            StatusCode = (int) attr.Code;

            if (attr.Model != null)
            {
                Schema = SwaggerExtensions.GetSchema(modelCatalog, attr.Model, false);
            }
        }
    }
}
