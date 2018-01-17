using System;
using Swagger.ObjectModel;

namespace Nancy.Swagger.Annotations.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Parameter, Inherited = true, AllowMultiple = true)]
    public class RouteParamAttribute : SwaggerDataTypeAttribute
    {
        private ParameterIn? paramIn;

        public RouteParamAttribute()
            : base(null)
        {
        }

        public RouteParamAttribute(ParameterIn paramIn, string name = null)
            : base(name)
        {
            this.ParamIn = paramIn;
            this.ParamType = typeof(string);
        }

        public ParameterIn ParamIn
        {
            get { return this.paramIn.GetValueOrDefault(); }
            set { this.paramIn = value; }
        }

        public ParameterIn? GetNullableParamType()
        {
            return this.paramIn;
        }

        public Type ParamType { get; set; }
    }
}