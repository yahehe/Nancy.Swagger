using System;
using JetBrains.Annotations;

namespace Nancy.Swagger.Attributes
{
    [PublicAPI]
    [AttributeUsage(AttributeTargets.Property)]
    public class RequiredAttribute : Attribute { }
}