using Nancy.Swagger.Services;
using Swagger.ObjectModel;

namespace Nancy.Swagger.Autofac.Demo.TagProviders
{


    public class UserTagProvider : ISwaggerTagProvider
    {
        public Tag GetTag()
        {
            return new Tag()
            {
                Description = "Operations related to users",
                Name = "Users"
            };
        }
    }
}