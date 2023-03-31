namespace confinancia.Models
{
    public class Persona
    {
        public int id { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string codSecundario1 { get; set; }
        public string correoElectronico { get; set; }
        public string numeroContacto { get; set; }
        public string numeroContactoAdicional { get; set; }
        public bool politicaTratamientoDatos { get; set; }
        public bool estado { get; set; }
    }
}
