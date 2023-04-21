using Microsoft.AspNetCore.Mvc;

namespace confinancia.Controllers
{
	public class OportunidadController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public int SendMail(string correo)
		{
			var numero = 123456;
			return numero;
		}

		[HttpGet]
		public int GetOTPMail(string correo)
		{
			var numero = 123456;
			return numero;
		}

		[HttpGet]
		public int SendMessage(string numero)
		{
			var codigo = 123456;
			return codigo;
		}

		[HttpGet]
		public int GetOTPCelular(string numero)
		{
			var codigo = 123456;
			return codigo;
		}

		[HttpGet]
		public int RegistraduriaCol(string documento)
		{
			var cc = 1032437606;
			var otraCC = 10;
			if(documento == cc.ToString())
			{
				return cc;
			}
			else
			{
				return otraCC;
			}
		}

	}
}
