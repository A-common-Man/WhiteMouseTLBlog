using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WhiteMouseTLBlog.Data;
using WhiteMouseTLBlog.ViewModels;

namespace WhiteMouseTLBlog.Controllers
{
    public class BlogController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly INotyfService _notyfication;

        public BlogController(ApplicationDbContext context,
                                INotyfService notyfService)
        {
            _context = context;
            _notyfication = notyfService;
        }

        [HttpGet("[controller]/{slug}")]
        public IActionResult Post(string slug)
        {
            if (slug == "")
            {
                _notyfication.Error("Post not found");
                return View();
            }
            var post = _context.Posts!.Include(x => x.ApplicationUser).FirstOrDefault(x => x.Slug == slug);
            if (post == null)
            {
                _notyfication.Error("Post not found");
                return View();
            }

            var vm = new BlogPostVM()
            {
                Id = post.Id,
                Title = post.Title,
                AuthorName = post.ApplicationUser!.FirstName + " " + post.ApplicationUser.LastName,
                CreateDate = post.CreateDate,
                ThumbnailUrl = post.ThumbnailUrl,
                Description = post.Description,
                ShortDescription = post.ShortDescription,
            };

            return View(vm);
        }
    }
}
