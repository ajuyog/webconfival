﻿namespace frontend.Models
{
	public class ComentarioDTO
	{
        public int Id { get; set; }
        public string Comentario { get; set; }
        public string FechaPublicacion { get; set; }
        public AutorDTO Autor { get; set; }
        public bool Estado { get; set; }
        public bool Revisado { get; set; }
        public int ComentarioId { get; set; }
        //public List<SubComentarioDTO> SubComentarios { get; set;}
    }
}