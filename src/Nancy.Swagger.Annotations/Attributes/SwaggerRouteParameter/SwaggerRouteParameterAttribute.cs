using Swagger.Model.ApiDeclaration;
using System;

namespace Nancy.Swagger.Annotations.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter, Inherited = true)]
    public class SwaggerRouteParameterAttribute : Attribute
    {
        public SwaggerRouteParameterAttribute(string name, ParameterType paramType)
        {
            Name = name;
            ParamType = paramType;
        }

        public string Description { get; set; }

        public string Name { get; private set; }

        public ParameterType ParamType { get; private set; }

        public bool Required { get; set; }
    }
}