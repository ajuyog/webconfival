namespace frontend.Models
{
	public class SubComentarioDTO
	{
		public int Id { get; set; }
		public int SubComentario { get; set; }
		public int ComentarioId { get; set; }
		public string Contenido { get; set; }
		public AutorDTO Autor { get; set; }
		public DateTime FechaPublicacion { get; set; }
    }
}
