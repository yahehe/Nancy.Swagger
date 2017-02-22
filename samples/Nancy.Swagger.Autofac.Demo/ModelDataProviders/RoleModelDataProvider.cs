using Nancy.Swagger.Autofac.Demo.Models;
using Nancy.Swagger.Services;

namespace Nancy.Swagger.Autofac.Demo.ModelDataProviders
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