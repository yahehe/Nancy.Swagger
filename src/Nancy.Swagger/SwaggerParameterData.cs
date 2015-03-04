using System;
using Swagger.ObjectModel;

namespace Nancy.Swagger
{
    [SwaggerApi]
    public class SwaggerParameterData
    {
        public string Name { get; set; }

        public ParameterIn ParamIn { get; set; }

        public string Description { get; set; }

        public bool Required { get; set; }

        public object DefaultValue { get; set; }

        public Type ParameterModel { get; set; }
    }
}