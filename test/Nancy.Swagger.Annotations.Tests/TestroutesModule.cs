using System;
using Nancy.Swagger.Annotations.Attributes.SwaggerRoute;
using Nancy.Swagger.Annotations.Attributes.SwaggerRouteParameter;

namespace Nancy.Swagger.Annotations.Tests
{
    public class TestRoutesModule : NancyModule
    {
        public TestRoutesModule()
            : base("testroutes")
        {
            Get["/anonymoushandler"] = _ => null;
            Get["/not-annotated/get"] = _ => HandlerWithoutAnnotations();

            Get["/annotated/"] = _ => GetAll();
            Get["/annotated/get/{id}"] = _ => GetById(_.id, Request.Query.q);
        }

        [Get("/annotated/get",
            Notes = "Some notes",
            Summary = "Some summary",
            Type = typeof(string)
        )]
        private static dynamic GetAll()
        {
            throw new NotImplementedException();
        }

        [Get("/annotated/get/{id}",
            Notes = "Some notes",
            Summary = "Some summary",
            Type = typeof(string)
        )]
        private static dynamic GetById(
            [PathParam("id", Required = true)] string id,
            [QueryParam("q", Description = "Query")] string query)
        {
            throw new NotImplementedException();
        }

        private static object HandlerWithoutAnnotations()
        {
            throw new NotImplementedException();
        }
    }
}