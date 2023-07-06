namespace frontend.Models
{
    public class OportunidadDTO
    {
        public int id { get; set; }
        public int tipoProvidenciaId { get; set; }
        public int medioControlId { get; set; }
        public int regimenId { get; set; }
        public int corporacionId { get; set; }
        public int entidadPagaduriaId { get; set; }
        public int leadPersonaId { get; set; }
        public DateTime? fechaEjecutoria { get; set; }
        public string numeroRadicado { get; set; }
        public string cuentaCobro { get; set; }
        public string demandante { get; set; }
        public int tipoActorId { get; set; }
    }
}
