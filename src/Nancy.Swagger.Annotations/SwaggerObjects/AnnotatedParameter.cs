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
        public AnnotatedParameter(ParameterInfo pi)
        {
            Name = pi.Name;

            var paramAttrs = pi.GetCustomAttributes<RouteParamAttribute>();
            if (!paramAttrs.Any())
            {
                Description = "Warning: no annotation found for this parameter";
                In = ParameterIn.Query; // Required, so use query as fallback

                return;
            }

            foreach (var attr in paramAttrs)
            {
                Name = attr.Name ?? Name;
                In = attr.GetNullableParamType() ?? In;
                Required = attr.GetNullableRequired() ?? Required;
                Description = attr.Description ?? Description;
                Default = attr.DefaultValue ?? Default;
            }

            Type = Primitive.IsPrimitive(pi.ParameterType) ? Primitive.FromType(pi.ParameterType).Type : "string";
        }
    }
}
