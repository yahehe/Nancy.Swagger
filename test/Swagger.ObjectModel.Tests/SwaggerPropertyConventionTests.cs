using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Should;

using Xunit.Extensions;
using Xunit.Sdk;

namespace Swagger.ObjectModel.Tests
{
    using Swagger.ObjectModel.Attributes;

    public class SwaggerPropertyConventionTests
    {
        /// <summary>
        /// Checks that all public properties and fields of types marked with the <see cref="SwaggerDataAttribute"/>
        /// has explicitly defined a <see cref="SwaggerPropertyAttribute"/> or <see cref="SwaggerEnumValueAttribute"/>.
        /// This allows us to rename properties and enum values without worrying about breaking the Swagger JSON schema.
        /// </summary>
        /// <param name="member">The member to test.</param>
        [SwaggerPropertyConventionTest]
        public void SwaggerDtoPropertiesShouldHaveSwaggerPropertyAttribute(MemberInfo member)
        {
            var swaggerProperty = member.GetCustomAttribute<SwaggerPropertyAttribute>();
            if (swaggerProperty != null)
            {
                swaggerProperty.Name.ShouldNotBeNull();
                return;
            }

            var enumValue = member.GetCustomAttribute<SwaggerEnumValueAttribute>();
            if (enumValue != null)
            {
                enumValue.Value.ShouldNotBeNull();
                return;
            }

            throw new Exception(string.Format(
                "Member {0} is missing SwaggerProperty- or SwaggerEnumValueAttribute.", GetDisplayName(member)));
        }

        private static string GetDisplayName(MemberInfo member)
        {
            return member.DeclaringType != null
                ? string.Join(".", member.DeclaringType.Name, member.Name)
                : member.Name;
        }

        private class SwaggerPropertyConventionTestAttribute : TheoryAttribute
        {
            protected override IEnumerable<ITestCommand> EnumerateTestCommands(IMethodInfo method)
            {
                return typeof (SwaggerDataAttribute).Assembly.GetTypes()
                    .Where(type => type.IsDefined<SwaggerDataAttribute>())
                    .SelectMany(GetMemberInfo)
                    .GroupBy(GetDisplayName)
                    .Select(x => x.First())
                    .Select(member => new SwaggerPropertyTestCommand(method, member));
            }

            private static IEnumerable<MemberInfo> GetMemberInfo(Type type)
            {
                if (type.IsEnum)
                {
                    return type.GetFields(BindingFlags.Public | BindingFlags.Static);
                }

                return type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            }

            private class SwaggerPropertyTestCommand : TheoryCommand
            {
                public SwaggerPropertyTestCommand(IMethodInfo testMethod, MemberInfo member)
                    : base(testMethod, new object[] { member })
                {
                    DisplayName = GetDisplayName(member);
                }
            }
        }
    }
}