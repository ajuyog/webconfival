namespace confinancia.Models
{
    public class SolicitudCreditoDTO
    {
        public Int64 id { get; set; }
        public Int64 montoSolicitud { get; set; }
        public Int64 cuotaEsperada { get; set; }
        public bool estado { get; set; }
        public Persona persona { get; set; }
        public EntidadPagaduria entidadPagaduria { get; set; }
        public TipoNombramiento tipoNombramiento { get; set; }
        public int plazoMeses { get; set; }
    }
}
