## 使用手册

---

### gRPC 客户端

> 提示：本文档中出现的示例代码，如果没有特别说明，均以 .NET Core 3.1 为例，其他版本写法可能会略有不同。

项目需要是一个 ASP.NET Core 类型的工程，并引入 **SKIT.WebX.Grpc** 和 **Grpc.Net.ClientFactory**。

---

#### 依赖注入

可按照[《Microsoft Docs - .NET Core 中的 gRPC 客户端工厂集成》](https://docs.microsoft.com/zh-cn/aspnet/core/grpc/clientfactory?view=aspnetcore-3.1)一文来实现 gRPC 客户端。

**SKIT.WebX.Grpc** 提供了 `IGrpcClientOptions` 接口，可以方便的依赖注入一些 gRPC 客户端所需的参数。

``` csharp
// 定义 gRPC 客户端配置项实体类
public class GrpcClientOptions ：IOptions<GrpcClientOptions>, IGrpcClientOptions
{
    GrpcClientOptions IOptions<GrpcClientOptions>.Value => this;

    // gRPC Service 地址
    public Uri BaseAddress { get; set; }

    // 是否忽略证书错误（一般用于自签名证书）
    public bool IgnoreCertificateErrors { get; set; }

    // 连接通道最大发送字节数
    public int? MaxChannelSendMessageSize { get; set; }

    // 连接通道最大接收字节数
    public int? MaxChannelReceiveMessageSize { get; set; }
}

// 依赖注入
services.AddGrpcClient<SampleService.SampleServiceClient, GrpcClientOptions>();
```