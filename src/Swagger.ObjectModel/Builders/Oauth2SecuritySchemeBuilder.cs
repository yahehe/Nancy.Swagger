namespace Swagger.ObjectModel.Builders
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The oauth 2 security scheme builder.
    /// </summary>
    public class Oauth2SecuritySchemeBuilder : SecuritySchemeBuilder
    {
        /// <summary>
        /// The build.
        /// </summary>
        /// <returns>
        /// The <see cref="SecurityScheme"/>.
        /// </returns>
        public override SecurityScheme Build()
        {
            if (this.flow == null)
            {
                throw new RequiredFieldException("Flow");
            }

            if ((this.flow == Oauth2Flows.Implicit || this.flow == Oauth2Flows.AccessCode) && string.IsNullOrWhiteSpace(this.authorizationUrl))
            {
                throw new RequiredFieldException("AuthorizationUrl");
            }

            if ((this.flow.Value == Oauth2Flows.AccessCode || this.flow.Value == Oauth2Flows.Application || this.flow.Value == Oauth2Flows.Password)
                && string.IsNullOrWhiteSpace(this.tokenUrl))
            {
                throw new RequiredFieldException("TokenUrl");
            }

            if (this.scopes == null || !this.scopes.Any())
            {
                throw new RequiredFieldException("Scopes");
            }

            return new SecurityScheme
                   {
                       Type = SecuritySchemes.Oauth2, 
                       Description = this.description, 
                       Flow = this.flow, 
                       AuthorizationUrl = this.authorizationUrl, 
                       TokenUrl = this.tokenUrl, 
                       Scopes = this.scopes
                   };
        }

        /// <summary>
        /// The flow.
        /// </summary>
        /// <param name="flow">
        /// The flow.
        /// </param>
        /// <returns>
        /// The <see cref="Oauth2SecuritySchemeBuilder"/>.
        /// </returns>
        public Oauth2SecuritySchemeBuilder Flow(Oauth2Flows flow)
        {
            this.flow = flow;
            return this;
        }

        /// <summary>
        /// The authorization url.
        /// </summary>
        /// <param name="authorizationUrl">
        /// The authorization url.
        /// </param>
        /// <returns>
        /// The <see cref="SecuritySchemeBuilder"/>.
        /// </returns>
        public SecuritySchemeBuilder AuthorizationUrl(string authorizationUrl)
        {
            this.authorizationUrl = authorizationUrl;
            return this;
        }

        /// <summary>
        /// The token url.
        /// </summary>
        /// <param name="tokenUrl">
        /// The token url.
        /// </param>
        /// <returns>
        /// The <see cref="SecuritySchemeBuilder"/>.
        /// </returns>
        public SecuritySchemeBuilder TokenUrl(string tokenUrl)
        {
            this.tokenUrl = tokenUrl;
            return this;
        }

        /// <summary>
        /// Add a scope
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="scope">
        /// The scope.
        /// </param>
        /// <returns>
        /// The <see cref="SecuritySchemeBuilder"/>.
        /// </returns>
        public SecuritySchemeBuilder Scope(string name, string scope)
        {
            if (this.scopes == null)
            {
                this.scopes = new Dictionary<string, string>();
            }

            this.scopes.Add(name, scope);

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