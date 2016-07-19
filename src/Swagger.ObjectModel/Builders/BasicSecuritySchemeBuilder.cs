// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BasicSecuritySchemeBuilder.cs" company="CHS Health Services">
//   Copyright (c) 2015 CHS Health Services. All rights reserved.
// </copyright>
// <summary>
//   The basic security scheme builder.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Swagger.ObjectModel.Builders
{
    /// <summary>
    /// The basic security scheme builder.
    /// </summary>
    public class BasicSecuritySchemeBuilder : SecuritySchemeBuilder
    {
        /// <summary>
        /// The build.
        /// </summary>
        /// <returns>
        /// The <see cref="SecurityScheme"/>.
        /// </returns>
        /// <exception cref="RequiredFieldException">
        /// </exception>
        public override SecurityScheme Build()
        {
            return new SecurityScheme { Type = SecuritySchemes.Basic, Description = this.description };
        }

        /// <summary>
        /// A short description for security scheme.
        /// </summary>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <returns>
        /// The <see cref="SecuritySchemeBuilder"/>.
        /// </returns>
        public BasicSecuritySchemeBuilder Description(string description)
        {
            this.description = description;
            return this;
        }
    }
}