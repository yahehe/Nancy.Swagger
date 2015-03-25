using Nancy.Swagger.Demo.Models;
using Nancy.Swagger.Services;

namespace Nancy.Swagger.Demo.ModelDataProviders
{
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