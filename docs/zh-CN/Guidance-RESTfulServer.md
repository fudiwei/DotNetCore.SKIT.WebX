## 使用手册

---

### RESTful 启动服务

> 提示：本文档中出现的示例代码，如果没有特别说明，均以 .NET Core 3.1 为例，其他版本写法可能会略有不同。

项目需要是一个 ASP.NET Core 类型的工程，并引入 **SKIT.WebX.RESTful**。

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

namespace SKIT.WebX.RESTful
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddWebXRESTful();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseWebXRESTful();
        }
    }
}
```

这样，就讲启用了 RESTful Web API 模式，支持基于 CORS 浏览器跨域请求。

---

#### 参数配置

**SKIT.WebX.RESTful** 实际上封装了健康检查、前向代理、认证、授权、MVC、CORS 等一些中间件，大多数情况下，你都不需要对其配置作出改变。

如果需要调整参数，可以在依赖注入时进行。

``` csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddWebXRESTful(settings => 
    {
        settings.SetupCorsOptions = (options) =>
        {
            // CORS 依赖项配置
        };

        settings.SetupForwardedHeadersOptions = (options) =>
        {
            // 前向代理依赖项配置
        };

        settings.ConfigureHealthChecksBuilder = (builder) =>
        {
            // 健康检查建造者配置
        };

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

        settings.SetupMvcOptions = (builder) =>
        {
            // MVC 依赖项配置
        };

        settings.JsonSerializer = WebXRESTfulServiceSettings.JsonSerializers.NewtonsoftJson; // JSON 序列化器。默认使用 Newtonsoft.Json
    });
}

public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    services.UseWebXRESTful(settings => 
    {
        settings.SetupRESTfulErrorOptions = (options) => 
        {
            // RESTful 错误处理中间件配置
        };

        settings.SetupHealthCheckOptions = (options) => 
        {
            // 健康检查中间件配置
        };

        settings.SetupForwardedHeadersOptions = (options) => 
        {
            // 前向代理中间件配置
        };

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

#### 定义 RESTful 端点

可按照[《Microsoft Docs - 在 ASP.NET Core MVC 中使用控制器处理请求》](https://docs.microsoft.com/zh-cn/aspnet/core/mvc/controllers/actions?view=aspnetcore-3.1)一文来实现 RESTful Web API 服务。

但如果需要 **SKIT.WebX.RESTful** 能自动将程序集下所有 Controller 自动注入，则控制器还需要继承自 `RESTfulControllerBase` 类型。

``` csharp
using System;
using Microsoft.AspNetCore.Mvc;

namespace SKIT.WebX.RESTful
{
    public class SampleController : RESTfulControllerBase
    {
    }
}
```