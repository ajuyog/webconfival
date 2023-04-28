namespace confinancia.Models
{
	public class ComentariosDTO
	{
        public int Id { get; set; }
        public int BlogId { get; set; }
        public string Nota { get; set; }
        public AutorDTO Autor { get; set; }
        public DateTime FechaPublicacion { get; set; }

        public List<SubComentarioDTO> SubComentarios { get; set;}
    }
}
