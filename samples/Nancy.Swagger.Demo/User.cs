namespace Nancy.Swagger.Demo
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        [Required]
        public string Name { get; set; }

        [Range(1, 100)]
        public int Age { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Address Address { get; set; }

        public Role Role { get; set; }
    }

    public class Address
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Town { get; set; }
        public string County { get; set; }
        public string  PostCode { get; set; }
    }

    public enum Role
    {
        User = 0,
        Admin = 1,
        God = 2
    }
}