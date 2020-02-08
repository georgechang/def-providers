using System;

namespace GC.DataExchange.Providers.WordPress.Models
{
    public class Post
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Excerpt { get; set; }
        public string AuthorName { get; set; }
    }
}
