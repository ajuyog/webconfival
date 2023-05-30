namespace confinancia.Models.JsonDTO
{
	public class ValidatOTPDTO
	{
        public int FuenteOTPId { get; set; }
        public int EmpresaId { get; set; }
        public string Llave { get; set; }
        public string Entrada { get; set; }
    }
}
