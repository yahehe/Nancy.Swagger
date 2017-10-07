using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Nancy.Swagger.Annotations.Attributes;
using Swagger.ObjectModel;

namespace Nancy.Swagger.Annotations.SwaggerObjects
{
    public class AnnotatedOperation : Operation
    {
        private ISwaggerModelCatalog _modelCatalog;

        public Type ResponseType { get; set; }

        public AnnotatedOperation(string name, MethodInfo handler, ISwaggerModelCatalog modelCatalog)
        {
            _modelCatalog = modelCatalog;

            OperationId = name;

            if (handler == null)
            {
                Description = "This route is not annotated. Please see " +
                              "https://github.com/yahehe/Nancy.Swagger/wiki/Nancy.Swagger-for-Nancy-v2 " +
                              "for information on how to annotate routes or to hide unannotated routes."; 
                Summary = "Warning: no annotated method found for this route";

                return;
            }
            
            foreach (var attr in handler.GetCustomAttributes<RouteAttribute>())
            {
                Summary = attr.Summary ?? Summary;
                Description = attr.Notes ?? Description;
                ResponseType = attr.Response ?? ResponseType;
                Consumes = attr.Consumes ?? Consumes;
                Produces = attr.Produces ?? Produces;
                Tags = attr.Tags ?? Tags;
            }

            Responses = handler.GetCustomAttributes<SwaggerResponseAttribute>()
                .Select(CreateSwaggerResponseObject)
                .ToDictionary(x => x.GetStatusCode().ToString(), y => (global::Swagger.ObjectModel.Response) y);


            Parameters = handler.GetParameters().Where(x => x.GetCustomAttributes<RouteParamAttribute>().Any())
                .Select(CreateSwaggerParameterData)
                .ToList();

        }

        private Parameter CreateSwaggerParameterData(ParameterInfo pi)
        {
            var param = (Parameter)new AnnotatedParameter(pi);
            if (param.In == ParameterIn.Body)
            {
                param = new AnnotatedBodyParameter(pi, _modelCatalog);
            }
            return param;
        }

        private AnnotatedResponse CreateSwaggerResponseObject(SwaggerResponseAttribute attr)
        {
            return new AnnotatedResponse(attr, _modelCatalog);
        }
    }
}
