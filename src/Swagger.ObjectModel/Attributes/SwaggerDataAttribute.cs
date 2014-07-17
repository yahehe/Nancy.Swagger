using System;
using JetBrains.Annotations;

namespace Swagger.ObjectModel.Attributes
{
    [MeansImplicitUse(ImplicitUseTargetFlags.WithMembers)]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Enum, AllowMultiple = false, Inherited = true)]
    public class SwaggerDataAttribute : Attribute { }
}