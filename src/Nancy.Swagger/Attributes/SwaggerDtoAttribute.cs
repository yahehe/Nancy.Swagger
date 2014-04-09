using System;
using JetBrains.Annotations;

namespace Nancy.Swagger.Attributes
{
    [MeansImplicitUse(ImplicitUseTargetFlags.WithMembers)]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public class SwaggerDtoAttribute : Attribute { }
}