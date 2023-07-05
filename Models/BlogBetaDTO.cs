namespace frontend.Models
{
	public class BlogBetaDTO
	{
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public DateTime Publicacion { get; set; }
        public DateTime Actualizacion { get; set; }
        public bool Estado { get; set; }
        public int[] CategoriaId { get; set; }
    }
}
