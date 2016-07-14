using Nancy.ModelBinding;
using Nancy.Swagger.Demo.Models;

namespace Nancy.Swagger.Demo.Modules
{
    public class ServiceDetailsModule : NancyModule
    {
        public ServiceDetailsModule() : base("/service")
        {
            Get["ServiceHome", "/"] = _ => "Hello again, Swagger!";

            Get["GetDetails", "/details"] = _ => new[] { new ServiceDetails() { Name = "Nancy Swagger Service" } };

            Post["PostNewCustomer", "/customer/{name}"] = parameters => new ServiceCustomer() { CustomerName = parameters.name };


        }
    }
}