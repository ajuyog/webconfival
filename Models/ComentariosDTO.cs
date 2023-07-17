using frontend.Models.Shared;

namespace frontend.Models
{
	public class ComentariosDTO: PaginadorDTO
	{
        public List<ComentarioDTO> Comentarios { get; set; }
    }
}
