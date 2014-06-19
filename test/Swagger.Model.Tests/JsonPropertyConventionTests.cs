using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Should;
using Swagger.Model.Attributes;
using Xunit.Extensions;
using Xunit.Sdk;

namespace Swagger.Model.Tests
{
    public class JsonPropertyConventionTests
    {
        /// <summary>
        /// Checks that all public properties and fields of types marked with the <see cref="SwaggerDataAttribute"/>
        /// has explicitly defined a <see cref="JsonPropertyAttribute"/> or <see cref="EnumMemberAttribute"/>.
        /// This allows us to rename properties and enum values without worrying about breaking the Swagger JSON schema.
        /// </summary>
        /// <param name="member">The member to test.</param>
        [JsonPropertyConventionTest]
        public void SwaggerDtoPropertiesShouldHaveJsonPropertyAttribute(MemberInfo member)
        {
            var jsonProperty = member.GetCustomAttribute<JsonPropertyAttribute>();
            if (jsonProperty != null)
            {
                jsonProperty.PropertyName.ShouldNotBeNull();
                return;
            }

            var enumMember = member.GetCustomAttribute<EnumMemberAttribute>();
            if (enumMember != null)
            {
                enumMember.Value.ShouldNotBeNull();
                return;
            }

            throw new Exception(string.Format(
                "Member {0} is missing JsonProperty- or EnumMemberAttribute.", GetDisplayName(member)));
        }

        private static string GetDisplayName(MemberInfo member)
        {
            return member.DeclaringType != null
                ? string.Join(".", member.DeclaringType.Name, member.Name)
                : member.Name;
        }

        private class JsonPropertyConventionTestAttribute : TheoryAttribute
        {
            protected override IEnumerable<ITestCommand> EnumerateTestCommands(IMethodInfo method)
            {
                return typeof (SwaggerDataAttribute).Assembly.GetTypes()
                    .Where(type => type.IsDefined<SwaggerDataAttribute>())
                    .SelectMany(GetMemberInfo)
                    .GroupBy(GetDisplayName)
                    .Select(x => x.First())
                    .Select(member => new JsonPropertyTestCommand(method, member));
            }

            private static IEnumerable<MemberInfo> GetMemberInfo(Type type)
            {
                if (type.IsEnum)
                {
                    return type.GetFields(BindingFlags.Public | BindingFlags.Static);
                }

                return type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            }

            private class JsonPropertyTestCommand : TheoryCommand
            {
                public JsonPropertyTestCommand(IMethodInfo testMethod, MemberInfo member)
                    : base(testMethod, new object[] { member })
                {
                    DisplayName = GetDisplayName(member);
                }
            }
        }
    }
}