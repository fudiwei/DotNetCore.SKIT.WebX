using System;
using Microsoft.Extensions.Hosting;

namespace STEP.WebX.GrpcSample
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
