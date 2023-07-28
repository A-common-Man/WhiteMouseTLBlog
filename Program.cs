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


//�������ݿ������ַ�����appsettings.json�е�DefaultConnection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//������ݿ������ķ���
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

//��ӽ�ɫ��֤����,�Զ������ϵͳ��ʹ���Լ����û��ͽ�ɫģ�ͣ�
//�������ص����ݽ�ʹ��EF Core�����ݿ�������г־û���
//��Ĭ�������ṩ������ӵ����ϵͳ
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

//ÿ��IDbInitializerӦ�ó����е��κ��������������ʱ��DbInitializer��Ӧ�ṩ�����ʵ����
//�÷�������Ҫ�޶���Χ�ķ�����������ڣ�����ζ��ÿ����Χ��ͨ����ÿ�� HTTP ���󣩽�����һ��AddScoped�µ�ʵ����
//�����ڸ÷�Χ�ڹ���
builder.Services.AddScoped<IDbInitializer,DbInitializer>();

//���aspnetcorehero֪ͨ����
builder.Services.AddNotyf(config => { config.DurationInSeconds = 10; config.IsDismissable = true; config.Position = NotyfPosition.BottomRight; });

//���Cookie��·��
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

//ʹ��aspnetcorehero֪ͨ����
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
