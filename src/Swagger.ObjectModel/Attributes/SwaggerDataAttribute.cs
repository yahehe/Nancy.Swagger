using System;

namespace Swagger.ObjectModel.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Enum, AllowMultiple = false, Inherited = true)]
    internal class SwaggerDataAttribute : Attribute { }
}