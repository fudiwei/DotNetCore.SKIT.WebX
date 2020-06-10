# STEP.WebX

一个分布式 Web API 微服务开发框架。

本库以 .NET Core 为目标平台。

---

## 特性

* 封装了常用的请求和响应的处理方法。
* 支持后台任务。
* 支持 RESTful Web API。
* 支持 gRPC Web API。

---

## 用法

### 示例代码

``` CSharp
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        // 依赖注入 RESTful 相关
        services.AddWebXRESTful();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // 启用 RESTful Web API
        app.UseWebXRESTful();
    }
}
```

### 使用手册

[点此](./Guidance.md)查看完整的使用手册。

### 注释

每个公共类、方法和属性已包含详细的文档注释。

😟 由于精力问题，这里只提供了英文版本的文档注释。

🙂 欢迎开发者们贡献其他语言版本。