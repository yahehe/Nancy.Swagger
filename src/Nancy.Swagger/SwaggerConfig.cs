using System;
using System.Collections.Concurrent;
using Swagger.ObjectModel;
using System.Reflection;
using System.Globalization;
using System.Linq;

namespace Nancy.Swagger
{
    [SwaggerApi]
    public static class SwaggerConfig
    {
        /// <summary>
        /// Contains all resolved ids by the <see cref="DefaultModelIdConvention"/>.
        /// </summary>
        private static ConcurrentDictionary<string, Type> _resolvedModelIds = new ConcurrentDictionary<string, Type>();

        /// <summary>
        /// The default resource listing path, <c>api-docs</c>.
        /// </summary>
        public const string DefaultResourceListingPath = "";

        static SwaggerConfig()
        {
            ResourceListingPath = DefaultResourceListingPath;
            ModelIdConvention = DefaultModelIdConvention;
            NicknameConvention = DefaultNicknameConvention;
        }

        /// <summary>
        /// Gets or sets the function which returns a unique model id for a given <see cref="Type"/>.
        /// </summary>
        public static Func<Type, string> ModelIdConvention { get; set; }

        /// <summary>
        /// Get or sets a function which returns a unique id for the given 
        /// <see cref="PathItem"/> that can be used by tools reading
        /// the output for further and easier manipulation.
        /// </summary>
        public static Func<string, HttpMethod, Operation, string> NicknameConvention { get; set; }

        /// <summary>
        /// Gets or sets the path at which the Swagger resource listing will be served. 
        /// Default value is <see cref="DefaultResourceListingPath"/>.
        /// </summary>
        public static string ResourceListingPath { get; set; }

        /// <summary>
        /// Returns a unique model id for the given <paramref name="type"/>. 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <remarks>Returns the type's name. If this id was resolved by a 
        /// different type first, prepend the last part of the type's namespace and check again.
        /// And so on.</remarks>
        public static string DefaultModelIdConvention(Type type)
        {
            if (type.IsContainer())
            {
                return DefaultModelIdConvention(type.GetElementType() ?? type.GetTypeInfo().GetGenericArguments().First()) + "[]";
            }

            var typeInfo = type.GetTypeInfo();
            if (typeInfo.IsGenericType)
            {
                // Format the generic type name to something like: GenericOfArgument1AndArgument2
                Type genericType = type.GetGenericTypeDefinition();
                Type[] genericArguments = type.GetGenericArguments();
                string genericTypeName = genericType.Name;

                // Trim the generic parameter counts from the name
                genericTypeName = genericTypeName.Substring(0, genericTypeName.IndexOf('`'));
                string[] argumentTypeNames = genericArguments.Select(t => DefaultModelIdConvention(t)).ToArray();
                return String.Format(CultureInfo.InvariantCulture, "{0}Of{1}", genericTypeName, String.Join("And", argumentTypeNames));
            }

            var id = type.Name;
            var fullName = type.FullName;
            var idx = fullName.LastIndexOf('.');
            while (_resolvedModelIds.GetOrAdd(id, type) != type)
            {
                idx = fullName.LastIndexOf('.', idx - 1);
                id = idx <= 0 ? fullName : fullName.Substring(idx).Replace(".", "");
            }

            return id;
        }

        /// <summary>
        /// Returns a unique id for the given <paramref name="route"/>
        /// that can be used by tools reading the output for further and easier
        /// manipulation.
        /// </summary>
        /// <param name="route">The route for which a nickname is returned.</param>
        /// <returns>a unique id for the given <paramref name="route"/> that can
        /// be used by tools reading the output for further and easier 
        /// manipulation.</returns>        
        public static string DefaultNicknameConvention(string path, HttpMethod method, Operation route)
        {
            return string.IsNullOrEmpty(route.OperationId)
                    ? string.Format("{0}/{1}", method.ToString().ToLower(), path).ToCamelCase()
                    : route.OperationId;
        }
    }
}
