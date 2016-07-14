using Nancy.Metadata.Modules;
using Nancy.Swagger.Demo.Models;
using Nancy.Swagger.Modules;
using Nancy.Swagger.Services.RouteUtils;
using Swagger.ObjectModel;

namespace Nancy.Swagger.Demo.Modules
{
    public class AnotherMetadataModule : SwaggerMetadataModule
    {
        public AnotherMetadataModule(ISwaggerModelCatalog modelCatalog) :base (modelCatalog)
        {
            RouteDescriber.DescribeRoute("AnotherHome", "", "Get Home", new[]
            {
                new HttpResponseMetadata {Code = 200, Message = "OK"}
            });

            RouteDescriber.AddAdditionalModels(typeof(ServiceOwner), typeof(ServiceCustomer));
            RouteDescriber.DescribeRoute<ServiceDetails>("GetDetails", "", "Get Details", new[]
            {
                new HttpResponseMetadata {Code = 200, Message = "OK"}
            });

            RouteDescriber.DescribeRouteWithParams<ServiceCustomer>("PostNewCustomer", "", "Add a new customer", new[]
            {
                new HttpResponseMetadata { Code = 200, Message = "Customer Added"},
            }, new[]
            {
                new Parameter{Name = "name" , In = ParameterIn.Path, Required = true, Description = "The customer's name", Default = "John Smith", Type = "string" },
            });
        }
    }
}