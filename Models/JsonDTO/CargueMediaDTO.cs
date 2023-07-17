namespace frontend.Models.JsonDTO
{
	public class CargueMediaDTO
	{
        public int Id { get; set; }
        public int ProyectoId { get; set; }
        public int MenuId { get; set; }
        public string Ruta { get; set; }
        public string UrlMedia { get; set; }
        public bool Estado { get; set; }
    }
}
