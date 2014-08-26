using Nancy.ModelBinding;
using Nancy.Swagger.Annotations.Attributes;
using Swagger.ObjectModel.ApiDeclaration;
using System;

namespace Nancy.Swagger.Annotations.Tests.Testdata.JsonNetEnricher
{
    public class JsonNetEnricherRoutesModule : NancyModule
    {
        public JsonNetEnricherRoutesModule()
            : base("json-net-enricher-routes")
        {
            // Primitive response
            Get["/models"] = _ => GetModels();            
        }

        [SwaggerRoute(HttpMethod.Get, "/models")]
        [SwaggerRoute(Response = typeof(JsonNetEnricherModel[]))]
        private static dynamic GetModels()
        {
            throw new NotImplementedException();
        }
    }
}