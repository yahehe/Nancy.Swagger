namespace Swagger.ObjectModel.Builders
{
    using System.Collections.Generic;

    /// <summary>
    /// The security requirement builder.
    /// </summary>
    public class SecurityRequirementBuilder
    {
        /// <summary>
        /// The scope names.
        /// </summary>
        private List<string> scopeNames;

        /// <summary>
        /// The scheme.
        /// </summary>
        private SecuritySchemes? scheme;

        /// <summary>
        /// The build.
        /// </summary>
        /// <returns>
        /// The <see cref="KeyValuePair"/>.
        /// </returns>
        /// <exception cref="RequiredFieldException">
        /// </exception>
        public KeyValuePair<SecuritySchemes, IEnumerable<string>> Build()
        {
            if (this.scheme == null)
            {
                throw new RequiredFieldException("Security Scheme");
            }

            // https://github.com/swagger-api/swagger-spec/blob/master/versions/2.0.md#patterned-fields-13
            // Each name must correspond to a security scheme which is declared in the Security Definitions. 
            // If the security scheme is of type "oauth2", then the value is a list of scope names required for the execution. 
            // For other security scheme types, the array MUST be empty.
            return this.scheme.Value == SecuritySchemes.Oauth2
                       ? new KeyValuePair<SecuritySchemes, IEnumerable<string>>(this.scheme.Value, this.scopeNames)
                       : new KeyValuePair<SecuritySchemes, IEnumerable<string>>(this.scheme.Value, new List<string>());
        }

        /// <summary>
        /// The security scheme.
        /// </summary>
        /// <param name="scheme">
        /// The scheme.
        /// </param>
        /// <returns>
        /// The <see cref="SecurityRequirementBuilder"/>.
        /// </returns>
        public SecurityRequirementBuilder SecurityScheme(SecuritySchemes scheme)
        {
            this.scheme = scheme;
            return this;
        }

        /// <summary>
        /// The security scheme.
        /// </summary>
        /// <param name="scopeName">
        /// The scope name.
        /// </param>
        /// <returns>
        /// The <see cref="SecurityRequirementBuilder"/>.
        /// </returns>
        public SecurityRequirementBuilder SecurityScheme(string scopeName)
        {
            if (this.scopeNames == null)
            {
                this.scopeNames = new List<string>();
            }

            this.scopeNames.Add(scopeName);
            return this;
        }

        /// <summary>
        /// The security scheme.
        /// </summary>
        /// <param name="scopeNames">
        /// The scope names.
        /// </param>
        /// <returns>
        /// The <see cref="SecurityRequirementBuilder"/>.
        /// </returns>
        public SecurityRequirementBuilder SecurityScheme(IEnumerable<string> scopeNames)
        {
            foreach (var scopeName in scopeNames)
            {
                this.SecurityScheme(scopeName);
            }

            return this;
        }
    }
}