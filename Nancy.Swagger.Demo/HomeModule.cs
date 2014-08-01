namespace Nancy.Swagger.Demo
{
    using System.Collections.Generic;
    using System.ComponentModel;

    using Nancy;
    using Nancy.ModelBinding;
    using Nancy.Swagger.Services;
    using System.ComponentModel.DataAnnotations;

    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["Home", "/"] = parameters => "Hello!";
            Get["GetUsers", "/users"] = _ => new List<User>(){new User(){Name = "Vincent Vega", Age = 45}};
            Post["PostUsers", "/users"] = _ =>
            {
                var result = this.BindAndValidate<User>();

                if (!this.ModelValidationResult.IsValid)
                {
                    return Negotiate.WithModel(new { Message = "Oops" })
                        .WithStatusCode(HttpStatusCode.UnprocessableEntity);
                }

                return Negotiate.WithModel(result).WithStatusCode(HttpStatusCode.Created);
            };
        }
    }

    public class User
    {
        [Required]
        public string Name { get; set; }

        [Range(1, 100)]
        public int Age { get; set; }

    }

    public class UserModelDataProvider : ISwaggerModelDataProvider
    {
        public SwaggerModelData GetModelData()
        {
            return SwaggerModelData.ForType<User>(with =>
            {
                with.Description("A user of our awesome system!");
                with.Property(x => x.Name)
                    .Description("The user's name")
                    .Required(true);
                with.Property(x => x.Age)
                    .Description("The user's age")
                    .Required(true)
                    .Minimum(1)
                    .Maximum(100);

            });
        }
    }
}