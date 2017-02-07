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

        private void BuildDescription(string description, string notes, string summary,
            IEnumerable<HttpResponseMetadata> responsesMetadatas, IEnumerable<Parameter> parameters,
            IEnumerable<Tag> tags = null, Func < OperationBuilder, Action> addModel = null)
        {
            _routeMetadataBuilder[description] = desc => desc.AsSwagger(with =>
            {
                with.Operation(op =>
                {
                    op.OperationId(description)
                        .Summary(summary)
                        .Description(notes);
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
                            if(param.Type == "file")
                            {
                                op.ConsumeMimeType("multipart/form-data");
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
                });
            });
        }

        public void DescribeRoute(string description, string notes, string summary, IEnumerable<HttpResponseMetadata> responseMetadatas, IEnumerable<Tag> tags = null, Func<OperationBuilder, Action> addModel = null)
        {
            BuildDescription(description, notes, summary, responseMetadatas, null, tags, addModel);
        }

        public void DescribeRoute<T>(string description, string notes, string summary, IEnumerable<HttpResponseMetadata> responseMetadatas, IEnumerable<Tag> tags = null)
        {
            DescribeRoute(description, notes, summary, responseMetadatas, tags, AddModel<T>);
        }

        public void DescribeRouteWithParams(string description, string notes, string summary, IEnumerable<HttpResponseMetadata> responsesMetadatas, IEnumerable<Parameter> parameters, IEnumerable<Tag> tags = null, Func<OperationBuilder, Action> addModel = null)
        {
            BuildDescription(description, notes, summary, responsesMetadatas, parameters, tags, addModel);
        }

        public void DescribeRouteWithParams<T>(string description, string notes, string summary, IEnumerable<HttpResponseMetadata> responsesMetadatas, IEnumerable<Parameter> parameters, IEnumerable<Tag> tags = null)
        {
            DescribeRouteWithParams(description, notes, summary, responsesMetadatas, parameters, tags, AddModel<T>);
        }

        private Action AddModel<T>(OperationBuilder op)
        {
            op.AddResponseSchema<T>(_modelCatalog);
            return null;
        }
    }
}
