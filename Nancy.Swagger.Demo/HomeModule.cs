namespace Nancy.Swagger.Demo
{
    using Nancy;
    using Nancy.ModelBinding;
    using System.Collections.Generic;

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
}