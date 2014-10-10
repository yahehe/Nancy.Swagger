using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Swagger.ObjectModel
{
    internal static class ReflectionExtensions
    {
        public static TAttribute GetCustomAttribute<TAttribute>(this ICustomAttributeProvider provider, bool inherits = true)
            where TAttribute : Attribute
        {
            return provider.GetCustomAttributes<TAttribute>(inherits).FirstOrDefault();
        }

        public static IEnumerable<TAttribute> GetCustomAttributes<TAttribute>(this ICustomAttributeProvider provider, bool inherits = true)
            where TAttribute : Attribute
        {
            return provider.GetCustomAttributes(typeof (TAttribute), inherits).Cast<TAttribute>();
        }

        public static bool IsDefined<TAttribute>(this ICustomAttributeProvider provider, bool inherits = true)
        {
            return provider.IsDefined(typeof (TAttribute), inherits);
        }
    }
}