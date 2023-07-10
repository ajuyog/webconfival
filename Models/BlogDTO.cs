using frontend.Models.Shared;

namespace frontend.Models
{
	public class BlogDTO
	{
        public int Id { get; set; } 
        public string Titulo { get; set; } 
        public string Contenido { get; set; } 
        public bool Estado { get; set; } 
        public DateTime Publicacion { get; set; } 
		public DateTime Actualizacion { get; set; } 
        //public AutorDTO Autor { get; set; }
        public List<CategoriaDTO> Categorias { get; set; }
		public List<ComentariosDTO> Comentarios { get; set; }
        public string Imagen { get; set; }
        public List<string> Galeria { get; set; }

    }
}
