using Nancy.ModelBinding;
using Nancy.Swagger.Demo.Models;

namespace Nancy.Swagger.Demo.Modules
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Head("/", _ => HttpStatusCode.OK, null, "Head");

            Get("/", _ => GetPetStoreUrl(), null, "Home");

            Get("/users", _ => new[] { new User { Name = "Vincent Vega", Age = 45 } }, null, "GetUsers");

            Post("/users", _ =>
            {
                var result = this.Bind<User>();

                return Negotiate.WithModel(result).WithStatusCode(HttpStatusCode.Created);
            }, null, "PostUsers");
        }

        private Response GetPetStoreUrl()
        {
            var port = Request.Url.Port ?? 80;
            return Response.AsRedirect($"http://petstore.swagger.io/?url=http://localhost:{port}/api-docs");
        }
    }
}