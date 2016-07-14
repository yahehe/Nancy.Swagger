using Nancy.ModelBinding;
using Nancy.Swagger.Demo.Models;

namespace Nancy.Swagger.Demo.Modules
{
    public class ServiceDetailsModule : NancyModule
    {
        public ServiceDetailsModule() : base("/service")
        {
            Get["ServiceHome", "/"] = _ => "Hello again, Swagger!";

            Get["GetDetails", "/details"] = _ => new ServiceDetails()
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
            } ;

            Get["GetCustomers", "/customers"] = _ => new[]
            {
                new ServiceCustomer() {CustomerName = "Jack"},
                new ServiceCustomer() {CustomerName = "Jill"}
            };

            Post["PostNewCustomer", "/customer/{service}"] = parameters =>
            {
                var result = this.BindAndValidate<ServiceCustomer>();

                if (!ModelValidationResult.IsValid)
                {
                    return Negotiate.WithModel(new { Message = parameters.service + " failed to add user." })
                        .WithStatusCode(HttpStatusCode.UnprocessableEntity);
                }
                return result;

            };


        }
    }
}