using Nancy.Swagger.Demo.Models;
using Nancy.Swagger.Services;

namespace Nancy.Swagger.Demo.ModelDataProviders
{
    public class RoleModelDataProvider : ISwaggerModelDataProvider
    {
        public SwaggerModelData GetModelData()
        {
            return SwaggerModelData.ForType<Role>(with =>
            {
                with.Description("A role within the system");
            });
        }
    }
}