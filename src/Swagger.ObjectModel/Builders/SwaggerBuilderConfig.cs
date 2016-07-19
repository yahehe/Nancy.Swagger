namespace Swagger.ObjectModel.Builders
{
    using System;
    using System.Collections.Concurrent;

    using global::Swagger.ObjectModel;

    public static class SwaggerBuilderConfig
    {
        /// <summary>
        /// Contains all resolved ids by the <see cref="DefaultModelIdConvention"/>.
        /// </summary>
        private static ConcurrentDictionary<string, Type> resolvedSchemas = new ConcurrentDictionary<string, Type>();

        static SwaggerBuilderConfig()
        {
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
        /// Returns a unique model id for the given <paramref name="type"/>. 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <remarks
        /// >Returns the type's name. If this id was resolved by a 
        /// different type first, prepend the last part of the type's namespace and check again.
        /// And so on.
        /// </remarks>
        public static string DefaultModelIdConvention(Type type)
        {
            var id = type.Name;
            var fullName = type.FullName;
            var idx = fullName.LastIndexOf('.');
            while (resolvedSchemas.GetOrAdd(id, type) != type)
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
                    ? Utilities.ToCamelCase(string.Format("{0}/{1}", method.ToString().ToLower(), path))
                    : route.OperationId;
        }
    }
}
