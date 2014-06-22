using System;
using JetBrains.Annotations;

namespace Swagger.Model.Attributes
{
    [MeansImplicitUse(ImplicitUseTargetFlags.WithMembers)]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Enum, AllowMultiple = false, Inherited = true)]
    public class SwaggerDataAttribute : Attribute { }
}