using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Nancy.Swagger.Annotations.Attributes;
using Swagger.ObjectModel;

namespace Nancy.Swagger.Annotations.SwaggerObjects
{
    public class AnnotatedBodyParameter : BodyParameter
    {
        public AnnotatedBodyParameter(string name, Type paramType, RouteParamAttribute attrib, ISwaggerModelCatalog modelCatalog)
        {
            Name = attrib.Name ?? name;
            this.AddBodySchema(paramType, modelCatalog);
        }
    }
}
