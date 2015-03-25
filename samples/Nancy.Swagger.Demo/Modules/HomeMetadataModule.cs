using Nancy.Metadata.Module;
using Nancy.Swagger.Demo.Models;
using Swagger.ObjectModel;

namespace Nancy.Swagger.Demo.Modules
{
    public class HomeMetadataModule : MetadataModule<PathItem>
    {
        public HomeMetadataModule()
        {
            Describe["GetUsers"] = description => description.AsSwagger(
                with => with.Operation(
                    op => op.OperationId("GetUsers")
                            .Summary("The list of users")
                            .Description("This returns a list of users from our awesome app")
                            .Response(r => r.Schema<User>().Description("The list of users"))));


            Describe["PostUsers"] =
                description =>
                description.AsSwagger(
                    with =>
                    with.Operation(
                        op =>
                        op.OperationId("PostUsers")
                          .Summary("Create a User")
                          .Description("Creates a user with the shown schema for our awesome app")
                          .Response(201, r => r.Description("Created a User"))
                          .Response(422, r => r.Description("Invalid input"))
                          .BodyParameter(p => p.Description("A User object").Name("user").Schema<User>())));
        }
    }
}