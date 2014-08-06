using System;
using Swagger.ObjectModel.ApiDeclaration;

namespace Nancy.Swagger
{
    [SwaggerApi]
    public class SwaggerParameterData
    {
        public string Name { get; set; }

        public ParameterType ParamType { get; set; }

        public string Description { get; set; }

        public bool Required { get; set; }

        public object DefaultValue { get; set; }

        public Type ParameterModel { get; set; }
    }
}