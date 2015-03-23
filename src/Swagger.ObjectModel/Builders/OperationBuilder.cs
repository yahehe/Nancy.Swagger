//  <copyright file="OperationBuilder.cs" company="Premise Health">
//      Copyright (c) 2015 Premise Health. All rights reserved.
//  </copyright>

namespace Swagger.ObjectModel.Builders
{
    using System;
    using System.Collections.Generic;
    using System.Net;

    /// <summary>
    /// The operation builder.
    /// </summary>
    public class OperationBuilder
    {
        /// <summary>
        /// The tags.
        /// </summary>
        private List<string> tags;

        /// <summary>
        /// The summary.
        /// </summary>
        private string summary;

        /// <summary>
        /// The description.
        /// </summary>
        private string description;

        /// <summary>
        /// The documentation.
        /// </summary>
        private ExternalDocumentation documentation;

        /// <summary>
        /// The operation id.
        /// </summary>
        private string operationId;

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
        private List<Parameter> parameters;

        /// <summary>
        /// The responses.
        /// </summary>
        private IDictionary<string, Response> responses;

        /// <summary>
        /// The schemes.
        /// </summary>
        private List<Schemes> schemes;

        /// <summary>
        /// The deprecated.
        /// </summary>
        private bool? deprecated;

        /// <summary>
        /// The security requirements.
        /// </summary>
        private IDictionary<SecuritySchemes, IEnumerable<string>> securityRequirements;

        /// <summary>
        /// The build.
        /// </summary>
        /// <returns>
        /// The <see cref="Operation"/>.
        /// </returns>
        public Operation Build(Operation provided = null)
        {
            if (this.responses == null)
            {
                throw new RequiredFieldException("Responses");
            }

            if (this.responses.Count < 1)
            {
                throw new InvalidOperationException(
                    "The Responses Object MUST contain at least one response code, and it SHOULD be the response for a successful operation call.");
            }
            provided = new Operation() ?? provided;

            provided.Tags = this.tags;
            provided.Summary = this.summary;
            provided.Description = this.description;
            provided.ExternalDocumentation = this.documentation;
            provided.OperationId = this.operationId;
            provided.Consumes = this.consumes;
            provided.Produces = this.produces;
            provided.Parameters = this.parameters;
            provided.Responses = this.responses;
            provided.Schemes = this.schemes;
            provided.Deprecated = this.deprecated;
            provided.SecurityRequirements = this.securityRequirements;
            return provided;
        }

        /// <summary>
        /// Add  list of tags for API documentation control. 
        /// Tags can be used for logical grouping of operations by resources or any other qualifier.
        /// </summary>
        /// <param name="tag">
        /// The tag.
        /// </param>
        /// <returns>
        /// The <see cref="OperationBuilder"/>.
        /// </returns>
        public OperationBuilder Tag(string tag)
        {
            if (this.tags == null)
            {
                this.tags = new List<string>();
            }

            this.tags.Add(tag);
            return this;
        }

        /// <summary>
        /// Add  list of tags for API documentation control. 
        /// Tags can be used for logical grouping of operations by resources or any other qualifier.
        /// </summary>
        /// <param name="tags">
        /// The tags.
        /// </param>
        /// <returns>
        /// The <see cref="OperationBuilder"/>.
        /// </returns>
        public OperationBuilder Tags(IEnumerable<string> tags)
        {
            foreach (var tag in tags)
            {
                this.Tag(tag);
            }

            return this;
        }

        /// <summary>
        /// A short summary of what the operation does. For maximum readability in the swagger-ui, this field SHOULD be less than 120 characters.
        /// </summary>
        /// <param name="summary">
        /// The summary.
        /// </param>
        /// <returns>
        /// The <see cref="OperationBuilder"/>.
        /// </returns>
        public OperationBuilder Summary(string summary)
        {
            this.summary = summary;
            return this;
        }

        /// <summary>
        /// A verbose explanation of the operation behavior. GitHub Flavored Markdown syntax can be used for rich text representation.
        /// </summary>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <returns>
        /// The <see cref="OperationBuilder"/>.
        /// </returns>
        public OperationBuilder Description(string description)
        {
            this.description = description;
            return this;
        }

