using confinancia.Models;
using confinancia.Models.JsonDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http.Headers;
using System.Text;

namespace confinancia.Controllers
{
	public class OportunidadController : Controller
	{
		#region CONSTRUCTOR
		private readonly IConfiguration _configuration;
		public OportunidadController(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		#endregion

		public IActionResult Index()
		{
			return View();
		}

		//[HttpGet]
		//public async Task<bool> SendMail(string correo)
		//{
		//	var obj = new SendOTP() { Valor = correo };
		//	var json = JsonConvert.SerializeObject(obj);
		//	var client = new HttpClient();
		//	var request = new HttpRequestMessage(HttpMethod.Post, "https://apileadconfival.azurewebsites.net/api/otp/");
		//	request.Headers.Add("Cache-Control", "no-cache");
		//	request.Headers.Add("XApiKey", "H^qP[7p#$18EXbV(lIP5xu+tCe-kgCM&{i_V,=(&");
		//	var content = new StringContent(json, null, "application/json");
		//	request.Content = content;
		//	var response = await client.SendAsync(request);
		//	if (response.IsSuccessStatusCode)
		//	{
		//		return true;
		//	}
		//	else
		//	{
		//		return false;
		//	}
		//}

		[HttpGet]
		public bool SendMail(string correo)
		{
			return true;
		}

		//[HttpGet]
		//public async Task<bool> GetOTPMail(string codigo)
		//{
		//	var client = new HttpClient();
		//	var request = new HttpRequestMessage(HttpMethod.Get, "https://apileadconfival.azurewebsites.net/api/otp?otp=" + codigo);
		//	request.Headers.Add("XApiKey", "H^qP[7p#$18EXbV(lIP5xu+tCe-kgCM&{i_V,=(&");
		//	var content = new StringContent(string.Empty);
		//	content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
		//	request.Content = content;
		//	var response = await client.SendAsync(request);
		//	if (response.IsSuccessStatusCode)
		//	{
		//		return true;
		//	}
		//	else
		//	{
		//		return false;
		//	}
		//}

		[HttpGet]
		public bool GetOTPMail(string codigo)
		{
			return true;
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
		public async Task<VerificaResultDTO> RegistraduriaCol(string documento)
		{
			/// -- Obtengo el tken de login -- //
			var token = GetToken();
			if(token.Result == "") 
			{
				return new VerificaResultDTO() { NumeroDocumento = "" };
			}
			else
			{
				var client = new HttpClient();
				var request = new HttpRequestMessage(HttpMethod.Get, "https://api2valuezbpm.azurewebsites.net/api/verifik/" + documento);
				request.Headers.Add("Authorization", "Bearer " + token.Result);
				var response = await client.SendAsync(request);
				if (response.IsSuccessStatusCode)
				{
					var responseStream = await response.Content.ReadAsStringAsync();
					var result = JsonConvert.DeserializeObject<VerificaResultDTO>(responseStream);
					return result;
				}
				else
				{
					var empty = new VerificaResultDTO();
					return empty;
				}
			}

		}


		[HttpGet]
		public async Task<string> GetToken()
		{
			var obj = new LoginTokenDTO()
			{
				Id = _configuration.GetSection("Variables:IdLogin").Value,
				Email = _configuration.GetSection("Variables:Email").Value,
				Password = _configuration.GetSection("Variables:Password").Value,
			};
			var json = JsonConvert.SerializeObject(obj);
			var client = new HttpClient();
			var request = new HttpRequestMessage(HttpMethod.Post, "https://api2valuezbpm.azurewebsites.net/api/cuentas/inicioSesion?secret=" + _configuration.GetSection("Variables:Secret").Value);
			var content = new StringContent(json, null, "application/json");
			request.Content = content;
			var response = await client.SendAsync(request);
			if (response.IsSuccessStatusCode)
			{
				var responseStream = await response.Content.ReadAsStringAsync();
				var result = JsonConvert.DeserializeObject<TokenValuezDTO>(responseStream);
				return result.Token;
			}
			else
			{
				var empty = "";
				return empty;
			}
		}

	}
}
