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

            var paramsList = new List<Parameter>();
            //first check if any RouteParamAttributes are on the Method.
            CreateSwaggerParametersFromMethodAttributes(handler, paramsList);
            //then check for RouteParamAttributes on themethod parameters
            CreateSwaggerParametersFromParameters(handler, paramsList);

            Parameters = paramsList;

        }

        private void CreateSwaggerParametersFromMethodAttributes(MethodInfo info, List<Parameter> paramsList)
        {
            var routeParamsAttributes = info.GetCustomAttributes<RouteParamAttribute>(true);
            foreach (var attrib in routeParamsAttributes)
            {
                if (string.IsNullOrEmpty(attrib.Name))
                    throw new ArgumentNullException("Name", "RouteParam name cannot be null when used on method.");
                //we can only have 1 body parameter
                if (attrib.ParamIn == ParameterIn.Body)
                {
                    //if we already have a body parameter then ignore any subsequent ones.
                    if (paramsList.Where(x => x.GetType() == typeof(AnnotatedBodyParameter)).Any())
                    {
                        continue;
                    }
                    if ((attrib.ParamType == null))
                        throw new ArgumentNullException("ParamType", "ParamType for Body must bespecified when RouteParam used on method.");

                    paramsList.Add(new AnnotatedBodyParameter(attrib.Name, attrib.ParamType, attrib, _modelCatalog));
                }
                //ignore duplicate named parameters.
                if (!paramsList.Any(x => x.Name.Equals(attrib.Name)))
                {
                    paramsList.Add(new AnnotatedParameter(attrib.Name, attrib.ParamType, attrib));
                }
            }
        }

        private void CreateSwaggerParametersFromParameters(MethodInfo info, List<Parameter> paramsList)
        {
            //get parameters that have RouteParamAttrbutes 
            var infos = info.GetParameters().Where(x => x.GetCustomAttributes<RouteParamAttribute>().Any()).ToList();

            foreach (var paramInfo in infos)
            {
                var paramAttrs = paramInfo.GetCustomAttributes<RouteParamAttribute>(true);
                //Body param trumps all other attributes.
                var bodyParamAttr = paramAttrs.FirstOrDefault(x => x.ParamIn == ParameterIn.Body);
                if (bodyParamAttr != null)
                {
                    //Attribute on method parameter will replace one specified on the method.
                    var existingBodyParam = paramsList.FirstOrDefault(x => x.GetType() == typeof(AnnotatedBodyParameter));
                    if (existingBodyParam != null)
                    {
                        paramsList.Remove(existingBodyParam);
                    }
                    paramsList.Add(new AnnotatedBodyParameter(paramInfo.Name, paramInfo.ParameterType, bodyParamAttr, _modelCatalog));
                    continue;
                }

                var nonBodyAttrs = paramAttrs.Where(x => x.ParamIn != ParameterIn.Body);
                foreach (var attr in nonBodyAttrs)
                {
                    paramsList.Add(new AnnotatedParameter(paramInfo.Name, paramInfo.ParameterType, attr));
                }
            }
        }

        private AnnotatedResponse CreateSwaggerResponseObject(SwaggerResponseAttribute attr)
        {
            return new AnnotatedResponse(attr, _modelCatalog);
        }
    }
}
