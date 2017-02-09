using Nancy.ModelBinding;
using Nancy.Swagger.Demo.Models;

namespace Nancy.Swagger.Demo.Modules
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Head("/", _ => HttpStatusCode.OK, null, "Head");

            Get("/", _ => Response.AsRedirect("/swagger-ui/dist/index.html"), null, "Home");

            Get("/users", _ => new[] { new User { Name = "Vincent Vega", Age = 45 } }, null, "GetUsers");

            Post("/users", _ =>
            {
                var result = this.Bind<User>();

                return Negotiate.WithModel(result).WithStatusCode(HttpStatusCode.Created);
            }, null, "PostUsers");
        }
    }
}