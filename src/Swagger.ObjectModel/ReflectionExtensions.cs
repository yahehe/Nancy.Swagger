using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Swagger.ObjectModel
{
    internal static class ReflectionExtensions
    {
        public static TAttribute GetCustomAttribute<TAttribute>(this MemberInfo member, bool inherits = true)
            where TAttribute : Attribute
        {
            return member.GetCustomAttributes<TAttribute>().FirstOrDefault();
        }

        public static IEnumerable<TAttribute> GetCustomAttributes<TAttribute>(this MemberInfo member, bool inherits = true)
            where TAttribute : Attribute
        {
            return member.GetCustomAttributes(typeof (TAttribute), inherits).Cast<TAttribute>();
        }

        public static bool IsDefined<TAttribute>(this MemberInfo member, bool inherits = true)
        {
            return member.IsDefined(typeof (TAttribute), inherits);
        }
    }
}