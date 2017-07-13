using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nancy.Owin;
using Nancy.TinyIoc;
using System.IO;

namespace Nancy.Swagger.NetCore.Demo
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseOwin(x => x.UseNancy(o =>
            {
                o.Bootstrapper = new StandaloneOutputBootstrapper("swagger.json");
            }));
        }

        private class StandaloneOutputBootstrapper : Swagger.Demo.Bootstrapper
        {
            private bool hasRun = false;
            private readonly string jsonPath;

            public StandaloneOutputBootstrapper(string jsonPath)
            {
                this.jsonPath = jsonPath;
            }

            protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
            {
                base.ConfigureRequestContainer(container, context);

                if (!hasRun)
                {
                    var swagger = Standalone.Generate(container, context);
                    File.WriteAllText(jsonPath, swagger.ToJson());
                    hasRun = true;
                }
            }
        }
    }
}
