﻿using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace SKIT.WebX.RESTfulSample
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
