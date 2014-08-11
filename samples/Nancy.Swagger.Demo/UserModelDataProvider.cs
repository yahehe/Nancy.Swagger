namespace Nancy.Swagger.Demo
{
    using Nancy.Swagger.Services;

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

    public class AddressModelDataProvider : ISwaggerModelDataProvider
    {
        public SwaggerModelData GetModelData()
        {
            return SwaggerModelData.ForType<Address>(with =>
            {
                with.Description("An address of a user");
                with.Property(x => x.Address1)
                    .Description("First Line of Address")
                    .Required(true);
            });
        }
    }
}