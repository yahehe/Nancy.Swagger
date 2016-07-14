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

        public SwaggerRouteDescriber(SwaggerMetadataModule.RouteMetadataBuilder describe, ISwaggerModelCatalog modelCatalog)
        {
            _routeMetadataBuilder = describe;
            this._modelCatalog = modelCatalog;
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
            Func<OperationBuilder, Action> addModel = null)
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
                            op.Response(metadata.Code, r => r.Description(string.IsNullOrEmpty(metadata.Message) ? "N/A" : metadata.Message));
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
                            op.Parameter(param);
                        }
                    }
                });
            });
        }

        public void DescribeRoute(string description, string notes, string summary, IEnumerable<HttpResponseMetadata> responseMetadatas, Func<OperationBuilder, Action> addModel = null)
        {
            BuildDescription(description, notes, summary, responseMetadatas, null, addModel);
        }

        public void DescribeRoute<T>(string description, string notes, string summary, IEnumerable<HttpResponseMetadata> responseMetadatas)
        {
            DescribeRoute(description, notes, summary, responseMetadatas, AddModel<T>);
        }

        public void DescribeRouteWithParams(string description, string notes, string summary, IEnumerable<HttpResponseMetadata> responsesMetadatas, IEnumerable<Parameter> parameters, Func<OperationBuilder, Action> addModel = null)
        {
            BuildDescription(description, notes, summary, responsesMetadatas, parameters, addModel);
        }

        public void DescribeRouteWithParams<T>(string description, string notes, string summary, IEnumerable<HttpResponseMetadata> responsesMetadatas, IEnumerable<Parameter> parameters)
        {
            DescribeRouteWithParams(description, notes, summary, responsesMetadatas, parameters, AddModel<T>);
        }

        private Action AddModel<T>(OperationBuilder op)
        {
            op.AddResponseSchema<T>(_modelCatalog);
            return null;
        }
    }
}
