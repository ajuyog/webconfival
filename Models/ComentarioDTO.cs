namespace frontend.Models
{
	public class ComentarioDTO
	{
        public int Id { get; set; }
        public string Comentario { get; set; }
        public bool Estado { get; set; }
        public string FechaPublicacion { get; set; }
        public int ComentarioId { get; set; }
        public bool Revisado { get; set; }
    }
}
