namespace confinancia.Models
{
	public class AutorDTO
	{
        public int Id { get; set; }
        public string Correo { get; set; }
        public ImagenesDTO ImagenAutor { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
}
