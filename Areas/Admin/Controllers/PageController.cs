using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using WhiteMouseTLBlog.Data;
using WhiteMouseTLBlog.Models;
using WhiteMouseTLBlog.ViewModels;

namespace WhiteMouseTLBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PageController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly INotyfService _notyfication;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public PageController(ApplicationDbContext context,
                                INotyfService notyfService,
                                IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _notyfication = notyfService;
            _webHostEnvironment = webHostEnvironment;

        }

        [HttpGet]
        public async Task<IActionResult> About()
        {
            var page = await _context.Pages!.FirstOrDefaultAsync(x => x.Slug == "about");
            var vm = new PageVM()
            {
                Id = page!.Id,
                Title = page.Title,
                ShortDescription = page.ShortDescription,
                Description = page.Description,
                ThumbnailUrl = page.ThumbnailUrl,
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> About(PageVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);
            var page = await _context.Pages!.FirstOrDefaultAsync(x => x.Slug == "about");
            if (page == null)
            {
                _notyfication.Error("Page not found");
                return View();
            }
            page.Title = vm.Title;
            page.ShortDescription = vm.ShortDescription;
            page.Description = vm.Description;

            if (vm.Thumbnail != null)
                page.ThumbnailUrl = UploadImage(vm.Thumbnail);
            await _context.SaveChangesAsync();
            _notyfication.Success("About page updated successfully");
            return RedirectToAction("About", "Page", new { area = "Admin" });
        }

        [HttpGet]
        public async Task<IActionResult> Contact()
        {
            var page = await _context.Pages!.FirstOrDefaultAsync(x => x.Slug == "contact");
            var vm = new PageVM()
            {
                Id = page!.Id,
                Title = page.Title,
                ShortDescription = page.ShortDescription,
                Description = page.Description,
                ThumbnailUrl = page.ThumbnailUrl,
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Contact(PageVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);
            var page = await _context.Pages!.FirstOrDefaultAsync(x => x.Slug == "contact");
            if (page == null)
            {
                _notyfication.Error("Page not found");
                return View();
            }
            page.Title = vm.Title;
            page.ShortDescription = vm.ShortDescription;
            page.Description = vm.Description;

            if (vm.Thumbnail != null)
                page.ThumbnailUrl = UploadImage(vm.Thumbnail);
            await _context.SaveChangesAsync();
            _notyfication.Success("Contact page updated successfully");
            return RedirectToAction("Contact", "Page", new { area = "Admin" });
        }

        [HttpGet]
        public async Task<IActionResult> Privacy()
        {
            var page = await _context.Pages!.FirstOrDefaultAsync(x => x.Slug == "privacy");
            var vm = new PageVM()
            {
                Id = page!.Id,
                Title = page.Title,
                ShortDescription = page.ShortDescription,
                Description = page.Description,
                ThumbnailUrl = page.ThumbnailUrl,
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Privacy(PageVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);
            var page = await _context.Pages!.FirstOrDefaultAsync(x => x.Slug == "privacy");
            if (page == null)
            {
                _notyfication.Error("Page not found");
                return View();
            }
            page.Title = vm.Title;
            page.ShortDescription = vm.ShortDescription;
            page.Description = vm.Description;

            if (vm.Thumbnail != null)
                page.ThumbnailUrl = UploadImage(vm.Thumbnail);
            await _context.SaveChangesAsync();
            _notyfication.Success("Privacy page updated successfully");
            return RedirectToAction("Privacy", "Page", new { area = "Admin" });
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
