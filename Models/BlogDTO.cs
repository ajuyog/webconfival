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
        public string Autor { get; set; }
        public string ImgAutor { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public List<ComentariosDTO> Comentarios { get; set; }
    }
}
