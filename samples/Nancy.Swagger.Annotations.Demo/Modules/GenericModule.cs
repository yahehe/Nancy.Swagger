using System.Collections.Generic;
using Microsoft.Extensions.FileProviders;
using Nancy.ModelBinding;
using Nancy.Responses.Negotiation;
using Nancy.Swagger.Demo.Models;
using Nancy.Swagger.Annotations.Attributes;
using Swagger.ObjectModel;
using Nancy.Swagger.Services;

namespace Nancy.Swagger.Demo.Modules
{
    public class GenericModule : NancyModule
    {
        public GenericModule(ISwaggerTagCatalog tagCatalog)
            : base("/generic")
        {
            tagCatalog.AddTag(new Tag()
            {
                Description = "All response are wrapped in a base class that can include metadata",
                Name = "Generics"
            });

            Get("/users", _ => GetUsers(), null, "GetUsers");

            Post("/users", _ =>
            {
                var user = this.Bind<User>();
                return PostUser(user);
            });
        }

        [Route("GetUsers")]
        [Route(HttpMethod.Get, "/users")]
        [Route(Summary = "Get All Users")]
        [SwaggerResponse(HttpStatusCode.OK, Message = "OK", Model = typeof(ApiResponse<IEnumerable<User>>))]
        [Route(Tags = new[] { "Generics" })]
        private ApiResponse<IEnumerable<User>> GetUsers()
        {
            return new ApiResponse<IEnumerable<User>>(new[] {new User {Name = "Vincent Vega", Age = 45}});
        }

        [Route(HttpMethod.Post, "/users")]
        [Route(Summary = "Post New User")]
        [SwaggerResponse(HttpStatusCode.OK, Message = "OK", Model = typeof(ApiResponse<User>))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Message = "Internal Server Error")]
        [Route(Produces = new[] { "application/json" })]
        [Route(Consumes = new[] { "application/json", "application/xml" })]
        [Route(Tags = new[] { "Generics" })]
        private ApiResponse<User> PostUser([RouteParam(ParameterIn.Body)] User user)
        {
            return new ApiResponse<User>(user);
        } 
    }
}