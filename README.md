# skynet-cloud--天网
#### 项目介绍
1. .net core 基于spring cloud 微服务开发
2. .net core全新的ORM框架
3. .net core基于nacos服务注册和发现
4. 基于 Spring Security oAuth 深度定制，支持社交登录等
5. 完整的OAuth 2.0 流程，资源服务器控制权限


#### 项目结构
```
├─skynet-cloud
│  │ 
│  ├─01.Skynet.Cloud.Framework
│  │  │ 
│  │  ├─1.Projects-------------工程
│  │  │  │ 
│  │  │  ├─Skynet.Cloud.Data-------------ORM
│  │  │  │ 
│  │  │  ├─Skynet.Cloud.Framework-------------框架基础组件
│  │  │  │ 
│  │  │  ├─Skynet.Cloud.Mvc-------------Mvc基础扩展
│  │  │  │ 
│  │  │  ├─Skynet.Cloud.Nacos-------------Nacos访问
│  │  │  │ 
│  │  │  ├─Skynet.Cloud.Security-------------安全设置
│  │  │  │ 
│  │  │  ├─Skynet.Cloud.WebCore-------------.net core Web扩展
│  │  │  │ 
│  │  │  ├─Steeltoe.Configuration.NacosServerBase-------------Nacos注册中心
│  │  │  │ 
│  │  │  ├─Steeltoe.Discovery.NacosBase-------------Nacos服务发现
│  │  │ 
│  │  ├─2.UnitTest-------------单元测试
│  │  │ 
│  │  ├─3.Demo-------------示例
│  │ 
│  ├─02.Skynet.Cloud.Upms-------------权限管理
│  │  │ 
│  │  ├─1.Projects-------------工程
│  │  │ 
│  │  ├─2.UnitTest-------------单元测试
│  │  │ 
│  │  ├─3.Demo-------------示例
│  │ 
│  ├─03.Skynet.Cloud.Dcs
│  │  │ 
│  │  ├─1.Projects-------------工程
│  │  │ 
│  │  ├─2.UnitTest-------------单元测试
│  │  │ 
│  │  ├─3.Demo-------------示例

```
----

#### 软件架构
![img](/image/5700335-8d69f4e885a4ec85.png)



#### ORM-ULinq介绍
ULinq是一个轻量简单易用的开源Linq ORM框架，支持Nullable类型和枚举类型，支持根据实体类自动建库建表建关系,对Linq的谓词提供了完美的支持，旨在让绝大部份的主流数据库都使用 Linq 来进行程序开发，让开发人员访问数据库从SQL中解放出来，易学易用上手快，配置简单，并且提供了源代码下载，方便定制。支持多数据库，目前支持Access、SQLServer、SqlCE、SQLite、MySQL、ORACLE，未来还会支持更多的数据库。

##### ORM-ULinq支持方法
<table>
<tr>
    <td>.Net Standard</td>
    <td>MSSQL</td>
    <td>Access</td>
    <td>SQLite</td>
    <td>SqlServerCE</td>
    <td>Oracle</td>
    <td>Firebird</td>
    <td>Postgres</td>
</tr>
<tr>
    <td>String.Concat</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td></td>
    <td></td>
</tr>
<tr>
    <td>String.Contains</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td></td>
    <td></td>
</tr>
<tr>
    <td>String.EndsWith</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td></td>
    <td></td>
</tr>
<tr>
    <td>String.IndexOf</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td></td>
    <td></td>
</tr>
<tr>
    <td>String.IsNullOrEmpty</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td></td>
    <td></td>
</tr>
<tr>
    <td>String.IsNullOrWhiteSpace</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td></td>
    <td></td>
</tr>
<tr>
    <td>String.LastIndexOf</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td></td>
    <td></td>
</tr>
<tr>
    <td>String.Length</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td></td>
    <td></td>
</tr>
<tr>
    <td>String.LeftOf</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td>不支持</td>
    <td></td>
    <td></td>
</tr>
<tr>
    <td>String.PadLeft</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td></td>
    <td></td>
</tr>
<tr>
    <td>String.PadRight</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td></td>
    <td></td>
</tr>
<tr>
    <td>String.Remove</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td></td>
    <td></td>
</tr>
<tr>
    <td>String.Replace</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td></td>
    <td></td>
</tr>
<tr>
    <td>String.RightOf</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td>不支持</td>
    <td></td>
    <td></td>
</tr>
<tr>
    <td>String.StartsWith</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td></td>
    <td></td>
</tr>
<tr>
    <td>String.Substring</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td></td>
    <td></td>
</tr>
<tr>
    <td>String.ToLower</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td></td>
    <td></td>
</tr>
<tr>
    <td>String.ToUpper</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td></td>
    <td></td>
</tr>
<tr>
    <td>String.Trim</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td></td>
    <td></td>
</tr>
<tr>
    <td>String.TrimEnd</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td></td>
    <td></td>
</tr>
<tr>
    <td>String.TrimStart</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td></td>
    <td></td>
</tr>
<tr>
    <td>String.Insert</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td></td>
    <td></td>
</tr>
<tr>
    <td>String.Reverse</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td>是</td>
    <td>不支持</td>
    <td></td>
    <td></td>
</tr>
</table>

##### Demo说明
1. Skynet.Cloud.ApiDemo：普通WebApi没有支持Spring Cloud
2. Skynet.Cloud.Cloud.CloudFoundryDemo： 支持Spring Cloud的API微服务开发