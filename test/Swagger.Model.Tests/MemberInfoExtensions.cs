using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Swagger.Model.Tests
{
    public static class MemberInfoExtensions
    {
        public static bool IsDefined<TAttribute>(this MemberInfo memberInfo, bool inherit = true)
            where TAttribute : Attribute
        {
            return memberInfo.IsDefined(typeof(TAttribute), inherit);
        }

        public static TAttribute GetCustomAttribute<TAttribute>(this MemberInfo memberInfo, bool inherit = true)
            where TAttribute : Attribute
        {
            return memberInfo.GetCustomAttributes<TAttribute>(inherit).FirstOrDefault();
        }

        public static IEnumerable<TAttribute> GetCustomAttributes<TAttribute>(this MemberInfo memberInfo, bool inherit = true)
            where TAttribute : Attribute
        {
            return memberInfo.GetCustomAttributes(typeof(TAttribute), inherit).Cast<TAttribute>();
        }
    }
}