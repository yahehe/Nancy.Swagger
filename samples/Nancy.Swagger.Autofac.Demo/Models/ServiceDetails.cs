using System.Collections.Generic;
using Nancy.Swagger.Annotations.Attributes;

namespace Nancy.Swagger.Autofac.Demo.Models
{
    [Model("The Details of this service")]
    public class ServiceDetails
    {
        [ModelProperty(Description = "The name of the service", Required = true)]
        public string Name { get; set; }

        [ModelProperty(Description = "The id of the service", Required = true, Minimum = 1)]
        public int ServiceNumber { get; set; }

        [ModelProperty(Description = "The owner of the service")]
        public ServiceOwner Owner { get; set; }

        [ModelProperty(Description = "The customers of the service")]
        public IList<ServiceCustomer> Customers { get; set; }

    }

    public class ServiceOwner
    {
        public string CompanyName { get; set; }
        public string CompanyContactEmail { get; set; }
        public string CompanyPhoneNumber { get; set; }
    }

    public class ServiceCustomer
    {
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
    }
}