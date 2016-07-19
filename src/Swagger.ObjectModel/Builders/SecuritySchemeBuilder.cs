// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SecuritySchemeBuilder.cs" company="CHS Health Services">
//   Copyright (c) 2015 CHS Health Services. All rights reserved.
// </copyright>
// <summary>
//   The security scheme builder.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Swagger.ObjectModel.Builders
{
    using System.Collections.Generic;

    /// <summary>
    /// The security scheme builder.
    /// </summary>
    public abstract class SecuritySchemeBuilder
    {
        /// <summary>
        /// The type.
        /// </summary>
        protected SecuritySchemes? type;

        /// <summary>
        /// The description.
        /// </summary>
        protected string description;

        /// <summary>
        /// The name.
        /// </summary>
        protected string name;

        /// <summary>
        /// The security in.
        /// </summary>
        protected ApiKeyLocations? securityIn;

        /// <summary>
        /// The flow.
        /// </summary>
        protected Oauth2Flows? flow;

        /// <summary>
        /// The authorization url.
        /// </summary>
        protected string authorizationUrl;

        /// <summary>
        /// The token url.
        /// </summary>
        protected string tokenUrl;

        /// <summary>
        /// The scopes.
        /// </summary>
        protected Dictionary<string, string> scopes;

        /// <summary>
        /// The build.
        /// </summary>
        /// <returns>
        /// The <see cref="SecurityScheme"/>.
        /// </returns>
        /// <exception cref="RequiredFieldException">
        /// </exception>
        public virtual SecurityScheme Build()
        {
            if (this.type == null)
            {
                throw new RequiredFieldException("Type");
            }

            return new SecurityScheme
                   {
                       Type = this.type.Value, 
                       Description = this.description, 
                       Name = this.name, 
                       In = this.securityIn, 
                       Flow = this.flow, 
                       AuthorizationUrl = this.authorizationUrl, 
                       TokenUrl = this.tokenUrl, 
                       Scopes = this.scopes
                   };
        }
    }
}