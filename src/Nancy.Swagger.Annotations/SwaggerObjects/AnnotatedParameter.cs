using System;
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
            Minimum = attr.GetNullableMinimum() ?? Minimum;
            Maximum = attr.GetNullableMaximum() ?? Maximum;
            UniqueItems = attr.GetNullableUniqueItems() ?? UniqueItems;
            Enum = attr.Enum ?? Enum;
            if (Primitive.IsPrimitive(paramType))
            {
                Type = Primitive.FromType(paramType).Type;
                Format = Primitive.FromType(paramType).Format;
            }
            else
            {
                Type = "string";
            }
        }
    }
}
