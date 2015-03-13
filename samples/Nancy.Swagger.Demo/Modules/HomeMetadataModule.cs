using System.Collections.Generic;
using Nancy.Metadata.Module;
using Nancy.Swagger.Demo.Models;
using Swagger.ObjectModel;

namespace Nancy.Swagger.Demo.Modules
{
    public class HomeMetadataModule : MetadataModule<PathItem>
    {
        public HomeMetadataModule()
        {
            Describe["GetUsers"] = description => description.AsSwagger(with =>
            {
                with.Operation(op =>
                               {
                                   op.OperationId = "GetUsers";
                                   op.Summary = "The list of users";
                                   op.Description = "This returns a list of users from our awesome app";
                                   op.Responses = new Dictionary<string, global::Swagger.ObjectModel.Response>()
                                                  {
                                                      {
                                                          "default", new global::Swagger.ObjectModel.Response()
                                                                     {
                                                                         Schema = new Schema()
                                                                                  {
                                                                                      Type = typeof(User).ToString()
                                                                                  }
                                                                     }
                                                      }
                                                  };
                               });
            });

            Describe["PostUsers"] = description => description.AsSwagger(with =>
            {
                with.Operation(op =>
                               {
                                   op.OperationId = "PostUsers";
                                   op.Summary = "Create a User";
                                   op.Description = "Creates a user with the shown schema for our awesome app";
                                   op.Responses = new Dictionary<string, global::Swagger.ObjectModel.Response>()
                                                  {
                                                      {
                                                          201.ToString(), new global::Swagger.ObjectModel.Response()
                                                                          {
                                                                              Description = "Created a User"
                                                                          }
                                                      },
                                                      {
                                                          422.ToString(), new global::Swagger.ObjectModel.Response()
                                                                          {
                                                                              Description = "Invalid input"
                                                                          }
                                                      }
                                                  };
                                   op.Parameters = new List<Parameter>()
                                                   {
                                                       new BodyParameter()
                                                       {
                                                           Description = "A User object",
                                                           Type = typeof(User).ToString(),
                                                           Required = true
                                                       }
                                                   };
                               });
            });
        }
    }
}