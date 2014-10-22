using Nancy.Bootstrapper;
using Nancy.Swagger.Annotations.Enrichers;

namespace Nancy.Swagger.Annotations
{
    public class SwaggerAnnotationsRegistrations : Registrations
    {
        public SwaggerAnnotationsRegistrations()
        {
            RegisterWithDefault<ISwaggerDataEnricherCatalog>(typeof(DefaultSwaggerDataEnricherCatalog));
            RegisterAll<ISwaggerDataEnricher>();
        }
    }
}