## 使用手册

---

### RESTful 异常处理

**STEP.WebX.RESTful** 将捕捉所有可能产生的错误，并包装成统一的 API 响应格式返回给请求调用者。

如果接口调用失败，将会在 HTTP 状态码中得到体现，通常可分为两种类型：

* 当 HTTP 状态码为 `4xx` 时，这些状态码表示请求本身存在问题（可能是缺少参数、格式错误等原因），妨碍了服务器的处理。

* 当 HTTP 状态码为 `5xx` 时，这些状态码表示服务器在尝试处理请求时发生内部错误（可能是业务逻辑错误、数据库宕机等原因）。

同时，响应结果中的 `errcode` 字段的值将会是 `5` 位正整数，其中前 `3` 位与 HTTP 状态码相同，描述了当前异常的具体原因。

---

#### 常见的 HTTP 状态码及其含义

| HTTP 状态码 | 含义 |
| ---------- | ---- |
| 400 BadRequest | 非法请求。通常原因是请求携带的参数或查询字符串错误。 |
| 401 Unauthorized | 请求未授权。通常原因是请求签名错误或授权令牌无效。 |
| 403 Forbidden | 请求被阻止。通常原因是不被允许访问请求的对象。 |
| 404 NotFound | 对象未找到。通常原因是请求的对象不存在或已被删除。 |
| 405 NotAllowed | 请求不被允许。通常原因是请求路由不正确。 |
| 408 Timeout | 请求超时。通常原因是请求中携带的时间戳信息错误。 |
| 429 QuotaExceed | 请求配额超限。通常原因是请求过于频繁。 |
| 500 InternalServerError | 服务器遇到了内部错误。 |


#### 常见的异常状态码及其含义

| errcode | errmsg | description |
| ------- | ------ | ----------- |
| 0 | ok | 无异常 |
| 40000 | data parsing error | 请求参数格式异常 |
| 40001 | lack of parameter | 缺少参数 |
| 40002 | invalid parameter | 非法参数 |
| 40011 | lack of query | 缺少查询条件 |
| 40012 | invalid query | 非法查询条件 |
| 40021 | lack of file | 缺少文件 |
| 40022 | invalid file | 非法文件 |
| 40099 | payload too large | 请求内容大小超出限制 |
| 40100 | no access | 无法访问 |
| 40101 | lack of grant | 缺少授权信息 |
| 40102 | invalid grant | 非法授权信息 |
| 40103 | lack of signature | 缺少请求签名 |
| 40104 | invalid signature | 非法请求签名 |
| 40105 | lack of app key | 缺少应用标识 |
| 40106 | invalid app key | 非法应用标识 |
| 40110 | hotlinking or referrer not in whitelist | 非法请求来源 |
| 40300 | permission required | 权限不足 |
| 40400 | non-exists resourse | 不存在的资源 |
| 40401 | obsoleted resourse | 已过期的资源 |
| 40500 | invalid method or no route matched | 路由错误 |
| 40800 | timeout or timestamp expired | 请求超时 |
| 40801 | lack of timestamp | 缺少时间戳 |
| 40802 | invalid timestamp | 非法时间戳 |
| 42900 | suspected replay attack | 疑似重放攻击 |
| 42901 | duplicate signature | 请求签名冲突 |
| 50000 | fatal error | 致命错误 |
| 50031 | remote invoking failed | 远程调用失败 |

以上的异常状态码已内置在 **STEP.WebX.RESTful** 中，如果开发者需要额外扩充，请避免和上面的值冲突，以免造成歧义。