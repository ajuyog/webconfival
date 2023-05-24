namespace confinancia.Models.JsonDTO
{
	public class BannerDTO
	{
        public string Nombre { get; set; }
        public string Url { get; set; }
		public IFormFile Archivos { get; set; }
	}
}
