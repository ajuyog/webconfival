using frontend.Models.Shared;

namespace frontend.Models
{
	public class CategoriasAdminDTO: PaginadorDTO
	{
		public List<CategoriaDTO> ResultCategoria { get; set; }
		public int TotalCategoria { get; set; }
        public string Search { get; set; }
    }
}
