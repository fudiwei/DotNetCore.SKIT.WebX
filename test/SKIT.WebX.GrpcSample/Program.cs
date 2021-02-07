using System;
using Microsoft.Extensions.Hosting;

namespace SKIT.WebX.GrpcSample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            HostBuilderHelper
                .CreateDefaultBuilder<Startup>(args)
                .Build()
                .Run();
        }
    }
}
