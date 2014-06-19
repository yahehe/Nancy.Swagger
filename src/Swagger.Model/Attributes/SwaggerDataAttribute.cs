using System;
using JetBrains.Annotations;

namespace Swagger.Model.Attributes
{
    [MeansImplicitUse(ImplicitUseTargetFlags.WithMembers)]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Enum)]
    public class SwaggerDataAttribute : Attribute { }
}