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
            Get["GetAllowMultipleParam", "/allowmultipleparam"] = _ => GetWithAllowMultipleParam(Request.Query.ids);
        }

        [SwaggerRoute("GetAllowMultipleParam")]
        private dynamic GetWithAllowMultipleParam(
            [SwaggerRouteParam(ParameterType.Query, "ids")] int[] ids)
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
            [SwaggerRouteParam(ParameterType.Path, "id")] int id
        )
        {
            throw new NotImplementedException();
        }

        [SwaggerRoute(HttpMethod.Options, "/models/{id}")]        
        private dynamic OptionsModel(
            [SwaggerRouteParam(ParameterType.Path, "id")] int id
        )
        {
            throw new NotImplementedException();
        }

        [SwaggerRoute(HttpMethod.Delete, "/models/{id}")]
        private dynamic DeleteModel(
            [SwaggerRouteParam(ParameterType.Path, "id")] int id
        )
        {
            throw new NotImplementedException();
        }

        [SwaggerRoute(HttpMethod.Patch, "/models/{id}")] 
        private dynamic PatchModel(
            [SwaggerRouteParam(ParameterType.Body)] TestModel testModel
        )
        {
            throw new NotImplementedException();
        }

        [SwaggerRoute(HttpMethod.Put, "/models/{id}")] 
        private dynamic PutModel(
            [SwaggerRouteParam(ParameterType.Body)] TestModel testModel
        )        
        {
            throw new NotImplementedException();
        }

        [SwaggerRoute(HttpMethod.Post, "/models/{id}")]
        private dynamic PostModel(
            [SwaggerRouteParam(ParameterType.Body, Required = true)] TestModel testModel
        )
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