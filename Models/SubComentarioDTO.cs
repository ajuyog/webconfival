namespace confinancia.Models
{
	public class SubComentarioDTO
	{
		public int Id { get; set; }
        public int ComentarioId { get; set; }
		public string Nota { get; set; }
		public string Autor { get; set; }
		public string ImgAutor { get; set; }
		public DateTime FechaPublicacion { get; set; }
	}
}
