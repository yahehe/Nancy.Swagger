using Nancy.ModelBinding;
using Nancy.Swagger.Demo.Models;

namespace Nancy.Swagger.Demo.Modules
{
    public class ServiceDetailsModule : NancyModule
    {
        public ServiceDetailsModule() : base("/service")
        {
            Get("/", _ => "Hello again, Swagger!", null, "ServiceHome");

            Get("/details", _ => new ServiceDetails()
            {
                Name = "Nancy Swagger Service",
                Owner = new ServiceOwner()
                {
                    CompanyName = "Swagger Example Inc.",
                    CompanyContactEmail = "company@swaggerexample.inc"
                },
                Customers = new []
                {
                    new ServiceCustomer() {CustomerName = "Jack"},
                    new ServiceCustomer() {CustomerName = "Jill"}
                }
            }, null, "GetDetails");

            Get("/customers", _ => new[]
            {
                new ServiceCustomer() {CustomerName = "Jack"},
                new ServiceCustomer() {CustomerName = "Jill"}
            }, null, "GetCustomers");

            Get("/customers/{name}", _ => new ServiceCustomer() {CustomerName = "Jack"}, null, "GetCustomer");

            Post("/customer/{service}", parameters =>
            {
                var result = this.Bind<ServiceCustomer>();
                return result;

            }, null, "PostNewCustomer");


        }
    }
}