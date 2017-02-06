using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Swagger.ObjectModel;

namespace Nancy.Swagger.Annotations.SwaggerObjects
{
    public class AnnotatedBodyParameter : BodyParameter
    {
        public AnnotatedBodyParameter(ParameterInfo pi, ISwaggerModelCatalog modelCatalog)
        {
            Name = pi.Name;
            this.AddBodySchema(pi.ParameterType, modelCatalog);
        }
    }
}
