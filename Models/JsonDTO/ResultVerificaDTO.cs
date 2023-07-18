namespace frontend.Models.JsonDTO
{
	public class ResultVerificaDTO
	{
        public int Id { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string NombreCompleto { get; set; }
        public string Error { get; set; }
    }
}
