using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy.Swagger.Services;
using Swagger.ObjectModel;

namespace Nancy.Swagger.Demo.TagProviders
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