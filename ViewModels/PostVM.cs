﻿namespace WhiteMouseTLBlog.ViewModels
{
    public class PostVM
    {
        public int Id { get;set; }
        public string? Title { get; set; }
        public string? AuthorName { get; set; }
        public DateTime CreateDate { get; set; }
        public string? ThumbnailUrl { get; set; }
    }
}
