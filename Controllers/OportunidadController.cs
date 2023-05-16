using confinancia.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace confinancia.Controllers
{
	public class OportunidadController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public async Task<bool> SendMail(string correo)
		{
			// -- API 1 -- //
			var obj = new SendOTP() { Valor = correo };
			var json = JsonConvert.SerializeObject(obj);
			var client = new HttpClient();
			var request = new HttpRequestMessage(HttpMethod.Post, "https://apileadconfival.azurewebsites.net/api/otp/");
			request.Headers.Add("Cache-Control", "no-cache");
			request.Headers.Add("XApiKey", "H^qP[7p#$18EXbV(lIP5xu+tCe-kgCM&{i_V,=(&");
			var content = new StringContent(json, null, "application/json");
			request.Content = content;
			var response = await client.SendAsync(request);
			if (response.IsSuccessStatusCode)
			{
				//var stringRespoonse = await response.Content.ReadAsStringAsync();
				//if(stringRespoonse == "Fue enviado un código de verificación a tu correo " + correo)
				//{
				//	return true;
				//}
				return true;
			}
			else
			{
				return false;
			}

		}

		[HttpGet]
		public async Task<bool> GetOTPMail(string codigo)
		{
			var client = new HttpClient();
			var request = new HttpRequestMessage(HttpMethod.Get, "https://apileadconfival.azurewebsites.net/api/otp?otp=" + codigo);
			request.Headers.Add("XApiKey", "H^qP[7p#$18EXbV(lIP5xu+tCe-kgCM&{i_V,=(&");
			var content = new StringContent(string.Empty);
			content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
			request.Content = content;
			var response = await client.SendAsync(request);
			if (response.IsSuccessStatusCode)
			{
				return true;
			}
			else
			{
				return false;
			}
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
