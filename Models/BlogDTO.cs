using System.Runtime.InteropServices;

namespace confinancia.Models
{
	public class BlogDTO
	{
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string ImgBlog { get; set; }
        public string Contenido { get; set; }
        public string IntroUno { get; set; }
        public string IntroDos { get; set; }
        public List<CategoriaDTO> Categorias { get; set; }
        public AutorDTO Autor { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public List<ComentariosDTO> Comentarios { get; set; }
    }
}
