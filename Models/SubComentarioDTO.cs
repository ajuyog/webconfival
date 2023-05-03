namespace confinancia.Models
{
	public class SubComentarioDTO
	{
		public int Id { get; set; }
		public int SubComentario { get; set; }
		public int ComentarioId { get; set; }
		public string Nota { get; set; }
		public AutorDTO Autor { get; set; }
		public DateTime FechaPublicacion { get; set; }
	}
}
