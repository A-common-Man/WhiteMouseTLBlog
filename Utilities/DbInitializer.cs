using Microsoft.AspNetCore.Identity;
using WhiteMouseTLBlog.Data;
using WhiteMouseTLBlog.Models;

namespace WhiteMouseTLBlog.Utilities
{
    /// <summary>
    /// 数据库初始化器
    /// </summary>
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext context,
                                UserManager<ApplicationUser> userManager,
                                RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public void Initialize()
        {
            //如果不存在WebsiteRoles.WebsiteAdmin管理员角色
            if (!_roleManager.RoleExistsAsync(WebsiteRoles.WebsiteAdmin).GetAwaiter().GetResult())
            {
                //创建一个管理员角色和一个作者角色
                _roleManager.CreateAsync(new IdentityRole(WebsiteRoles.WebsiteAdmin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(WebsiteRoles.WebsiteAuthor)).GetAwaiter().GetResult();
                //创建一个用户admin@gmail.com
                _userManager.CreateAsync(new ApplicationUser()
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    FirstName = "Super",
                    LastName = "Admin"
                },"Admin@0011").Wait();

                //查找admin@gmail.com用户，如果存在该用户，则为该用户添加网站管理员角色
                var appUser = _context.ApplicationUsers!.FirstOrDefault(x => x.Email == "admin@gmail.com");
                if (appUser != null)
                {
                    _userManager.AddToRoleAsync(appUser,WebsiteRoles.WebsiteAdmin).GetAwaiter().GetResult();
                }
                /* 逐个添加页面
                var AboutPage = new Page()
                {
                    Title = "About Us",
                    Slug = "about",
                };
                var ContactPage = new Page()
                {
                    Title = "Contact Us",
                    Slug = "contact",
                };
                var PrivacyPolicyPage = new Page()
                {
                    Title = "Privacy Policy",
                    Slug = "privacy",
                };
                _context.Pages.Add(AboutPage);
                _context.Pages.Add(ContactPage);
                _context.Pages.Add(PrivacyPolicyPage);
                _context.SaveChanges();
                */

                //多个添加页面：关于我们、联系我们、隐私政策的页面
                var listOfPages = new List<Page>()
                {
                    new Page()
                    {
                        Title = "About Us",
                        Slug = "about",
                    },
                    new Page()
                    {
                        Title = "Contact Us",
                        Slug = "contact",
                    },
                    new Page()
                    {
                        Title = "Privacy Policy",
                        Slug = "privacy",
                    }
                };
                //范围添加页面
                _context.Pages!.AddRange(listOfPages);
                //保存到数据库
                _context.SaveChanges();
            }
        }
    }
}
