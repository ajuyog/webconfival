namespace confinancia.Models
{
	public class ComentariosDTO
	{
        public int Id { get; set; }
        public string Comentario { get; set; }
        public AutorDTO Autor { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public bool Activo { get; set; }
        public BlogDTO BlogId { get; set; }
        public bool Revisado { get; set; }
        public int ComentarioId { get; set; }
        //public List<SubComentarioDTO> SubComentarios { get; set;}
    }
}
