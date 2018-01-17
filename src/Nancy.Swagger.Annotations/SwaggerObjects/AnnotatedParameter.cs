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
        public AnnotatedParameter(string name, Type paramType, RouteParamAttribute attr)
        {
            Name = attr.Name ?? name;
            In = attr.GetNullableParamType() ?? In;
            Required = attr.GetNullableRequired() ?? Required;
            Description = attr.Description ?? Description;
            Default = attr.DefaultValue ?? Default;
            Type = Primitive.IsPrimitive(paramType) ? Primitive.FromType(paramType).Type : "string";
        }
    }
}
