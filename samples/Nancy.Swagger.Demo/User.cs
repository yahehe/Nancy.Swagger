namespace Nancy.Swagger.Demo
{
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        [Required]
        public string Name { get; set; }

        [Range(1, 100)]
        public int Age { get; set; }
    }
}