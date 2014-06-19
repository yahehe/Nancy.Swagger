using System;
using JetBrains.Annotations;

namespace Nancy.Swagger.Attributes
{
    [MeansImplicitUse(ImplicitUseTargetFlags.WithMembers)]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Enum)]
    public class SwaggerDataAttribute : Attribute { }
}