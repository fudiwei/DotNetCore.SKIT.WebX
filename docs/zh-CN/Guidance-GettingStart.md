## 使用手册

---

### 开始上手

> 提示：本文档中出现的示例代码，如果没有特别说明，均以 .NET Core 3.1 为例，其他版本写法可能会略有不同。

项目需要是一个 ASP.NET Core 类型的工程，并引入 **STEP.WebX.Core**。

---

#### 构建主机

在 `Program.cs` 文件中，添加如下代码：

``` csharp
using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace STEP.WebX.RESTful
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
```

这样，就启用了一个 Kestrel 主机，并初始化了一些与之相关的参数。当然，你也可以在构建过程中自行调整它们。

与此同时，还会自动注册系统支持的字符集，以支持中文等字符的显示（请参考[《Microsoft Docs - System.Text.Encoding.RegisterProvider 方法》](https://docs.microsoft.com/zh-cn/dotnet/api/system.text.encoding.registerprovider?view=netcore-3.1)）。

---

#### 配置注入

**STEP.WebX** 封装了动态配置的依赖注入方法，可以很简单将配置项映射为强类型。

注意，强类型实体需实现 `Microsoft.Extensions.Options.IOptions<TOptions>` 接口。

只需在 `Startup.cs` 的文件中，在依赖注入时添加：

``` csharp
services.RegisterOptions<TOptions>();
```

就会自动从配置源中读取配置，并映射成实体。

通常情况下，将会从 Host 初始化时指定的配置源读取配置；如果需要额外指定配置源，那么可以：

``` csharp
IConfiguration configuration = GetConfiurationFromSomewhere();
services.RegisterOptions<TOptions>(configuration);
```

默认将会读取与强类型实体同名的配置项，例如，存在一个配置源 `appsettings.json`：

``` json
{
    "Sample": {
        "Name": "Hello World."
    }
}
```

那么将映射到同名类型 `SampleOptions` 上（结尾的 `Options` 可省略）：

``` csharp
public class SampleOptions
{
    public string Name { get; set; }
}

services.RegisterOptions<SampleOptions>();
```

如果强类型实体类名与配置源中的名称不一致，那么可以：

``` csharp
services.RegisterOptions<SampleOptions>("SampleAlias");
```

---

#### 后台任务

ASP.NET Core 中提供了 `Microsoft.Extensions.Hosting.IHostedService` 这一接口，可用于实现一个后台任务。

**STEP.WebX** 在此基础之上，封装了 `BackgroundSchedulerService` 抽象类型，支持后台任务轮询，并可设置任务延时启动、轮询间隔，可以自动注入 ASP.NET Core 的生命周期和依赖项。

本项目根目录下的 `test\STEP.WebX.RESTfulSample\Services\Background\ClockingService` 文件，给出了一个后台任务的示例。

如果对后台任务的定时执行有更细粒度的控制需求，可以配合基于 [Quartz.NET](https://www.quartz-scheduler.net/) 的 [Quartz.Plugins.Attributes](https://github.com/fudiwei/Quartz.Plugins.Attributes) 来使用。

---

#### 扩展方法

**STEP.WebX** 封装了一些 Web API 中常用的扩展方法，可供开发者使用。

* 将 HTTP 请求的标头集合转换为字典：

``` csharp
IDictionary<string, string> headerMaP = HttpContext.Request.Headers.ToDictionary();
```

* 尝试获取 HTTP 请求标头中指定名称的值（名称忽略大小写；如果有多个值将只返回第一个）：

``` csharp
bool hasVal = HttpContext.Request.Headers.TryGetValue("Authorization", out string val);
```

* 获取发起请求的客户端 IP 地址（优先返回 `X-Forwarded-For` / `X-Original-Forwarded-For`，以支持通过反向/正向代理）：

``` csharp
string clientIp = HttpContext.Request.GetClientIp();
```

* 获取分布式链路请求 ID（优先返回 `X-Request-Id`）：

``` csharp
string requestId = HttpContext.Request.GetRequestId();
```

* 异步获取 HTTP 请求正文内容，并以字节数组的方式读取：

``` csharp
byte[] data = await HttpContext.Request.ReadBodyAsByteArrayAsync();
```

* 异步获取 HTTP 请求正文内容，并以字符串的方式读取：

``` csharp
string data = await HttpContext.Request.ReadBodyAsStringAsync();
```

* 异步获取 HTTP 请求正文内容，并以 JSON 的方式读取（依赖于 [Newtonsoft.Json](https://www.newtonsoft.com/json)）：

``` csharp
JToken data = await HttpContext.Request.ReadBodyAsJsonAsync();
```