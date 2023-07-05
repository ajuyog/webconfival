namespace frontend.Models
{
    public class PersonaDTO
    {
        public int id { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string codSecundario1 { get; set; }
        public string correoElectronico { get; set; }
        public string numeroContacto { get; set; }
        public DateTime? fechaNacimiento { get; set; }
        public DateTime? fechaExpedicion { get; set; }
        public string tipoDocumento { get; set; }
        public bool politicaTratamientoDatos { get; set; }
        public bool estado { get; set; }
    }
}
