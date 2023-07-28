# WhiteMouseTLBlog——个人博客网站
<br/>
## **介绍**

WhiteMouseTLBlog使用了.net core 6 和SQLServer开发的。

主要分为前端展示数据系统和后台管理系统两部分，后台管理系统开辟了一个新的Area命名为Admin。

主要使用的技术和设计思想是asp.net core MVC、EFCore中的CodeFirst、MVVM、FileSteam文件流和Using对象释放

使用的Nuget包：

Microsoft.EntityFrameworkCore    -版本6.11 ORM（实体关系映射）框架
Microsoft.EntityFrameworkCore.Tools    -版本6.11 快速迁移指令工具
Microsoft.EntityFrameworkCore.SqlServer        -版本6.11 对象关系映射对应数据库
Microsoft.AspNetCore.Identity.EntityFrameworkCore     -版本6.11 实体关系映射的身份认证
Microsoft.AspNetCore.Identity.UI         -版本6.11  身份认证UI
AspNetCoreHero.ToastNotification         -版本1.1.0 通知弹窗
X.PagedList.Mvc.Core                            -版本8.4.7 实现列表分页翻页
部署这个网站购买了一个域名和一个Ubuntu20.04的Linux服务器，服务器使用.net core 自带的轻量级跨平台的Kestrel和反向代理服务器Nginx，在Nignx中添加了一个自启动服务。

## 使用方法
1.下载代码，下载VS2022，下载SqlServer2022<br/>
2、在appsettings.json中更改数据库连接字符串ConnectionStrings为对应数据库连接字符串<br/>
3、依次点击VS2022中的工具→Nuget包管理器→程序包管理控制台，输入 add-migration IninialCreate 和 update-database<br/>
4、点击VS2022运行按钮实现本地运行。
