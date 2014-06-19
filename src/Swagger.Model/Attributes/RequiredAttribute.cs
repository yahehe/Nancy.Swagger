using System;
using JetBrains.Annotations;

namespace Swagger.Model.Attributes
{
    [PublicAPI]
    [AttributeUsage(AttributeTargets.Property)]
    public class RequiredAttribute : Attribute { }
}