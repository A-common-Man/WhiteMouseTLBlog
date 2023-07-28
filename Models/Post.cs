namespace WhiteMouseTLBlog.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? ShortDescription { get; set; }
        public string? Description { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public string? Slug { get;set; }
        public string? ThumbnailUrl { get; set; }

        //与ApplicationUser的关系
        public string? ApplicationUserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
    }
}
