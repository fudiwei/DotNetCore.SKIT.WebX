## 使用手册

---

### RESTful Web API

> 提示：本文档中出现的示例代码，如果没有特别说明，均以 .NET Core 3.1 为例，其他版本写法可能会略有不同。

项目需要是一个 ASP.NET Core 类型的工程，并引入 **STEP.WebX.RESTful**。

---

#### 请求谓词

RESTful 中常用 `GET`、`POST`、`PUT`、`PATCH`、`DELETE` 等谓词，用来表示增删改查相关的动作。

**STEP.WebX.RESTful** 提供了相应的常量值，可用于限定指定路由所接受的谓词。

``` csharp
[AcceptVerbs(RESTfulVerbs.Post)]
public Task<IActionResult> SampleAction()
{
    // 该 Action 将只接受 POST 请求
}

[AcceptVerbs(RESTfulVerbs.Put, RESTfulVerbs.Patch)]
public Task<IActionResult> SampleAction()
{
    // 该 Action 将即能接受 PUT 请求、又能接受 PATCH 请求
}
```

---

#### 模型验证

**STEP.WebX.RESTful** 是基于 ASP.NET Core MVC 实现的，所有 ASP.NET Core MVC 支持的请求模型验证特性，**STEP.WebX.RESTful** 也都支持（请参考[《Microsoft Docs - ASP.NET Core MVC 中的模型验证》](https://docs.microsoft.com/zh-cn/aspnet/core/mvc/models/validation?view=aspnetcore-3.1)）。

但因为异常状态的统一转换（见下文），ASP.NET Core 内置的这些特性，在请求模型验证失败时的错误信息不够详细具体。为了更明确地说明错误原因，**STEP.WebX.RESTful** 也提供了一些请求模型验证特性。

下面给出一些具体的示例：

* **非空限定**：不可为 `null`，且字符串空白时也视为 `null`：

``` csharp
public class SampleModel
{
    [FieldNotNull(AllowEmptyStrings = false)]
    public string Id { get; set; }
}
```

* **长度限定**：字符串长度不能超过 `10`：

``` csharp
public class SampleModel
{
    [FieldLength(10)]
    public string Name { get; set; }
}
```

* **范围限定**：数值需大于 `18` 且小于 `100`：

``` csharp
public class SampleModel
{
    [FieldInRange(18, 100)]
    public int Age { get; set; }
}
```

* **正则限定**：字符串需是手机号码格式：

``` csharp
public class SampleModel
{
    [FieldRegex(@"^1[3456789]\d{9}$")]
    public string Mobile { get; set; }
}
```

* **枚举限定**：字符串只能是特定的值：

``` csharp
public class SampleModel
{
    [FieldEnumeratedValues("UNKNOWN", "MALE", "FEMALE")]
    public string Gender { get; set; }
}
```

---

#### 分页查询

很多时候，我们需要以分页的形式查询某个数据列表。**STEP.WebX.RESTful** 内置了分页的查询参数强类型映射方式。

``` csharp
public Task<IActionResult> SampleAction([FromQuery] PagingQueryModel pagingQuery)
{
    /**
     * 支持两种分页查询方式。
     *
     * 方式一：指定页数。
     *     page：（可选）指定页数，默认值 1。
     *     limit：（可选）指定每页记录数，默认值 10。
     *     order_by：（可选）指定排序方式，形如 k1=asc&k2=desc，默认值空。
     *     require_count：（可选）是否需要返回当前查询条件下的记录总数量，默认值 false。
     *
     * 方式二：指定上一页最后一条记录的标识符（一般指对应数据的 ID）。
     *     offset：（可选）指定上一页最后一条记录的标识符，默认值空。
     *     limit：同上。
     *     order_by：同上。
     *     require_count：同上。
     */
}
```

---

#### 响应结果

为了规范化 Web API 的响应结果的数据结构，**STEP.WebX.RESTful** 内置了统一的抽象模型 —— `RESTfulResult`。它有着固定的数据结构，且遵循 HTTP 状态码的语义，以 JSON 形式返回给请求发起者。

通常情况下，响应结果将以 `200` 的 HTTP 状态码返回，形如：

``` javascript
{
    "errcode": 0,
    "errmsg": "ok",
    "ret": true,
    "data": {
        /* DATA */
    }
}
```

如果接口调用失败，会返回除 `2xx` 以外的其他 HTTP 状态码。**STEP.WebX.RESTful** 内置了一些常见的错误类型，开发者也可根据业务需要自行扩充。

> 提示：后面的章节将给出常见的错误类型清单。

---

#### HttpClient

如果想以 HttpClient 的方式调用 RESTful Web API，可以配合 [STEP.Http.WebApi](https://github.com/fudiwei/STEP.Http) 使用。