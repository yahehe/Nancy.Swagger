using Nancy.Swagger.Services;

namespace Nancy.Swagger
{
    public class Registrations : Bootstrapper.Registrations
    {
        public Registrations()
        {
            RegisterWithDefault<ISwaggerMetadataConverter>(typeof (DefaultSwaggerMetadataConverter));
        }
    }
}