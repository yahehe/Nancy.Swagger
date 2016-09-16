using Nancy.ModelBinding;
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
            Post["/models/{id}"] = _ => PostModel(this.Bind<TestModel>());
            Put["/models/{id}"] = _ => PutModel(this.Bind<TestModel>());
            Delete["/models/{id}"] = _ => DeleteModel(_.id);
            Patch["/models/{id}"] = _ => PatchModel(this.Bind<TestModel>());
            Options["/models/{id}"] = _ => OptionsModel(_.id);

            // Misc other cases
            Get["GetNamedRoute", "/namedroute"] = _ => GetNamedRoute();
            Get["/allowmultipleparam"] = _ => GetWithAllowMultipleParam(Request.Query.ids);
            Get["/model-with-duplicate-typename"] = _ => GetModelWithDuplicateTypeName();
        }

        [Route(HttpMethod.Get, "/model-with-duplicate-typename")]
        [Route(Response=typeof(InOtherNamespace.TestModel))]
        private dynamic GetModelWithDuplicateTypeName()
        {
            throw new NotImplementedException();
        }

        [Route(HttpMethod.Get, "/allowmultipleparam")]
        private dynamic GetWithAllowMultipleParam(
            [RouteParam(ParameterType.Query, "ids")] int[] ids)
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
            [RouteParam(ParameterType.Path, "id")] int id
        )
        {
            throw new NotImplementedException();
        }

        [Route(HttpMethod.Options, "/models/{id}")]        
        private dynamic OptionsModel(
            [RouteParam(ParameterType.Path, "id")] int id
        )
        {
            throw new NotImplementedException();
        }

        [Route(HttpMethod.Delete, "/models/{id}")]
        private dynamic DeleteModel(
            [RouteParam(ParameterType.Path, "id")] int id
        )
        {
            throw new NotImplementedException();
        }

        [Route(HttpMethod.Patch, "/models/{id}")] 
        private dynamic PatchModel(
            [RouteParam(ParameterType.Body)] TestModel testModel
        )
        {
            throw new NotImplementedException();
        }

        [Route(HttpMethod.Put, "/models/{id}")] 
        private dynamic PutModel(
            [RouteParam(ParameterType.Body)] TestModel testModel
        )        
        {
            throw new NotImplementedException();
        }

        [Route(HttpMethod.Post, "/models/{id}")]
        [Response(HttpStatusCode.NotFound)]
        [Response(HttpStatusCode.ImATeapot, "I'm a teapot")]
        [Response(HttpStatusCode.InternalServerError, Model = typeof(string))]
        [Response(HttpStatusCode.OK, Message = "Everything OK", Model = typeof(TestModel))] 
        private dynamic PostModel(
            [RouteParam(ParameterType.Body, Required = true)] TestModel testModel
        )
        {
            throw new NotImplementedException();
        }

        [Route(HttpMethod.Get, "/strings/{id}")]
        [Route(Response = typeof(string))]
        [Route(Produces = new[] { "application/json" } )]
        [Route(Consumes = new[] { "application/json", "application/xml" })]        
        private static dynamic GetStringById(
            [RouteParam(ParameterType.Path, "id", Required = true)] int id,
            [RouteParam(ParameterType.Query, "q")]
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