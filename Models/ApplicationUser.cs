using Microsoft.AspNetCore.Identity;

namespace WhiteMouseTLBlog.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set;}

        //与Post的关系
        public List<Post>? Posts { get; set; }
    }
}
