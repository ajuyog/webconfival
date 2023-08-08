using frontend.Models.Shared;

namespace frontend.Models
{
	public class ComentariosDTO: PaginadorDTO
	{
        public int totalBlogs { get; set; }
        public List<ComentarioDTO> comentariosBlog { get; set; }
        public string Search { get; set; }
        public int BlogId { get; set; }
    }
}
