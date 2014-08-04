using Nancy.Swagger.Annotations.Attributes;
using Swagger.ObjectModel.ApiDeclaration;
using System;

namespace Nancy.Swagger.Annotations.Tests
{
    public class TestRoutesModule : NancyModule
    {
        public TestRoutesModule()
            : base("testroutes")
        {
            // Routes without metadata
            Get["/anonymoushandler"] = _ => null;
            Get["/not-annotated/get"] = _ => HandlerWithoutAnnotations();

            // Primitive response
            Get["/strings"] = _ => GetStrings();
            Get["/strings/{id}"] = _ => GetStringById(_.id, Request.Query.q);

            // Non-primitive response
            Get["/models"] = _ => GetModels();
            Get["/models/{id}"] = _ => GetModel(_.id);

            // Named route
            Get["GetIntegers", "/integers"] = _ => GetIntegers();
        }

        [SwaggerRoute("GetIntegers")]
        [SwaggerRoute(Response = typeof(int[]))]
        private static dynamic GetIntegers()
        {
            throw new NotImplementedException();
        }

        [SwaggerRoute(HttpMethod.Get, "/models/{id}")]
        [SwaggerRoute(Response = typeof(TestModel))]
        private static dynamic GetModel(
            [SwaggerRouteParam(ParameterType.Path, "id")] int id
        )
        {
            throw new NotImplementedException();
        }

        [SwaggerRoute(HttpMethod.Get, "/models")]
        [SwaggerRoute(Response = typeof(TestModel[]))]
        private static dynamic GetModels()
        {
            throw new NotImplementedException();
        }

        [SwaggerRoute(HttpMethod.Get, "/strings/{id}")]
        [SwaggerRoute(Response = typeof(string))]
        private static dynamic GetStringById(
            [SwaggerRouteParam(ParameterType.Path, "id", Required = true)] int id,
            [SwaggerRouteParam(ParameterType.Query, "q")]
            [SwaggerRouteParam(Description = "Query")] string query
        )
        {
            throw new NotImplementedException();
        }

        [SwaggerRoute(HttpMethod.Get, "/strings")]
        [SwaggerRoute(Notes = "Some notes")]
        [SwaggerRoute(Summary = "Some summary")]
        [SwaggerRoute(Response = typeof(string[]))]
        private static dynamic GetStrings()
        {
            throw new NotImplementedException();
        }

        private static object HandlerWithoutAnnotations()
        {
            throw new NotImplementedException();
        }
    }
}