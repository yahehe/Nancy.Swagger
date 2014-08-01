using Swagger.ObjectModel.ApiDeclaration;
using System;

namespace Nancy.Swagger.Annotations.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter, Inherited = true, AllowMultiple = true)]
    public class SwaggerRouteParamAttribute : SwaggerDataTypeAttribute
    {
        private ParameterType? _paramType;

        public SwaggerRouteParamAttribute()
            : base(null)
        {
        }

        public SwaggerRouteParamAttribute(ParameterType paramType, string name)
            : base(name)
        {
            ParamType = paramType;
        }

        public ParameterType ParamType
        {
            get { return _paramType.GetValueOrDefault(); }
            set { _paramType = value; }
        }

        public ParameterType? GetNullableParamType()
        {
            return _paramType;
        }
    }
}