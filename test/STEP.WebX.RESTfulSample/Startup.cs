using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace STEP.WebX.RESTfulSample
{
    using RESTful;

    public class Startup
    {
        private IConfiguration Configutaion { get; }

        public Startup(IConfiguration configuration)
        {
            Configutaion = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddWebXRESTful();
            services.AddHostedServices();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseWebXRESTful();
        }
    }
}
