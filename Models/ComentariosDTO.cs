namespace confinancia.Models
{
	public class ComentariosDTO
	{
        public int Id { get; set; }
        public string Contenido { get; set; }
        public AutorDTO Autor { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public bool Activo { get; set; }

        public List<SubComentarioDTO> SubComentarios { get; set;}
    }
}
