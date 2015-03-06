using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swagger.ObjectModel.Builders
{
    public class Oauth2SecuritySchemeBuilder : SecuritySchemeBuilder
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
            if (this.flow == null)
            {
                throw new RequiredFieldException("Flow");
            }

            return new SecurityScheme
                       {
                           Type = SecuritySchemes.Oauth2,
                           Description = this.description,
                           Flow = this.flow
                       };
        }

        public Oauth2SecuritySchemeBuilder Flow(Oauth2Flows flow)
        {
            this.flow = flow;
            return this;
        }
#error stopped here
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
        /// The scope.
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
    }
}