        /// <summary>
        /// Additional external documentation
        /// </summary>
        /// <param name="documentation">
        /// The documentation.
        /// </param>
        /// <returns>
        /// The <see cref="OperationBuilder"/>.
        /// </returns>
        public OperationBuilder ExternalDocumentation(ExternalDocumentation documentation)
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
        /// The <see cref="OperationBuilder"/>.
        /// </returns>
        public OperationBuilder ExternalDocumentation(ExternalDocumentationBuilder documentation)
        {
            this.documentation = documentation.Build();
            return this;
        }

        /// <summary>
        /// A friendly name for the operation. 
        /// The id MUST be unique among all operations described in the API. 
        /// Tools and libraries MAY use the operation id to uniquely identify an operation.
        /// </summary>
        /// <param name="operationId">
        /// The operation id.
        /// </param>
        /// <returns>
        /// The <see cref="OperationBuilder"/>.
        /// </returns>
        public OperationBuilder OperationId(string operationId)
        {
            this.operationId = operationId;
            return this;
        }

        /// <summary>
        /// Add a MIME type the operation can consume
        /// </summary>
        /// <param name="consume">
        /// The consume.
        /// </param>
        /// <returns>
        /// The <see cref="OperationBuilder"/>.
        /// </returns>
        public OperationBuilder ConsumeMimeType(string consume)
        {
            if (this.consumes == null)
            {
                this.consumes = new List<string>();
            }

            this.consumes.Add(consume);
            return this;
        }

        /// <summary>
        /// Add a list of MIME types the operation can consume
        /// </summary>
        /// <param name="consumes">
        /// The consumes.
        /// </param>
        /// <returns>
        /// The <see cref="OperationBuilder"/>.
        /// </returns>
        public OperationBuilder ConsumeMimeTypes(IEnumerable<string> consumes)
        {
            foreach (var consume in consumes)
            {
                this.ConsumeMimeType(consume);
            }

            return this;
        }

        /// <summary>
        /// Add a MIME type the operation can produce
        /// </summary>
        /// <param name="produce">
        /// The produce.
        /// </param>
        /// <returns>
        /// The <see cref="OperationBuilder"/>.
        /// </returns>
        public OperationBuilder ProduceMimeType(string produce)
        {
            if (this.produces == null)
            {
                this.produces = new List<string>();
            }

            this.produces.Add(produce);
            return this;
        }

        /// <summary>
        /// Add a list of MIME types the operation can produce
        /// </summary>
        /// <param name="produces">
        /// The produces.
        /// </param>
        /// <returns>
        /// The <see cref="OperationBuilder"/>.
        /// </returns>
        public OperationBuilder ProduceMimeTypes(IEnumerable<string> produces)
        {
            foreach (var produce in produces)
            {
                this.ProduceMimeType(produce);
            }

            return this;
        }

        /// <summary>
        /// Add a parameter for this operation
        /// </summary>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        /// <returns>
        /// The <see cref="OperationBuilder"/>.
        /// </returns>
        public OperationBuilder Parameter(Parameter parameter)
        {
            if (this.parameters == null)
            {
                this.parameters = new List<Parameter>();
            }

            this.parameters.Add(parameter);
            return this;
        }

        /// <summary>
        /// Add a parameter for this operation
        /// </summary>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        /// <returns>
        /// The <see cref="OperationBuilder"/>.
        /// </returns>
        public OperationBuilder Parameter(Action<ParameterBuilder> parameter)
        {
            var builder = new ParameterBuilder();
            parameter(builder);
            return this.Parameter(builder.Build());
        }

        /// <summary>
        /// Add a body parameter for this operation
        /// </summary>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        /// <returns>
        /// The <see cref="OperationBuilder"/>.
        /// </returns>
        public OperationBuilder BodyParameter(Action<BodyParameterBuilder> parameter)
        {
            var builder = new BodyParameterBuilder();
            parameter(builder);
            return this.Parameter(builder.Build());
        }

