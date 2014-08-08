namespace Nancy.Swagger.Demo
{
    using Nancy.Metadata.Module;

    public class HomeMetadataModule : MetadataModule<SwaggerRouteData>
    {
        public HomeMetadataModule()
        {
            Describe["GetUsers"] = description => description.AsSwagger(with =>
            {
                with.ResourcePath("/users");
                with.Summary("The list of users");
                with.Notes("This returns a list of users from our awesome app");
            });

            Describe["PostUsers"] = description => description.AsSwagger(with =>
            {
                with.ResourcePath("/users");
                with.Summary("Create a User");
                with.Response(201, "Created a User");
                with.Response(422, "Invalid input");
                with.Model<User>();
                with.BodyParam<User>("A User object", required:true);
                with.Notes("Creates a user with the shown schema for our awesome app");
            });
        }
    }
}