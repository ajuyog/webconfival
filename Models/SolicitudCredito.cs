namespace confinancia.Models
{
    public class SolicitudCredito
    {
        public Int64 montoSolicitud { get; set; }        
        public bool estado { get; set; }
        public int personaId { get; set; }
        public int entidadPagaduriaId { get; set; }
        public int tipoNombramientoId { get; set; }
        public int plazoMeses { get; set; }
    }
}
