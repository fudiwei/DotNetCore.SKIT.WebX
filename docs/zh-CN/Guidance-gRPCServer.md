## 使用手册

---

### gRPC 启动服务

> 提示：本文档中出现的示例代码，如果没有特别说明，均以 .NET Core 3.1 为例，其他版本写法可能会略有不同。

项目需要是一个 ASP.NET Core 类型的工程，并引入 **SKIT.WebX.Grpc** 和 **Grpc.AspNetCore**。

---

#### 依赖注入

在 `Startup.cs` 文件中，添加如下代码：

``` csharp
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace SKIT.WebX.Grpc
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddWebXGrpc();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseWebXGrpx();
        }
    }
}
```

这样，就讲启用了 RESTful Web API 模式，支持基于 CORS 浏览器跨域请求。

---

#### 参数配置

**SKIT.WebX.Grpc** 实际上封装了认证、授权、gRPC 等一些中间件，大多数情况下，你都不需要对其配置作出改变。

如果需要调整参数，可以在依赖注入时进行。

``` csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddWebXGrpc(settings => 
    {
        settings.SetupAuthenticationOptions = (builder) =>
        {
            // 认证依赖项配置
        };

        settings.ConfigureAuthenticationBuilder = (builder) =>
        {
            // 认证建造者配置
        };

        settings.SetupAuthorizationOptions = (builder) =>
        {
            // 授权依赖项配置
        };

        settings.SetupGrpcServiceOptions = (app) => 
        {
            // Grpc Service 依赖项配置
        };
    });
}

public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    services.UseWebXGrpx(settings => 
    {
        settings.ExecuteBeforeUseEndpoints = (app) =>
        {
            // 在启用端点路由前执行，可用于添加授权、鉴权等中间件
        };

        settings.ConfigureEndpointRouteBuilder = (endpoints) =>
        {
            // 在启用端点路由时执行，可用于添加路由等操作
        };
    });
}
```

---

#### 定义 gRPC 端点

可按照[《Microsoft Docs - .NET Core 上的 gRPC 的简介》](https://docs.microsoft.com/en-us/aspnet/core/grpc/?view=aspnetcore-3.1)一文来实现 gRPC Web API 服务。

但如果需要 **SKIT.WebX.Grpc** 能自动将程序集下所有 gRPC Service 自动注入，则还需要添加 `GrpcService` 的特性。

``` csharp
using System;
using Grpc.Core;

namespace SKIT.WebX.Grpc
{
    [GrpcService]
    public class SampleGrpcService : SampleService.SampleServiceBase
    {
    }
}
```

---

#### RESTful 与 gRPC 混合使用

由于 **SKIT.WebX.RESTful** 和 **SKIT.WebX.Grpc** 各自定义了不同的中间件管道顺序，所以当二者需要同时启用时，需要在 `Startup.cs` 中做特殊处理。

``` csharp
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddWebXRESTful();
        services.AddWebXGrpc();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        /**
         * 错误做法！
         */
        app.UseWebXRESTful();
        app.UseWebXGrpc();

        /**
         * 正确做法。
         */
        app.UseWebXRESTful(settings =>
        {
            settings.ConfigureEndpointRouteBuilder = (endpoints) =>
            {
                endpoints.MapGrpcServices();
            };
        });
    }
}
```