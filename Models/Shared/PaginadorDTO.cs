namespace frontend.Models.Shared
{
    public class PaginadorDTO
    {
        public int Count { get; set; }
        public int Paginas { get; set; }
        public int PaginaActual { get; set; }
        public string BaseUrl { get; set; }
        public string Folder { get; set; }
        public string Entorno { get; set; }
    }
}
