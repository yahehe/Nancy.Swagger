using Swagger.ObjectModel.SwaggerDocument;
using System;

namespace Nancy.Swagger.Annotations.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter, Inherited = true, AllowMultiple = true)]
    public class SwaggerRouteParamAttribute : SwaggerDataTypeAttribute
    {
        private ParameterIn? paramIn;

        public SwaggerRouteParamAttribute()
            : base(null)
        {
        }

        public SwaggerRouteParamAttribute(ParameterIn paramIn, string name = null)
            : base(name)
        {
            this.ParamIn = paramIn;
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
    }
}