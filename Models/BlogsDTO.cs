using frontend.Models.Shared;

namespace frontend.Models
{
    public class BlogsDTO: PaginadorDTO
    {
        public List<BlogDTO> ResultBlog { get; set; }
        public int totalBlogFalse { get; set; }
        public int totalBlogTrue { get; set; }
        public int TotalBlog { get; set; }
    }
}
