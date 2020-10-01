namespace BlogClient.Models
{
    public class CategoryWithBlogCountModel{
        public int BlogsCount { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}