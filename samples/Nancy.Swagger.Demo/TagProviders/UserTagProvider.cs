using System;
using Swagger.ObjectModel;
using Nancy.Swagger.Services;

namespace Nancy.Swagger.Demo.TagProviders
{
    public class UserTagProvider : ISwaggerTagProvider
    {
        public Tag GetTag()
        {
            return new Tag()
            {
                Description = "Operations related to users",
                Name = "Users",
                ExternalDocumentation = new ExternalDocumentation() {Description = "Check our documentation", Url = "http://nancyfx.org/"}
            };
        }
    }
}