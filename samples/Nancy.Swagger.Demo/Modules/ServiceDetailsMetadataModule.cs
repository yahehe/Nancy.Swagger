using System.Collections.Generic;
using Nancy.Metadata.Modules;
using Nancy.Swagger.Demo.Models;
using Nancy.Swagger.Modules;
using Nancy.Swagger.Services;
using Nancy.Swagger.Services.RouteUtils;
using Swagger.ObjectModel;

namespace Nancy.Swagger.Demo.Modules
{
    public class ServiceDetailsMetadataModule : SwaggerMetadataModule
    {
        public ServiceDetailsMetadataModule(ISwaggerModelCatalog modelCatalog, ISwaggerTagCatalog tagCatalog) :base (modelCatalog, tagCatalog)
        {
            RouteDescriber.AddBaseTag(new Tag()
            {
                Description = "Operations for handling the service",
                Name = "Service"
            });

            var customerSubTag = new Tag()
            {
                Name = "Service/Customers",
                Description = "Operations of 'Service' relating to Customers"
            };

            RouteDescriber.DescribeRoute("ServiceHome", "", "Get Home", new[]
            {
                new HttpResponseMetadata {Code = 200, Message = "OK"}
            });

            RouteDescriber.AddAdditionalModels(typeof(ServiceOwner), typeof(ServiceCustomer));
            RouteDescriber.DescribeRoute<ServiceDetails>("GetDetails", "", "Get Details", new[]
            {
                new HttpResponseMetadata {Code = 200, Message = "OK"}
            });

            RouteDescriber.DescribeRoute<IEnumerable<ServiceCustomer>>("GetCustomers", "", "Get Customers", new[]
            {
                new HttpResponseMetadata {Code = 200, Message = "OK"}
            }, new[]
            {
                customerSubTag
            });

            RouteDescriber.DescribeRouteWithParams<ServiceCustomer>("PostNewCustomer", "", "Add a new customer", new[]
            {
                new HttpResponseMetadata { Code = 200, Message = "Customer Added"},
            }, new[]
            {
                new Parameter{Name = "service", In = ParameterIn.Path, Required = true, Description = "The service's name", Default = "Nancy Swagger Service", Type = "string" },
                new BodyParameter<ServiceCustomer>(ModelCatalog) {Name = "user",  Required = true, Description = "The user"}, 
            }, new []
            {
                customerSubTag
            });
        }
    }

}