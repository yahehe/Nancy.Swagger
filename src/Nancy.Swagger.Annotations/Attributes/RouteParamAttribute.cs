using Swagger.ObjectModel.ApiDeclaration;
using System;

namespace Nancy.Swagger.Annotations.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter, Inherited = true, AllowMultiple = true)]
    public class RouteParamAttribute : SwaggerDataTypeAttribute
    {
        private ParameterType? _paramType;

        public RouteParamAttribute()
            : base(null)
        {
        }

        public RouteParamAttribute(ParameterType paramType, string name = null)
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