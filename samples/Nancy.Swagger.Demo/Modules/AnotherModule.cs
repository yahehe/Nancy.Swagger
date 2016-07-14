using Nancy.ModelBinding;
using Nancy.Swagger.Demo.Models;

namespace Nancy.Swagger.Demo.Modules
{
    public class AnotherModule : NancyModule
    {
        public AnotherModule() : base("/anotherModule")
        {
            Get["AnotherHome", "/"] = _ => "Hello again, Swagger!";

            Get["GetDetails", "/details"] = _ => new[] { new ServiceDetails() { Name = "Nancy Swagger Service" } };

            Post["PostNewCustomer", "/customer/{name}"] = parameters => new[] { new ServiceCustomer() { CustomerName = parameters.name } };


        }
    }
}