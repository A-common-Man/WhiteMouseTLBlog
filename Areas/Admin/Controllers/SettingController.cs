using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using WhiteMouseTLBlog.Data;
using WhiteMouseTLBlog.Models;
using WhiteMouseTLBlog.ViewModels;

namespace WhiteMouseTLBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SettingController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly INotyfService _notyfication;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SettingController(ApplicationDbContext context, 
                                    INotyfService notyfService,
                                    IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _notyfication = notyfService;

        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var settings = _context.Settings!.ToList();
            if (settings.Count > 0)
            {
                var vm = new SettingVM()
                {
                    Id = settings[0].Id,
                    SiteName = settings[0].SiteName,
                    Title = settings[0].Title,
                    ShortDescription = settings[0].ShortDescription,
                    ThumbnailUlr = settings[0].ThumbnailUlr,
                    FacebookUrl = settings[0].FacebookUrl,
                    TwitterUrl = settings[0].TwitterUrl,
                    GithubUrl = settings[0].GithubUrl,
                };
                return View(vm);
            }
            var setting = new Setting()
            {
                SiteName = "Demo Name",
            };

            await _context.Settings!.AddAsync(setting);
            await _context.SaveChangesAsync();

            var createSettings = _context.Settings!.ToList();
            var createVm = new SettingVM()
            {
                Id = createSettings[0].Id,
                SiteName = createSettings[0].SiteName,
                Title = createSettings[0].Title,
                ShortDescription = createSettings[0].ShortDescription,
                ThumbnailUlr = createSettings[0].ThumbnailUlr,
                FacebookUrl = createSettings[0].FacebookUrl,
                TwitterUrl = createSettings[0].TwitterUrl,
                GithubUrl = createSettings[0].GithubUrl,
            };
            return View(createVm);

        }

        [HttpPost]
        public async Task<IActionResult> Index(SettingVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);
            var setting = await _context.Settings!.FirstOrDefaultAsync(x => x.Id == vm.Id);
            if (setting == null)
            {
                _notyfication.Error("Setting went wrong");
                return View(vm);
            }

            setting.SiteName = vm.SiteName;
            setting.Title = vm.Title;   
            setting.ShortDescription = vm.ShortDescription;
            setting.TwitterUrl = vm.TwitterUrl;
            setting.FacebookUrl = vm.FacebookUrl;
            setting.GithubUrl = vm.GithubUrl;
            if (vm.Thumbnail != null)
            {
                setting.ThumbnailUlr = UploadImage(vm.Thumbnail);
            }
            await _context.SaveChangesAsync();
            _notyfication.Success("Setting Updated Successfully");

            return RedirectToAction("Index", "Setting", new { area = "Admin" });

        }

        private string UploadImage(IFormFile file)
        {
            string uniqueFileName = "";
            var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "thumbnails");
            uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            var filePath = Path.Combine(folderPath, uniqueFileName);
            using (FileStream fileStream = System.IO.File.Create(filePath))
            {
                file.CopyTo(fileStream);
            }
            return uniqueFileName;
        }
    }
}
