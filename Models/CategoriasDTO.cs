using frontend.Models.Shared;

namespace frontend.Models
{
    public class CategoriasDTO : PaginadorDTO
    {
        public List<CategoriaDTO> ResultCategorias { get; set; }
        public int TotalCategoria { get; set; }
    }
}
