# skynet-cloud--天网
## 项目介绍
1. .net core 基于spring cloud 微服务开发
2. .net core全新的ORM框架
3. .net core基于nacos服务注册和发现
4. 基于 Spring Security oAuth 深度定制，支持社交登录等
5. 完整的OAuth 2.0 流程，资源服务器控制权限


## 项目结构
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

## 软件架构
![img](/image/5700335-8d69f4e885a4ec85.png)



## ORM
### ULinq介绍
ULinq是一个轻量简单易用的开源Linq ORM框架，支持Nullable类型和枚举类型，支持根据实体类自动建库建表建关系,对Linq的谓词提供了完美的支持，旨在让绝大部份的主流数据库都使用 Linq 来进行程序开发，让开发人员访问数据库从SQL中解放出来，易学易用上手快，配置简单，并且提供了源代码下载，方便定制。支持多数据库，目前支持Access、SQLServer、SqlCE、SQLite、MySQL、ORACLE，未来还会支持更多的数据库。

### ULinq支持方法
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

### ULinq使用简介
#### 创建和持久化类
##### 创建和设计持久化类
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UWay.Ufa.Enterprise.Entity
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
##### Table Config
1.  Name 属性 用来描述Table Name
默认约定：表名和实体类名完全一致
重写方式一：使用DbConfiguration SetClassNameToTalbeName(Func<string, string> fnClassNameToTableName)，表名和类名不一样，但是大部分都遵循一定的规律，比如表名都是复数，类名都是单数，那么可以自定义这种类名到表名的映射规则，少数不一致的可以通过重写方式二进行
DbConfiguration
                .Configure(connectionStringName)
                .SetSqlLogger(() => new SqlLog(Console.Out))
                .SetClassNameToTalbeName(DbConfiguration.Plural);//把类名转化为复数形式的表名
重写方式二：标签方式
[Table(Name = "Order Details")]
public class OrderDetail {}
重写方式三：使用Fluent Api来设置类名到表名间的映射，例如 把OrderDetail 实体类名映射到表名为:Order Details
DbConfiguration
                .Configure(connectionStringName)
                .SetSqlLogger(() => new SqlLog(Console.Out))
                .AddClass<OrderDetail>(p =>
                    {
                        p.TableName("Order Details");
                    });//注册映射类
2. Readonly 属性用来描述表是否是只读的
默认约定：false， 表示可以增删改查
重写方式一：标签方式
[Table(Name = "Order Details",Readonly=true)]
public class OrderDetail
重写方式二：Fluent API
 p.TableName("Order Details").Readonly();
 3. Schema 属性，数据库schema名称,可选的
 默认约定：null
 重写方式一：标签方式
 [Table(Name = "Order Details",Readonly=true, Schema="dbo")]
public class OrderDetail
重写方式二：Fluent API
p.TableName("Order Details").Readonly().Schema("dbo");

完整的基于Lambda表达式的Fluent API配置代码如下：
static DbConfiguration dbConfiguration3 = DbConfiguration
              .Configure(connectionStringName)
              .AddClass<OrderDetail>(p => { p.TableName("Order Details").Readonly().Schema("dbo"); });
##### Attribute Mapping
   Attribute的映射配置方式和LinqToSQL的配置方式类似，不用太多的笔墨进行介绍每个Attribute的含义，直接用代码说话。 （以Northwind数据库的Customers表和Orders 表为例，客户和订单是一对多关系为例）
   

    [Table(Name = "Customers")]
    public class Customer
    {
        [Id(Name="CustomerId")]//主键映射
        public string Id;
        [Column]public string ContactName;
        [Column]public string CompanyName;
        [Column]public string City;
        [Column]public string Country;

        [Ignore]public string Phone;//忽略，不映射该字段
    }

    [Table(Name = "Orders")]
    public class Order
    {
        [Id(IsDbGenerated = true)]//该主键是自动增一
        public int OrderID;

        [Column]public string CustomerID;//外键字段
        [Column]public DateTime OrderDate;
    }            
    ULinq基于Attribute的配置方式：
     TableAttribute：映射表名
     IdAttribute：映射主键的， IsDbGenerated=true 属性标致主键是自动增一的
     IgnoreAttribute：忽略映射的
     ColumnAttribute: 映射列的
     AssociationAttribute：映射关系的，一对多，多对一，一对一都用这一个标签
##### FluentAPI
1. 定义实体类
    
    public class Customer
    {
        public string Id;
        public string ContactName;
        public string CompanyName;
        public string City;
        public string Country;
        public string Phone;
     }
    public class Order
    {
        public int OrderID;
        public string CustomerID;
        public DateTime OrderDate;
    }
2. 引入FluentAPI的命名空间 
using UWay.Skynet.Cloud.Data.Mapping.Fluent
3. 创建CustomerMap类
   

     
    
        
        

            class CustomerMap : ClassMap<Customer>
            {
        public CustomerMap(){
            //设置TableName映射
            TableName("Customers");
            // 设置主键映射，把Id属性映射到数据表的CustomerId列上
            Id(e => e.Id).ColumnName("CustomerId");
            //列映射
            Column(e => e.ContactName).ColumnName("ContactName");
            Column(e => e.CompanyName);
            Column(e => e.City);
            Column(e => e.Country);

            //Phone 字段忽略映射
            Ignore(e => e.Phone);
        }
    }
4. 注册FluentAPI映射类
![img](/image/注册类.png)
##### XML Mapping file
ULinq.Xsd 定义了Mapping、Table、Id、Column、ComputedColumn、Association、Version等 映射元素
![img](/image/ULinq.png)
创建映射文件
![img](/image/映射文件.png)
##### create DbContext
![img](/image/初始化SqlServer.png)
配置文件
![img](/image/IDbContext配置信息.png)

### 使用Nacos配置中心

### 使用Nacos注册中心

### 使用spring cloud config 配置中心

### 使用spring cloud Hytrix

### 使用Security



### 使用spring cloud 构建微服务时的配置文件
[配置文件例子](/config/config.yaml)

### Demo说明
1. Skynet.Cloud.ApiDemo：普通WebApi没有支持Spring Cloud
2. Skynet.Cloud.Cloud.CloudFoundryDemo： 支持Spring Cloud的API微服务开发