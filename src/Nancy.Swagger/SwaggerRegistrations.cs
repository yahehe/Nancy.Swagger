using Nancy.Bootstrapper;
using Nancy.Swagger.Services;

namespace Nancy.Swagger
{
    [SwaggerApi]
    public class SwaggerRegistrations : Registrations
    {
        public SwaggerRegistrations()
        {
            RegisterWithDefault<ISwaggerMetadataConverter>(typeof(DefaultSwaggerMetadataConverter));
        }
    }
}