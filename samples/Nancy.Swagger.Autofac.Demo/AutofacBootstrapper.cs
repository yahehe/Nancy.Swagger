using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Autofac;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.Autofac;
using Nancy.Swagger.Annotations;
using Nancy.Swagger.Autofac.Demo.Modules;
using Nancy.Swagger.Services;
using Swagger.ObjectModel;

namespace Nancy.Swagger.Autofac.Demo
{
    public class AutofacBootstrapper : AutofacNancyBootstrapper
    {
        public AutofacBootstrapper() : base()
        {
        }

        protected override void ConfigureApplicationContainer(ILifetimeScope container)
        {
            SwaggerMetadataProvider.SetInfo("Nancy Swagger w/ AutoFac Example", "v0", "Our awesome service", new Contact()
            {
                EmailAddress = "exampleEmail@example.com"
            });

            SwaggerAnnotationsConfig.ShowOnlyAnnotatedRoutes = true;
            this.ApplicationPipelines.AfterRequest.AddItemToEndOfPipeline(x => x.Response.Headers.Add("Access-Control-Allow-Origin", "*"));
        }
    }
}
