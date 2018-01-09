using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Nancy.Swagger.Annotations.Attributes;
using Swagger.ObjectModel;

namespace Nancy.Swagger.Annotations.SwaggerObjects
{
    public class AnnotatedParameter : Parameter
    {
        public AnnotatedParameter(ParameterInfo pi, RouteParamAttribute attr)
        {
            Name = attr.Name ?? pi.Name;
            In = attr.GetNullableParamType() ?? In;
            Required = attr.GetNullableRequired() ?? Required;
            Description = attr.Description ?? Description;
            Default = attr.DefaultValue ?? Default;
            Type = Primitive.IsPrimitive(pi.ParameterType) ? Primitive.FromType(pi.ParameterType).Type : "string";
        }
    }
}
