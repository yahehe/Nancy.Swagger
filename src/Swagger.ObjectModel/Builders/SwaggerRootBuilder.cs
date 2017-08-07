// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SwaggerRootBuilder.cs" company="">
//   
// </copyright>
// <summary>
//   The swagger root builder.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Swagger.ObjectModel.Builders
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The swagger root builder.
    /// </summary>
    public class SwaggerRootBuilder
    {
        /// <summary>
        /// The paths.
        /// </summary>
        private IDictionary<string, PathItem> paths;

        /// <summary>
        /// The info.
        /// </summary>
        private Info info;

        /// <summary>
        /// The tags.
        /// </summary>
        private List<Tag> tags;

        /// <summary>
        /// The documentation.
        /// </summary>
        private ExternalDocumentation documentation;

        /// <summary>
        /// The consumes.
        /// </summary>
        private List<string> consumes;

        /// <summary>
        /// The produces.
        /// </summary>
        private List<string> produces;

        /// <summary>
        /// The parameters.
        /// </summary>
        private IDictionary<string, Parameter> parameters;

        /// <summary>
        /// The responses.
        /// </summary>
        private IDictionary<string, Response> responses;

        /// <summary>
        /// The schemes.
        /// </summary>
        private List<Schemes> schemes;

        /// <summary>
        /// The security requirements.
        /// </summary>
        private IDictionary<SecuritySchemes, IEnumerable<string>> securityRequirements;

        /// <summary>
        /// The host.
        /// </summary>
        private string host;

        /// <summary>
        /// The base path.
        /// </summary>
        private string basePath;

        /// <summary>
        /// The security definitions.
        /// </summary>
        private IDictionary<string, SecurityScheme> securityDefinitions;

        /// <summary>
        /// The definitions.
        /// </summary>
        private IDictionary<string, Schema> definitions;

        /// <summary>
        /// Initializes a new instance of the <see cref="SwaggerRootBuilder"/> class. 
        /// </summary>
        /// <param name="info">
        /// The info.
        /// </param>
        public SwaggerRootBuilder(Info info)
        {
            this.info = info;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SwaggerRootBuilder"/> class. 
        /// </summary>
        public SwaggerRootBuilder()
        {
        }

        /// <summary>
        /// The build.
        /// </summary>
        /// <returns>
        /// The <see cref="SwaggerRoot"/>.
        /// </returns>
        public SwaggerRoot Build()
        {
            if (this.info == null)
            {
                throw new RequiredFieldException("Info");
            }

            if (this.paths == null || !this.paths.Any())
            {
                throw new RequiredFieldException("Paths");
            }

            return new SwaggerRoot
                   {
                       Info = this.info, 
                       Paths = this.paths, 
                       Host = this.host, 
                       BasePath = this.basePath, 
                       Schemes = this.schemes, 
                       Consumes = this.consumes, 
                       Produces = this.produces, 
                       Definitions = this.definitions, 
                       Parameters = this.parameters, 
                       Responses = this.responses, 
                       SecurityDefinitions = this.securityDefinitions, 
                       Security = this.securityRequirements, 
                       Tags = this.tags, 
                       ExternalDocumentation = this.documentation
                   };
        }

        /// <summary>
        /// The info.
        /// </summary>
        /// <param name="info">
        /// The info.
        /// </param>
        /// <returns>
        /// The <see cref="SwaggerRootBuilder"/>.
        /// </returns>
        public SwaggerRootBuilder Info(Info info)
        {
            this.info = info;
            return this;
        }

        /// <summary>
        /// The info.
        /// </summary>
        /// <param name="info">
        /// The info.
        /// </param>
        /// <returns>
        /// The <see cref="SwaggerRootBuilder"/>.
        /// </returns>
        public SwaggerRootBuilder Info(InfoBuilder info)
        {
            this.info = info.Build();
            return this;
        }

        /// <summary>
        /// Add an available path (endpoint) for the API
        /// </summary>
        /// <param name="endpointName">
        /// A relative path to an individual endpoint. 
        /// The path is appended to the basePath in order to construct the full URL. Path templating is allowed.
        /// </param>
        /// <param name="pathItem">
        /// The path item.
        /// </param>
        /// <returns>
        /// The <see cref="SwaggerRootBuilder"/>.
        /// </returns>
        public SwaggerRootBuilder Path(string endpointName, PathItem pathItem)
        {
            if (this.paths == null)
            {
                this.paths = new Dictionary<string, PathItem>();
            }

            if (endpointName.First() != '/')
            {
                endpointName = "/" + endpointName;
            }

            this.paths.Add(endpointName, pathItem);
            return this;
        }

        /// <summary>
        /// Add an available path (endpoint) for the API
        /// </summary>
        /// <param name="endpointName">
        /// A relative path to an individual endpoint. 
        /// The path is appended to the basePath in order to construct the full URL. Path templating is allowed.
        /// </param>
        /// <param name="pathItem">
        /// The path item.
        /// </param>
        /// <returns>
        /// The <see cref="SwaggerRootBuilder"/>.
        /// </returns>
        public SwaggerRootBuilder Path(string endpointName, PathItemBuilder pathItem)
        {
            return this.Path(endpointName, pathItem.Build());
        }

        /// <summary>
        /// Add an available path (endpoint) for the API with no documentation. 
        /// The path itself is still exposed to the documentation viewer but they will not know which operations and parameters are available.
        /// </summary>
        /// <param name="endpointName">
        /// A relative path to an individual endpoint. 
        /// The path is appended to the basePath in order to construct the full URL. Path templating is allowed.
        /// </param>
        /// <returns>
        /// The <see cref="SwaggerRootBuilder"/>.
        /// </returns>
        public SwaggerRootBuilder Path(string endpointName)
        {
            return this.Path(endpointName, new PathItem());
        }

        /// <summary>
        /// The host (name or ip) serving the API. This MUST be the host only and does not include the scheme nor sub-paths. 
        /// It MAY include a port. 
        /// If the host is not included, the host serving the documentation is to be used (including the port). 
        /// The host does not support path templating.
        /// </summary>
        /// <param name="host">
        /// The host.
        /// </param>
        /// <returns>
        /// The <see cref="SwaggerRootBuilder"/>.
        /// </returns>
        public SwaggerRootBuilder Host(string host)
        {
            this.host = host;
            return this;
        }

        /// <summary>
        /// The base path on which the API is served, which is relative to the host. If it is not included, the API is served directly under the host. 
        /// The value MUST start with a leading slash (/). 
        /// The basePath does not support path templating.
        /// </summary>
        /// <param name="basePath">
        /// The base path
        /// </param>
        /// <returns>
        /// The <see cref="SwaggerRootBuilder"/>.
        /// </returns>
        public SwaggerRootBuilder BasePath(string basePath)
        {
            if (basePath.First() != '/')
            {
                basePath = "/" + basePath;
            }

            this.basePath = basePath;
            return this;
        }

        /// <summary>
        /// Add a transfer protocol
        /// </summary>
        /// <param name="scheme">
        /// The scheme.
        /// </param>
        /// <returns>
        /// The <see cref="SwaggerRootBuilder"/>.
        /// </returns>
        public SwaggerRootBuilder Scheme(Schemes scheme)
        {
            if (this.schemes == null)
            {
                this.schemes = new List<Schemes>();
            }

            this.schemes.Add(scheme);
            return this;
        }

        /// <summary>
        /// Add a MIME type the API can consume
        /// </summary>
        /// <param name="consume">
        /// The consume.
        /// </param>
        /// <returns>
        /// The <see cref="SwaggerRootBuilder"/>.
        /// </returns>
        public SwaggerRootBuilder ConsumeMimeType(string consume)
        {
            if (this.consumes == null)
            {
                this.consumes = new List<string>();
            }

            this.consumes.Add(consume);
            return this;
        }

        /// <summary>
        /// Add a list of MIME types the API can consume
        /// </summary>
        /// <param name="consumes">
        /// The consumes.
        /// </param>
        /// <returns>
        /// The <see cref="SwaggerRootBuilder"/>.
        /// </returns>
        public SwaggerRootBuilder ConsumeMimeTypes(IEnumerable<string> consumes)
        {
            foreach (var consume in consumes)
            {
                this.ConsumeMimeType(consume);
            }

            return this;
        }

        /// <summary>
        /// Add a MIME type the API can produce
        /// </summary>
        /// <param name="produce">
        /// The produce.
        /// </param>
        /// <returns>
        /// The <see cref="SwaggerRootBuilder"/>.
        /// </returns>
        public SwaggerRootBuilder ProduceMimeType(string produce)
        {
            if (this.produces == null)
            {
                this.produces = new List<string>();
            }

            this.produces.Add(produce);
            return this;
        }

        /// <summary>
        /// Add a list of MIME types the API can produce
        /// </summary>
        /// <param name="produces">
        /// The produces.
        /// </param>
        /// <returns>
        /// The <see cref="SwaggerRootBuilder"/>.
        /// </returns>
        public SwaggerRootBuilder ProduceMimeTypes(IEnumerable<string> produces)
        {
            foreach (var produce in produces)
            {
                this.ProduceMimeType(produce);
            }

            return this;
        }

        /// <summary>
        /// Add a parameter that can be reused across operations
        /// </summary>
        /// <param name="name"> Map a name for the parameter </param>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        /// <returns>
        /// The <see cref="SwaggerRootBuilder"/>.
        /// </returns>
        public SwaggerRootBuilder Parameter(string name, Parameter parameter)
        {
            if (this.parameters == null)
            {
                this.parameters = new Dictionary<string, Parameter>();
            }

            this.parameters.Add(name, parameter);
            return this;
        }

        /// <summary>
        /// Add a parameter that can be reused across operations.
        /// Uses the parameter name as the name.
        /// </summary>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        /// <returns>
        /// The <see cref="SwaggerRootBuilder"/>.
        /// </returns>
        public SwaggerRootBuilder Parameter(Parameter parameter)
        {
            return this.Parameter(parameter.Name, parameter);
        }

        /// <summary>
        /// Add a parameter for this operation
        /// </summary>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        /// <returns>
        /// The <see cref="SwaggerRootBuilder"/>.
        /// </returns>
        public SwaggerRootBuilder Parameter(ParameterBuilder parameter)
        {
            return this.Parameter(parameter.Build());
        }

        /// <summary>
        /// Add a body parameter for this operation
        /// </summary>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        /// <returns>
        /// The <see cref="SwaggerRootBuilder"/>.
        /// </returns>
        public SwaggerRootBuilder Parameter(BodyParameterBuilder parameter)
        {
            return this.Parameter(parameter.Build());
        }

        /// <summary>
        /// Add a response to be reused across operations. Response definitions can be referenced to the ones defined here.
        /// </summary>
        /// <param name="name">
        /// The name
        /// </param>
        /// <param name="response">
        /// The response.
        /// </param>
        /// <returns>
        /// The <see cref="SwaggerRootBuilder"/>.
        /// </returns>
        public SwaggerRootBuilder Response(string name, Response response)
        {
            if (this.responses == null)
            {
                this.responses = new Dictionary<string, Response>();
            }

            this.responses.Add(name, response);
            return this;
        }

        /// <summary>
        /// Add a response to be reused across operations. Response definitions can be referenced to the ones defined here.
        /// </summary>
        /// <param name="name">
        /// The name
        /// </param>
        /// <param name="response">
        /// The response.
        /// </param>
        /// <returns>
        /// The <see cref="SwaggerRootBuilder"/>.
        /// </returns>
        public SwaggerRootBuilder Response(string name, ResponseBuilder response)
        {
            return this.Response(name, response.Build());
        }

        /// <summary>
        /// Add a Security scheme definitions that can be used across the specification.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="scheme">
        /// The scheme.
        /// </param>
        /// <returns>
        /// The <see cref="SwaggerRootBuilder"/>.
        /// </returns>
        public SwaggerRootBuilder SecurityDefinition(string name, SecurityScheme scheme)
        {
            if (this.securityDefinitions == null)
            {
                this.securityDefinitions = new Dictionary<string, SecurityScheme>();
            }

            this.securityDefinitions.Add(name, scheme);
            return this;
        }

        /// <summary>
        /// Add a Security scheme definitions that can be used across the specification.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="scheme">
        /// The scheme.
        /// </param>
        /// <returns>
        /// The <see cref="SwaggerRootBuilder"/>.
        /// </returns>
        public SwaggerRootBuilder SecurityDefinition(string name, SecuritySchemeBuilder scheme)
        {
            if (this.securityDefinitions == null)
            {
                this.securityDefinitions = new Dictionary<string, SecurityScheme>();
            }

            this.securityDefinitions.Add(name, scheme.Build());
            return this;
        }

        /// <summary>
        /// Add a security requirement
        /// </summary>
        /// <param name="security">
        /// The security.
        /// </param>
        /// <returns>
        /// The <see cref="SwaggerRootBuilder"/>.
        /// </returns>
        public SwaggerRootBuilder SecurityRequirement(KeyValuePair<SecuritySchemes, IEnumerable<string>> security)
        {
            if (this.securityRequirements == null)
            {
                this.securityRequirements = new Dictionary<SecuritySchemes, IEnumerable<string>>();
            }

            this.securityRequirements.Add(security);
            return this;
        }

        /// <summary>
        /// Add a security requirement from the builder
        /// </summary>
        /// <param name="security">
        /// The security.
        /// </param>
        /// <returns>
        /// The <see cref="SwaggerRootBuilder"/>.
        /// </returns>
        public SwaggerRootBuilder SecurityRequirement(SecurityRequirementBuilder security)
        {
            return this.SecurityRequirement(security.Build());
        }

        /// <summary>
        /// Shortcut to add a security requirement that is not <see cref="SecuritySchemes.Oauth2"/>
        /// </summary>
        /// <param name="securityScheme">
        /// The security scheme.
        /// </param>
        /// <returns>
        /// The <see cref="SwaggerRootBuilder"/>.
        /// </returns>
        public SwaggerRootBuilder SecurityRequirement(SecuritySchemes securityScheme)
        {
            return this.SecurityRequirement(new SecurityRequirementBuilder().SecurityScheme(securityScheme).Build());
        }

        /// <summary>
        /// Additional external documentation
        /// </summary>
        /// <param name="documentation">
        /// The documentation.
        /// </param>
        /// <returns>
        /// The <see cref="SwaggerRootBuilder"/>.
        /// </returns>
        public SwaggerRootBuilder ExternalDocumentation(ExternalDocumentation documentation)
        {
            this.documentation = documentation;
            return this;
        }

        /// <summary>
        /// Additional external documentation 
        /// </summary>
        /// <param name="documentation">
        /// The documentation.
        /// </param>
        /// <returns>
        /// The <see cref="SwaggerRootBuilder"/>.
        /// </returns>
        public SwaggerRootBuilder ExternalDocumentation(ExternalDocumentationBuilder documentation)
        {
            this.documentation = documentation.Build();
            return this;
        }

        /// <summary>
        /// The tag.
        /// </summary>
        /// <param name="tag">
        /// The tag.
        /// </param>
        /// <returns>
        /// The <see cref="SwaggerRootBuilder"/>.
        /// </returns>
        public SwaggerRootBuilder Tag(Tag tag)
        {
            if (this.tags == null)
            {
                this.tags = new List<Tag>();
            }

            this.tags.Add(tag);
            return this;
        }

        /// <summary>
        /// The tag.
        /// </summary>
        /// <param name="tag">
        /// The tag.
        /// </param>
        /// <returns>
        /// The <see cref="SwaggerRootBuilder"/>.
        /// </returns>
        public SwaggerRootBuilder Tag(TagBuilder tag)
        {
            return this.Tag(tag.Build());
        }

        /// <summary>
        /// The definition.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="definition">
        /// The definition.
        /// </param>
        /// <returns>
        /// The <see cref="SwaggerRootBuilder"/>.
        /// </returns>
        public SwaggerRootBuilder Definition(string name, Schema definition)
        {
            if (this.definitions == null)
            {
                this.definitions = new Dictionary<string, Schema>();
            }

            if (!this.definitions.ContainsKey(name))
            {
                this.definitions.Add(name, definition);
            }
            return this;
        }

        /// <summary>
        /// The definition.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="definition">
        /// The definition.
        /// </param>
        /// <returns>
        /// The <see cref="SwaggerRootBuilder"/>.
        /// </returns>
        public SwaggerRootBuilder Definition<T>(string name, SchemaBuilder<T> definition)
        {
            return this.Definition(name, definition.Build());
        }
    }
}