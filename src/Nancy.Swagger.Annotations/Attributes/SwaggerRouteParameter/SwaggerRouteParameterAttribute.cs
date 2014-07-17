using System;
using Swagger.ObjectModel.ApiDeclaration;

namespace Nancy.Swagger.Annotations.Attributes.SwaggerRouteParameter
{
    [AttributeUsage(AttributeTargets.Parameter, Inherited = true)]
    public abstract class SwaggerRouteParameterAttribute : Attribute
    {
        protected SwaggerRouteParameterAttribute(string name, ParameterType paramType)
        {
            Name = name;
            ParamType = paramType;
        }

        public string Name { get; private set; }

        public ParameterType ParamType { get; private set; }

        public string Description { get; set; }

        public bool Required { get; set; }
    }
}