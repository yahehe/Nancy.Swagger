//  <copyright file="ApiKeySecuritySchemeBuilder.cs" company="Premise Health">
//      Copyright (c) 2015 Premise Health. All rights reserved.
//  </copyright>

namespace Swagger.ObjectModel.Builders
{
    /// <summary>
    /// The api key security scheme builder.
    /// </summary>
    public class ApiKeySecuritySchemeBuilder : SecuritySchemeBuilder
    {
        /// <summary>
        /// The build.
        /// </summary>
        /// <returns>
        /// The <see cref="SecurityScheme"/>.
        /// </returns>
        /// <exception cref="RequiredFieldException">
        /// </exception>
        public SecurityScheme Build()
        {
            if (string.IsNullOrWhiteSpace(this.name))
            {
                throw new RequiredFieldException("Name");
            }

            if (this.securityIn == null)
            {
                throw new RequiredFieldException("In");
            }

            return new SecurityScheme { Type = SecuritySchemes.ApiKey, Name = this.name, In = this.securityIn };
        }

        /// <summary>
        /// The name.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="SecuritySchemeBuilder"/>.
        /// </returns>
        public SecuritySchemeBuilder Name(string name)
        {
            this.name = name;
            return this;
        }

        /// <summary>
        /// Declare that the API key is located in the query
        /// </summary>
        /// <returns>
        /// The <see cref="SecuritySchemeBuilder"/>.
        /// </returns>
        public SecuritySchemeBuilder IsInQuery()
        {
            return this.In(ApiKeyLocations.Query);
        }

        /// <summary>
        /// Declare that the API key is located in a header
        /// </summary>
        /// <returns>
        /// The <see cref="SecuritySchemeBuilder"/>.
        /// </returns>
        public SecuritySchemeBuilder IsInHeader()
        {
            return this.In(ApiKeyLocations.Header);
        }

        /// <summary>
        /// The in.
        /// </summary>
        /// <param name="securityIn">
        /// The security in.
        /// </param>
        /// <returns>
        /// The <see cref="SecuritySchemeBuilder"/>.
        /// </returns>
        private SecuritySchemeBuilder In(ApiKeyLocations securityIn)
        {
            this.securityIn = securityIn;
            return this;
        }
    }
}