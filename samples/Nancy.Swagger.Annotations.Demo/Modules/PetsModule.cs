using System;
using Nancy.ModelBinding;
using Nancy.Swagger.Annotations.Attributes;
using Nancy.Swagger.Annotations.Demo.Models;
using Swagger.ObjectModel.ApiDeclaration;

namespace Nancy.Swagger.Annotations.Demo.Modules
{
    public class PetsModule : NancyModule
    {
        public PetsModule()
            : base("pet")
        {
            Post[""] = _ => AddNewPet(this.Bind<Pet>());
            Put[""] = _ => UpdatePet(this.Bind<Pet>());
        }

        [Route(HttpMethod.Post, "")]
        [Route(Summary = "Add a new pet to the store")]
        [SwaggerResponse(HttpStatusCode.MethodNotAllowed, "Invalid input")]
        private static dynamic AddNewPet(
            [RouteParam(ParameterType.Body)]
            [RouteParam(Description = "Pet object that needs to be added to the store")] 

            Pet pet
        )
        {
            throw new NotImplementedException();
        }

        [Route(HttpMethod.Put, "")]
        [Route(Summary = "Update an existing pet")]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Invalid ID supplied")]
        [SwaggerResponse(HttpStatusCode.NotFound, "Pet not found")]
        [SwaggerResponse(HttpStatusCode.MethodNotAllowed, "Validation exception")]
        private static dynamic UpdatePet(
            [RouteParam(ParameterType.Body)]
            [RouteParam(Description = "Pet object that needs to be updated in the store")] 
            Pet pet
        )
        {
            throw new NotImplementedException();
        }
    }
}