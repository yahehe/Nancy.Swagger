using System;
using Nancy.ModelBinding;
using Nancy.Swagger.Annotations.Attributes;
using Swagger.ObjectModel;

namespace Nancy.Swagger.Annotations.Tests
{
    public class TestRoutesModule : NancyModule
    {
        public TestRoutesModule()
            : base("testroutes")
        {
            // Routes without metadata
            Get("/anonymoushandler", _ => (object) null);
            Get("/not-annotated/get", _ => HandlerWithoutAnnotations());

            // Primitive response
            Get("/strings", _ => GetStrings());
            Get("/strings/{id}", _ => GetStringById(_.id, Request.Query.q));

            // Non-primitive response
            Get("/models", _ => GetModels());
            Get("/models/{id}", _ => GetModel(_.id));
            Post("/models/{id}", _ => PostModel(this.Bind<TestModel>()));
            Put("/models/{id}", _ => PutModel(this.Bind<TestModel>()));
            Delete("/models/{id}", _ => DeleteModel(_.id));
            Patch("/models/{id}", _ => PatchModel(this.Bind<TestModel>()));
            Options("/models/{id}", _ => OptionsModel(_.id));

            // Misc other cases
            Get("/namedroute", _ => GetNamedRoute(), null, "GetNamedRoute");
            Get("/allowmultipleparam", _ => GetWithAllowMultipleParam(Request.Query.ids));
            Get("/model-with-duplicate-typename", _ => GetModelWithDuplicateTypeName());
        }

        [Route(HttpMethod.Get, "/model-with-duplicate-typename")]
        [Route(Response = typeof(InOtherNamespace.TestModel))]
        private dynamic GetModelWithDuplicateTypeName()
        {
            throw new NotImplementedException();
        }

        [Route(HttpMethod.Get, "/allowmultipleparam")]
        private dynamic GetWithAllowMultipleParam(
            [RouteParam(ParameterIn.Query, "ids")] int[] ids)
        {
            throw new NotImplementedException();
        }

        [Route("GetNamedRoute")]
        [Route(Response = typeof(int[]))]
        private static dynamic GetNamedRoute()
        {
            throw new NotImplementedException();
        }

        [Route(HttpMethod.Get, "/models")]
        [Route(Response = typeof(TestModel[]))]
        private static dynamic GetModels()
        {
            throw new NotImplementedException();
        }

        [Route(HttpMethod.Get, "/models/{id}")]
        [Route(Response = typeof(TestModel))]
        private static dynamic GetModel(
            [RouteParam(ParameterIn.Path, "id")] int id
        )
        {
            throw new NotImplementedException();
        }

        [Route(HttpMethod.Options, "/models/{id}")]
        private dynamic OptionsModel(
            [RouteParam(ParameterIn.Path, "id")] int id
        )
        {
            throw new NotImplementedException();
        }

        [Route(HttpMethod.Delete, "/models/{id}")]
        private dynamic DeleteModel(
            [RouteParam(ParameterIn.Path, "id")] int id
        )
        {
            throw new NotImplementedException();
        }

        [Route(HttpMethod.Patch, "/models/{id}")]
        private dynamic PatchModel(
            [RouteParam(ParameterIn.Body)] TestModel testModel
        )
        {
            throw new NotImplementedException();
        }

        [Route(HttpMethod.Put, "/models/{id}")]
        private dynamic PutModel(
            [RouteParam(ParameterIn.Body)] TestModel testModel
        )
        {
            throw new NotImplementedException();
        }

        [Route(HttpMethod.Post, "/models/{id}")]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.ImATeapot, "I'm a teapot")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Model = typeof(string))]
        [SwaggerResponse(HttpStatusCode.OK, Message = "Everything OK", Model = typeof(TestModel))]
        private dynamic PostModel(
            [RouteParam(ParameterIn.Body, Required = true)] TestModel testModel
        )
        {
            throw new NotImplementedException();
        }

        [Route(HttpMethod.Get, "/strings/{id}")]
        [Route(Response = typeof(string))]
        [Route(Produces = new[] { "application/json" })]
        [Route(Consumes = new[] { "application/json", "application/xml" })]
        private static dynamic GetStringById(
            [RouteParam(ParameterIn.Path, "id", Required = true)] int id,
            [RouteParam(ParameterIn.Query, "q")]
            [RouteParam(Description = "Query")] string query
        )
        {
            throw new NotImplementedException();
        }

        [Route(HttpMethod.Get, "/strings")]
        [Route(Notes = "Some notes")]
        [Route(Summary = "Some summary")]
        [Route(Response = typeof(string[]))]
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