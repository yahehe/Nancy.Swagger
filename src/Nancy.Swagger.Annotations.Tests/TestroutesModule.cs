using Nancy.Swagger.Annotations.Attributes;
using System;

namespace Nancy.Swagger.Annotations.Tests
{
    public class TestRoutesModule : NancyModule
    {
        public TestRoutesModule()
            : base("testroutes")
        {
            Get["/anonymoushandler"] = _ => { return null; };
            Get["/not-annotated/get"] = _ => HandlerWithoutAnnotations();

            Get["/annotated/"] = _ => GetAll();
            Get["/annotated/get/{id}"] = _ => GetById(_.id, Request.Query.q);
        }

        [Get("/annotated/get",
            Notes = "Some notes",
            Summary = "Some summary",
            Type = typeof(string)
        )]
        private dynamic GetAll()
        {
            throw new NotImplementedException();
        }

        [Get("/annotated/get/{id}",
            Notes = "Some notes",
            Summary = "Some summary",
            Type = typeof(string)
        )]
        private dynamic GetById(
            [PathParam("id", Required = true)] string id,
            [QueryParam("q", Description = "Query")] string query)
        {
            throw new NotImplementedException();
        }

        private object HandlerWithoutAnnotations()
        {
            throw new NotImplementedException();
        }
    }
}