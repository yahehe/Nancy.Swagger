using Nancy.ModelBinding;
using Nancy.Swagger.Annotations.Attributes;
using Swagger.ObjectModel.SwaggerDocument;
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

        [SwaggerRoute(HttpMethod.Get, "/model-with-duplicate-typename")]
        [SwaggerRoute(Response=typeof(InOtherNamespace.TestModel))]
        private dynamic GetModelWithDuplicateTypeName()
        {
            throw new NotImplementedException();
        }

        [SwaggerRoute(HttpMethod.Get, "/allowmultipleparam")]
        private dynamic GetWithAllowMultipleParam(
            [SwaggerRouteParam(ParameterIn.Query, "ids")] int[] ids)
        {
            throw new NotImplementedException();
        }

        [SwaggerRoute("GetNamedRoute")]
        [SwaggerRoute(Response = typeof(int[]))]
        private static dynamic GetNamedRoute()
        {
            throw new NotImplementedException();
        }

        [SwaggerRoute(HttpMethod.Get, "/models")]
        [SwaggerRoute(Response = typeof(TestModel[]))]
        private static dynamic GetModels()
        {
            throw new NotImplementedException();
        }

        [SwaggerRoute(HttpMethod.Get, "/models/{id}")]
        [SwaggerRoute(Response = typeof(TestModel))]
        private static dynamic GetModel(
            [SwaggerRouteParam(ParameterIn.Path, "id")] int id
        )
        {
            throw new NotImplementedException();
        }

        [SwaggerRoute(HttpMethod.Options, "/models/{id}")]        
        private dynamic OptionsModel(
            [SwaggerRouteParam(ParameterIn.Path, "id")] int id
        )
        {
            throw new NotImplementedException();
        }

        [SwaggerRoute(HttpMethod.Delete, "/models/{id}")]
        private dynamic DeleteModel(
            [SwaggerRouteParam(ParameterIn.Path, "id")] int id
        )
        {
            throw new NotImplementedException();
        }

        [SwaggerRoute(HttpMethod.Patch, "/models/{id}")] 
        private dynamic PatchModel(
            [SwaggerRouteParam(ParameterIn.Body)] TestModel testModel
        )
        {
            throw new NotImplementedException();
        }

        [SwaggerRoute(HttpMethod.Put, "/models/{id}")] 
        private dynamic PutModel(
            [SwaggerRouteParam(ParameterIn.Body)] TestModel testModel
        )        
        {
            throw new NotImplementedException();
        }

        [SwaggerRoute(HttpMethod.Post, "/models/{id}")]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.ImATeapot, "I'm a teapot")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Model = typeof(string))]
        [SwaggerResponse(HttpStatusCode.OK, Message = "Everything OK", Model = typeof(TestModel))] 
        private dynamic PostModel(
            [SwaggerRouteParam(ParameterIn.Body, Required = true)] TestModel testModel
        )
        {
            throw new NotImplementedException();
        }

        [SwaggerRoute(HttpMethod.Get, "/strings/{id}")]
        [SwaggerRoute(Response = typeof(string))]
        [SwaggerRoute(Produces = new[] { "application/json" } )]
        [SwaggerRoute(Consumes = new[] { "application/json", "application/xml" })]        
        private static dynamic GetStringById(
            [SwaggerRouteParam(ParameterIn.Path, "id", Required = true)] int id,
            [SwaggerRouteParam(ParameterIn.Query, "q")]
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