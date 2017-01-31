using System.Collections.Generic;
using Nancy.ModelBinding;
using Nancy.Swagger.Annotations.Attributes;
using Nancy.Swagger.Demo.Models;
using Swagger.ObjectModel;

namespace Nancy.Swagger.Demo.Modules
{
    public class ServiceDetailsModule : NancyModule
    {
        public ServiceDetailsModule() : base("/service")
        {
            Get("/", _ => GetHome(), null, "ServiceHome");

            Get("/details", _ => GetServiceDetails(), null, "GetDetails");

            Get("/customers", _ => GetServiceCustomers(), null, "GetCustomers");

            Get("/customers/{name}", _ => GetServiceCustomer(), null, "GetCustomer");

            Post("/customer/{service}", parameters => PostServiceCustomer(this.Bind<ServiceCustomer>()), null, "PostNewCustomer");


        }


        [Route("ServiceHome")]
        [Route(HttpMethod.Get, "/")]
        [SwaggerResponse(HttpStatusCode.OK, Message = "OK")]
        private string GetHome()
        {
            return "Hello again, Swagger!";
        }


        [Route("GetDetails")]
        [Route(HttpMethod.Get, "/details")]
        [SwaggerResponse(HttpStatusCode.OK, Message = "OK", Model = typeof(ServiceDetails))]
        private ServiceDetails GetServiceDetails()
        {
            return new ServiceDetails()
            {
                Name = "Nancy Swagger Service",
                Owner = new ServiceOwner()
                {
                    CompanyName = "Swagger Example Inc.",
                    CompanyContactEmail = "company@swaggerexample.inc"
                },
                Customers = new[]
                {
                    new ServiceCustomer() {CustomerName = "Jack"},
                    new ServiceCustomer() {CustomerName = "Jill"}
                }
            };
        }
        
        [Route("GetCustomers")]
        [Route(HttpMethod.Get, "/customers")]
        [SwaggerResponse(HttpStatusCode.OK, Message = "OK", Model = typeof(IEnumerable<ServiceCustomer>))]
        private ServiceCustomer[] GetServiceCustomers()
        {
            return new[]
            {
                new ServiceCustomer() {CustomerName = "Jack"},
                new ServiceCustomer() {CustomerName = "Jill"}
            };
        }

        [Route("GetCustomer")]
        [Route(HttpMethod.Get, "/customers/{name}")]
        [SwaggerResponse(HttpStatusCode.OK, Message = "OK", Model = typeof(ServiceCustomer))]
        private ServiceCustomer GetServiceCustomer()
        {
            return new ServiceCustomer() { CustomerName = "Jack" };
        }

        [Route("PostNewCustomer")]
        [Route(HttpMethod.Post, "/customer/{service}")]
        [SwaggerResponse(HttpStatusCode.OK, Message = "OK", Model = typeof(ServiceCustomer))]
        [Route(Produces = new[] { "application/json" })]
        [Route(Consumes = new[] { "application/json", "application/xml" })]
        private ServiceCustomer PostServiceCustomer([RouteParam(ParameterIn.Body, BodyParamType = typeof(ServiceCustomer))] ServiceCustomer customer)
        {
            return customer;
        }
    }
}