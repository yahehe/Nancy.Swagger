//  <copyright file="SecuritySchemeBuilder.cs" company="Premise Health">
//      Copyright (c) 2015 Premise Health. All rights reserved.
//  </copyright>

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
        protected SecuritySchemes type;

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
                           Type = this.type,
                           Description = this.description,
                           Name = this.name,
                           In = this.securityIn,
                           Flow = this.flow,
                           AuthorizationUrl = this.authorizationUrl,
                           TokenUrl = this.tokenUrl,
                           Scopes = this.scopes
                       };
        }

        /// <summary>
        ///  The type of the security scheme
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The <see cref="SecuritySchemeBuilder"/>.
        /// </returns>
        public SecuritySchemeBuilder Type(SecuritySchemes type)
        {
            this.type = type;
            return this;
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
        public SecuritySchemeBuilder Description(string description)
        {
            this.description = description;
            return this;
        }
    }
}