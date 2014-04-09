using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Nancy.Swagger.Tests
{
    public static class MemberInfoExtensions
    {
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