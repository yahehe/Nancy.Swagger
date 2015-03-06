//  <copyright file="BasicSecuritySchemeBuilder.cs" company="Premise Health">
//      Copyright (c) 2015 Premise Health. All rights reserved.
//  </copyright>

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
    }
}