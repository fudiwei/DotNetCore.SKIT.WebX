using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace STEP.WebX.RESTfulSample
{
    using RESTful;

    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
        }

#if !NETCOREAPP3_0
        public IServiceProvider ConfigureServices(IServiceCollection services)
#else
        public void ConfigureServices(IServiceCollection services)
#endif
        {
            services.AddWebXRESTful();
            services.AddHostedServices();

#if !NETCOREAPP3_0
            return services.BuildServiceProvider();
#endif
        }

#if !NETCOREAPP3_0
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
#else
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
#endif
        {
            app.UseWebXRESTful();
        }
    }
}
