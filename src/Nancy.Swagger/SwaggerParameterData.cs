using System;

using Nancy.Swagger.ApiDeclaration;

namespace Nancy.Swagger
{
    public class SwaggerParameterData
    {
        public string Name { get; set; }

        public ParameterType ParamType { get; set; }

        public string Description { get; set; }

        public bool Required { get; set; }

        public bool AllowMultiple { get; set; }

        public object DefaultValue { get; set; }

        public Type ParameterModel { get; set; }

        public Parameter ToParameter()
        {
            var parameter = new Parameter
            {
                Name = Name,
                ParamType = ParamType,
                Description = Description,
                Required = Required,
                AllowMultiple = AllowMultiple,
                DefaultValue = DefaultValue
            };

            if (Primitive.IsPrimitive(ParameterModel))
            {
                var primitive = Primitive.FromType(ParameterModel);

                parameter.Type = primitive.Type;
                parameter.Format = primitive.Format;
            }
            else
            {
                parameter.Type = ParameterModel.DefaultModelId();
            }

            return parameter;
        }
    }
}