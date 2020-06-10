using System;
using System.IO;
using System.Text;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace STEP.WebX
{
    /// <summary>
    /// 
    /// </summary>
    public static class HostBuilderHelper
    {
#if NETCORE_2_X
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHostBuilder CreateDefaultBuilder(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            return WebHost
                .CreateDefaultBuilder(args)
                .UseContentRoot(Environment.CurrentDirectory)
                .UseSockets()
                .UseKestrel(options =>
                {
                    options.Limits.KeepAliveTimeout = TimeSpan.FromSeconds(30);
                    options.Limits.RequestHeadersTimeout = TimeSpan.FromSeconds(30);
                    options.Limits.MaxRequestBodySize = Constants.MAX_HTTP_MESSAGE_SIZE;
                });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TStartup"></typeparam>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHostBuilder CreateDefaultBuilder<TStartup>(string[] args)
            where TStartup : class
        {
            return CreateDefaultBuilder(args)
                .UseStartup<TStartup>();
        }

#else
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateDefaultBuilder(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            return Host
                .CreateDefaultBuilder(args)
                .UseContentRoot(Environment.CurrentDirectory)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseKestrel(options =>
                    {
                        options.Limits.KeepAliveTimeout = TimeSpan.FromSeconds(30);
                        options.Limits.RequestHeadersTimeout = TimeSpan.FromSeconds(30);
                        options.Limits.MaxRequestBodySize = Constants.MAX_HTTP_MESSAGE_SIZE;
                    });
                    webBuilder.UseContentRoot(Environment.CurrentDirectory);
                });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TStartup"></typeparam>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateDefaultBuilder<TStartup>(string[] args)
            where TStartup : class
        {
            return CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<TStartup>();
                });
        }
#endif
    }
}
