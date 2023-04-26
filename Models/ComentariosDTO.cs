namespace confinancia.Models
{
	public class ComentariosDTO
	{
        public int Id { get; set; }
        public int BlogId { get; set; }
        public string Nota { get; set; }
        public string Autor { get; set; } = null!;
        public string ImgAutor { get; set; }
        public DateTime FechaPublicacion { get; set; }

        public List<SubComentarioDTO> SubComentarios { get; set;}
    }
}
