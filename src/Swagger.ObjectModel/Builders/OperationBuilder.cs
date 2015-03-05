using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swagger.ObjectModel.Builders
{
    public class OperationBuilder
    {
        private List<string> tags;

        private string summary;

        private string description;

        private ExternalDocumentation documentation;

        private string operationId;

        private List<string> consumes;

        private List<string> produces;

        private List<Parameter> parameters;

        private List<Response> responses;

        private List<Schemes> schemes;

        private bool? deprecated;

        private IDictionary<SecuritySchemes, IEnumerable<string>> securityRequirements;

        private HttpMethod method;

        public Operation Build()
        {
            return new Operation()
                       {
                           Tags = this.tags,
                           Summary = this.summary,
                           Description = this.description,
                           ExternalDocumentation = this.documentation,
                           OperationId = this.operationId,
                           Consumes = this.consumes,
                           Produces = this.produces,
                           Parameters = this.parameters,
                           Responses = this.responses,
                           Schemes = this.schemes,
                           Deprecated = this.deprecated,
                           SecurityRequirements = this.securityRequirements,
                           Method = this.method
                       };
        }

        public OperationBuilder Tag(string tag)
        {
            if (this.tags == null)
            {
                this.tags = new List<string>();
            }

            this.tags.Add(tag);
            return this;
        }

        public OperationBuilder Tags(IEnumerable<string> tags)
        {
            if (this.tags == null)
            {
                this.tags = new List<string>();
            }

            foreach (var tag in tags)
            {
                this.tags.Add(tag);
            }

            return this;
        }

        public OperationBuilder ConsumeMimeType(string consume)
        {
            if (this.consumes == null)
            {
                this.consumes = new List<string>();
            }
            this.consumes.Add(consume);
            return this;
        }


        public OperationBuilder ConsumeMimeTypes(IEnumerable<string> consumes)
        {
            if (this.consumes == null)
            {
                this.consumes = new List<string>();
            }

            foreach (var consume in consumes)
            {
                this.consumes.Add(consume);
            }

            return this;
        }

        public OperationBuilder ProduceMimeType(string produce)
        {
            if (this.produces == null)
            {
                this.produces = new List<string>();
            }
            this.produces.Add(produce);
            return this;
        }
        
        public OperationBuilder ProduceMimeTypes(IEnumerable<string> produces)
        {
            if (this.produces == null)
            {
                this.produces = new List<string>();
            }

            foreach (var produce in produces)
            {
                this.produces.Add(produce);
            }

            return this;
        }

        public OperationBuilder Parameter(Parameter parameter)
        {
            if (this.parameters == null)
            {
                this.parameters = new List<Parameter>();
            }

            this.parameters.Add(parameter);
            return this;
        }

        public OperationBuilder Parameter(ParameterBuilder parameter)
        {
            if (this.parameters == null)
            {
                this.parameters = new List<Parameter>();
            }

            this.parameters.Add(parameter.Build());
            return this;
        }

        public OperationBuilder Parameters(IEnumerable<Parameter> parameters)
        {
            if (this.parameters == null)
            {
                this.parameters = new List<Parameter>();
            }

            foreach (var parameter in parameters)
            {
                this.parameters.Add(parameter);
            }

            return this;
        }

#error Stopped here
        public OperationBuilder Response(string response) { if (this.responses == null) { this.responses = new List<string>(); } this.responses.Add(response); return this; }
        public OperationBuilder Scheme(string scheme) { if (this.schemes == null) { this.schemes = new List<string>(); } this.schemes.Add(scheme); return this; }
        public OperationBuilder SecurityRequirement(string security) { if (this.securitys == null) { this.securitys = new List<string>(); } this.securitys.Add(security); return this; }
    }
}
