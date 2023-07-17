using frontend.Models.Shared;

namespace frontend.Models
{
    public class CategoriasDTO : PaginadorDTO
    {
        public List<CategoriaDTO> Categorias { get; set; }
    }
}
