using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nancy.Metadata.Modules;
using Nancy.Swagger.Modules;
using Swagger.ObjectModel;
using Swagger.ObjectModel.Builders;

namespace Nancy.Swagger.Services.RouteUtils
{
    public class SwaggerRouteDescriber
    {
        private readonly SwaggerMetadataModule.RouteMetadataBuilder _routeMetadataBuilder;
        private readonly ISwaggerModelCatalog _modelCatalog;
        private readonly ISwaggerTagCatalog _tagCatalog;
        private Tag _baseTag;

        public SwaggerRouteDescriber(SwaggerMetadataModule.RouteMetadataBuilder describe, ISwaggerModelCatalog modelCatalog, ISwaggerTagCatalog tagCatalog)
        {
            _routeMetadataBuilder = describe;
            _modelCatalog = modelCatalog;
            _tagCatalog = tagCatalog;
        }

        public void AddBaseTag(Tag baseTag)
        {
            _baseTag = baseTag;
            if (_baseTag != null) _tagCatalog.AddTag(_baseTag);
        }

        /// <summary>
        /// This is used if your route's response model has subclasses that you want modeled too.
        /// Currently the top level class is modeled using the T generic type parameter, so this is only 
        /// needed if you want subclasses included in the models.
        /// </summary>
        /// <param name="types">The types to model</param>
        public void AddAdditionalModels(params Type[] types)
        {
            _modelCatalog.AddModels(types);
        }

        private OperationBuilder BuildDescription(string description, string notes, string summary,
            IEnumerable<HttpResponseMetadata> responsesMetadatas, IEnumerable<Parameter> parameters,
            IEnumerable<Tag> tags = null, Func < OperationBuilder, Action> addModel = null)
        {
            OperationBuilder op = new OperationBuilder();

            op.OperationId(description).Summary(summary).Description(notes);

            if (responsesMetadatas != null)
            {
                foreach (var metadata in responsesMetadatas)
                {
                    var schema = metadata.GetSchema(_modelCatalog);
                    if (schema != null)
                    {
                        op.Response(metadata.Code, r => r.Description(string.IsNullOrEmpty(metadata.Message) ? "N/A" : metadata.Message).Schema(schema));
                    }
                    else
                    {
                        op.Response(metadata.Code, r => r.Description(string.IsNullOrEmpty(metadata.Message) ? "N/A" : metadata.Message));
                    }
                }
            }

            addModel?.Invoke(op);

            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    if (string.IsNullOrEmpty(param.Type) && param.In != ParameterIn.Body)
                    {
                        param.Type = "string";
                    }
                    if (param.Type == "file")
                    {
                        op.ConsumeMimeTypes(new[] { "multipart/form-data", "application/x-www-form-urlencoded" });
                    }
                    op.Parameter(param);
                }
            }

            if (_baseTag != null)
            {
                op.Tag(_baseTag.Name);
            }

            if (tags != null)
            {
                foreach (var tag in tags)
                {
                    if (!_tagCatalog.Contains(tag)) _tagCatalog.AddTag(tag);

                    op.Tag(tag.Name);
                }
            }

            _routeMetadataBuilder[description] = desc => desc.AsSwagger(with =>
            {
                with.Operation(op);
            });

            return op;
        }

        public OperationBuilder DescribeRoute(string description, string notes, string summary, IEnumerable<HttpResponseMetadata> responseMetadatas, IEnumerable<Tag> tags = null, Func<OperationBuilder, Action> addModel = null)
        {
            return BuildDescription(description, notes, summary, responseMetadatas, null, tags, addModel);
        }

        public OperationBuilder DescribeRoute<T>(string description, string notes, string summary, IEnumerable<HttpResponseMetadata> responseMetadatas, IEnumerable<Tag> tags = null)
        {
            return DescribeRoute(description, notes, summary, responseMetadatas, tags, AddModel<T>);
        }

        public OperationBuilder DescribeRouteWithParams(string description, string notes, string summary, IEnumerable<HttpResponseMetadata> responsesMetadatas, IEnumerable<Parameter> parameters, IEnumerable<Tag> tags = null, Func<OperationBuilder, Action> addModel = null)
        {
            return BuildDescription(description, notes, summary, responsesMetadatas, parameters, tags, addModel);
        }

        public OperationBuilder DescribeRouteWithParams<T>(string description, string notes, string summary, IEnumerable<HttpResponseMetadata> responsesMetadatas, IEnumerable<Parameter> parameters, IEnumerable<Tag> tags = null)
        {
            return DescribeRouteWithParams(description, notes, summary, responsesMetadatas, parameters, tags, AddModel<T>);
        }

        private Action AddModel<T>(OperationBuilder op)
        {
            op.AddResponseSchema<T>(_modelCatalog);
            return null;
        }
    }
}
