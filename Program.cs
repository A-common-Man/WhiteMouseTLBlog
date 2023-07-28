using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WhiteMouseTLBlog.Data;
using WhiteMouseTLBlog.Models;
using WhiteMouseTLBlog.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


//定义数据库连接字符串是appsettings.json中的DefaultConnection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//添加数据库上下文服务
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

//添加角色认证服务,自定义身份系统以使用自己的用户和角色模型；
//与身份相关的数据将使用EF Core的数据库操作进行持久化；
//将默认令牌提供程序添加到身份系统
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

//每当IDbInitializer应用程序中的任何组件请求依赖项时，DbInitializer都应提供该类的实例。
//该方法设置要限定范围的服务的生命周期，这意味着每个范围（通常是每个 HTTP 请求）将创建一次AddScoped新的实例，
//并将在该范围内共享。
builder.Services.AddScoped<IDbInitializer,DbInitializer>();

//添加aspnetcorehero通知服务
builder.Services.AddNotyf(config => { config.DurationInSeconds = 10; config.IsDismissable = true; config.Position = NotyfPosition.BottomRight; });

//添加Cookie的路径
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/login";
    options.AccessDeniedPath = "/AccessDenied";
});

var app = builder.Build();

DataSeeding();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

//使用aspnetcorehero通知服务
app.UseNotyf();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "area",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

void DataSeeding()
{
    using(var scope = app.Services.CreateScope())
    {
        var DbInitialize = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        DbInitialize.Initialize();
    }
}

app.Run();
