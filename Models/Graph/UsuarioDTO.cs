﻿namespace frontend.Models.Graph
{
	public class UsuarioDTO
	{
        public int Id { get; set; }
        public string Email { get; set; }
        public string EmailNormalizado { get; set; }
        public string PasswordHash { get; set; }
    }
}
