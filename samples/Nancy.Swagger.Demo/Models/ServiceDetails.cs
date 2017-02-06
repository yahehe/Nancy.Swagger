using System;
using System.Collections.Generic;

namespace Nancy.Swagger.Demo.Models
{
    public class ServiceDetails
    {
        public string Name { get; set; }
        public int ServiceNumber { get; set; }
        public ServiceOwner Owner { get; set; }
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