        /// <summary>
        /// Add parameters that are valid for this operation
        /// </summary>
        /// <param name="parameters">
        /// The parameters.
        /// </param>
        /// <returns>
        /// The <see cref="OperationBuilder"/>.
        /// </returns>
        public OperationBuilder Parameters(IEnumerable<Parameter> parameters)
        {
            foreach (var parameter in parameters)
            {
                this.Parameter(parameter);
            }

            return this;
        }

        /// <summary>
        /// Add the default response
        /// </summary>
        /// <param name="response">
        /// The response.
        /// </param>
        /// <returns>
        /// The <see cref="OperationBuilder"/>.
        /// </returns>
        public OperationBuilder Response(Action<ResponseBuilder> response)
        {
            return this.Response("default", response);
        }

        /// <summary>
        /// Add the expected response object for an HTTP Status Code
        /// </summary>
        /// <param name="httpStatusCode">
        /// The http status code.
        /// </param>
        /// <param name="response">
        /// The response.
        /// </param>
        /// <returns>
        /// The <see cref="OperationBuilder"/>.
        /// </returns>
        public OperationBuilder Response(HttpStatusCode httpStatusCode, Action<ResponseBuilder> response)
        {
            return this.Response(httpStatusCode.ToString(), response);
        }

        /// <summary>
        /// Add the expected response object for an HTTP Status Code
        /// </summary>
        /// <param name="httpStatusCode">
        /// The http status code.
        /// </param>
        /// <param name="response">
        /// The response.
        /// </param>
        /// <returns>
        /// The <see cref="OperationBuilder"/>.
        /// </returns>
        public OperationBuilder Response(int httpStatusCode, Action<ResponseBuilder> response)
        {
            return this.Response(httpStatusCode.ToString(), response);
        }

        /// <summary>
        /// Add the expected response object for an HTTP Status Code
        /// </summary>
        /// <param name="httpStatusCode">
        /// The http status code.
        /// </param>
        /// <param name="response">
        /// The response.
        /// </param>
        /// <returns>
        /// The <see cref="OperationBuilder"/>.
        /// </returns>
        public OperationBuilder Response(string httpStatusCode, Action<ResponseBuilder> response)
        {
            if (this.responses == null)
            {
                this.responses = new Dictionary<string, Response>();
            }
            var builder = new ResponseBuilder();
            response(builder);
            this.responses.Add(httpStatusCode, builder.Build());
            return this;
        }

        /// <summary>
        /// Add a transfer protocol
        /// </summary>
        /// <param name="scheme">
        /// The scheme.
        /// </param>
        /// <returns>
        /// The <see cref="OperationBuilder"/>.
        /// </returns>
        public OperationBuilder Scheme(Schemes scheme)
        {
            if (this.schemes == null)
            {
                this.schemes = new List<Schemes>();
            }

            this.schemes.Add(scheme);
            return this;
        }

        /// <summary>
        /// Declares this operation to be deprecated
        /// </summary>
        /// <returns>
        /// The <see cref="OperationBuilder"/>.
        /// </returns>
        public OperationBuilder IsDeprecated()
        {
            this.deprecated = true;
            return this;
        }

        /// <summary>
        /// Add a security requirement
        /// </summary>
        /// <param name="security">
        /// The security.
        /// </param>
        /// <returns>
        /// The <see cref="OperationBuilder"/>.
        /// </returns>
        public OperationBuilder SecurityRequirement(KeyValuePair<SecuritySchemes, IEnumerable<string>> security)
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
        /// The <see cref="OperationBuilder"/>.
        /// </returns>
        public OperationBuilder SecurityRequirement(SecurityRequirementBuilder security)
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
        /// The <see cref="OperationBuilder"/>.
        /// </returns>
        public OperationBuilder SecurityRequirement(SecuritySchemes securityScheme)
        {
            return this.SecurityRequirement(new SecurityRequirementBuilder().SecurityScheme(securityScheme).Build());
        }
    }
}