using confinancia.Models;
using confinancia.Models.JsonDTO;
using confinancia.Services.Token;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using Newtonsoft.Json;
using System;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;

namespace confinancia.Controllers
{
	public class OportunidadController : Controller
	{
		#region CONSTRUCTOR
		private readonly IConfiguration _configuration;
		private readonly IGetToken _getToken;

		public OportunidadController(IConfiguration configuration, IGetToken getToken)
		{
			_configuration = configuration;
			_getToken = getToken;
		}
		#endregion

		public IActionResult Index()
		{
			return View();
		}

		//[HttpGet]
		//public async Task<bool> SendOTP(string entrada)
		//{
		//	var pattern = "(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|\"(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21\\x23-\\x5b\\x5d-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])*\")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\\[(?:(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9]))\\.){3}(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9])|[a-z0-9-]*[a-z0-9]:(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21-\\x5a\\x53-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])+)\\])";
		//	Regex validMail = new Regex(pattern);

		//	var token = GetToken();
		//	if (token.Result == "")
		//	{
		//		return false;
		//	}
		//	if (token.Result.Length == 177)
		//	{
		//		var client = new HttpClient();
		//		var request = new HttpRequestMessage(HttpMethod.Post, "https://api2valuezbpm.azurewebsites.net/api/otp/enviar?longitud=" + _configuration.GetSection("OTP:Longitud").Value + "&caducidadSg=" + _configuration.GetSection("OTP:Caducidad").Value);
		//		request.Headers.Add("Authorization", "Bearer " + token.Result);
		//		var obj = new EnvioOTPDTO()
		//		{
		//			FuenteOTPId = validMail.IsMatch(entrada) == true ? Convert.ToInt32(_configuration.GetSection("OTP:FuenteOTPMail").Value) : Convert.ToInt32(_configuration.GetSection("OTP:FuenteOTPSMS").Value),
		//			EmpresaId = Convert.ToInt32(_configuration.GetSection("OTP:EmpresaId").Value),
		//			Entrada = validMail.IsMatch(entrada) == true ? entrada : "57" + entrada,
		//		};
		//		var json = JsonConvert.SerializeObject(obj);
		//		var content = new StringContent(json, null, "application/json");
		//		request.Content = content;
		//		var response = await client.SendAsync(request);
		//		if (response.IsSuccessStatusCode)
		//		{
		//			return true;
		//		}
		//		else
		//		{
		//			return false;
		//		}
		//	}
		//	return false;
		//}

		[HttpGet]
		public bool SendOTP(string entrada)
		{
			return true;
		}

		//[HttpGet]
		//public async Task<string> ValidOTP(string llave, string entrada)
		//{
		//	var pattern = "(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|\"(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21\\x23-\\x5b\\x5d-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])*\")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\\[(?:(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9]))\\.){3}(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9])|[a-z0-9-]*[a-z0-9]:(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21-\\x5a\\x53-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])+)\\])";
		//	Regex validMail = new Regex(pattern);
		//	var token = GetToken();
		//	if (token.Result == "")
		//	{
		//		return "error";
		//	}
		//	if (token.Result.Length == 177)
		//	{
		//		var client = new HttpClient();
		//		var request = new HttpRequestMessage(HttpMethod.Post, "https://api2valuezbpm.azurewebsites.net/api/otp/validatemailotp");
		//		request.Headers.Add("Authorization", "Bearer " + token.Result);
		//		var obj = new ValidatOTPDTO()
		//		{
		//			FuenteOTPId = validMail.IsMatch(entrada) == true ? Convert.ToInt32(_configuration.GetSection("OTP:FuenteOTPMail").Value) : Convert.ToInt32(_configuration.GetSection("OTP:FuenteOTPSMS").Value),
		//			EmpresaId = Convert.ToInt32(_configuration.GetSection("OTP:EmpresaId").Value),
		//			Entrada = validMail.IsMatch(entrada) == true ? entrada : "57" + entrada,
		//			Llave = llave
		//		};
		//		var json = JsonConvert.SerializeObject(obj);
		//		var content = new StringContent(json, null, "application/json");
		//		request.Content = content;
		//		var response = await client.SendAsync(request);
		//		if (response.IsSuccessStatusCode)
		//		{
		//			return "success";
		//		}
		//		else
		//		{
		//			var responseStream = await response.Content.ReadAsStringAsync();
		//			return responseStream;
		//		}
		//	}
		//	return "error";
		//}

		[HttpGet]
		public string ValidOTP(string llave, string entrada)
		{
			return "success";
		}

		[HttpGet]
		public async Task<ResultVerificaDTO> RegistraduriaCol(string documento, string nombre, string apellido, string tipoDocumento)
		{
			var error = new ResultVerificaDTO();
			var token = await _getToken.GetTokenV();
			if (token == "")
			{
				return error;
			}
			if (token.Length == 177)
			{
				var client = new HttpClient();
				var request = new HttpRequestMessage(HttpMethod.Post, "https://api2valuezbpm.azurewebsites.net/api/verifik?idNumber=" + documento + "&fNombres=" + nombre + "&fApellidos=" + apellido + "&tipoDocumento=" + tipoDocumento);
				request.Headers.Add("Authorization", "Bearer " + token);
				var response = await client.SendAsync(request);
				if (response.IsSuccessStatusCode)
				{
					var responseStream = await response.Content.ReadAsStringAsync();
					var result = JsonConvert.DeserializeObject<ResultVerificaDTO>(responseStream);
					return result;
				}
				else
				{
					var responseStreamElse = await response.Content.ReadAsStringAsync();
					var objError = new ResultVerificaDTO()
					{
						Error = responseStreamElse
					};
					return objError;
				}
			}
			return error;
		}

		[HttpGet]
		public string GetAttempts(string valor)
		{
			return valor == "SMS" ? _configuration.GetSection("Intentos:SMS").Value : valor == "Verifik" ? _configuration.GetSection("Intentos:Verifik").Value : "";
		}

		[HttpPost]
		public async Task<bool> SaveForm()
		{
			var token = await _getToken.GetTokenV();

			var form = await HttpContext.Request.ReadFormAsync();
			var persona = new PersonaDTO()
			{
				nombres = form["nombres-ok"],
				apellidos = form["apellidos-ok"],
				codSecundario1 = form["no-documento-ok"],
				correoElectronico = form["correo-ok"],
				numeroContacto = form["no-contacto-ok"],
				fechaNacimiento = form["fNacimiento-ok"] == "" ? new DateTime(1900, 1, 1) : DateTime.Parse(HttpContext.Request.Form["fNacimiento-ok"]),
				fechaExpedicion = form["fExpedicion-ok"] == "" ? new DateTime(1900, 1, 1) : DateTime.Parse(HttpContext.Request.Form["fExpedicion-ok"]),
				tipoDocumento = form["tp-documento-ok"],
				politicaTratamientoDatos = true,
				estado = true
			};
			var leadOportunidad = new OportunidadDTO()
			{
				tipoProvidenciaId = Convert.ToInt32(form["fallo"]),
				medioControlId = Convert.ToInt32(form["medio-control"]),
				regimenId = Convert.ToInt32(form["tipo-regimen"]),
				corporacionId = Convert.ToInt32(form["corporacion"]),
				entidadPagaduriaId = Convert.ToInt32(form["entidad-pagaduria"]),
				fechaEjecutoria = form["f-ejecutoria"] == "" ? DateTime.ParseExact("1900-01-01 14:00:00,531", "yyyy-MM-dd HH:mm:ss,fff", System.Globalization.CultureInfo.InvariantCulture) : DateTime.Parse(form["f-ejecutoria"]),
				numeroRadicado = form["numero-radicado-user"],
				cuentaCobro = form["cuenta-cobro-user"],
				demandante = form["demandante"]

			};
			var archivoC = form.Files;

			var scId = 0;
			var ok = false;
			//httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");
			var httpClient = new HttpClient();

			httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
			var response = await httpClient.PostAsJsonAsync("https://api2valuezbpm.azurewebsites.net/api/leadPersona", persona);
			if (response.IsSuccessStatusCode)
			{
				// return Content("response1" + response);
				var responseStream = await response.Content.ReadAsStringAsync();
				var objPN = JsonConvert.DeserializeObject<PersonaDTO>(responseStream);
				leadOportunidad.leadPersonaId = objPN.id;
				var response2 = await httpClient.PostAsJsonAsync("https://api2valuezbpm.azurewebsites.net/api/leadOportunidad", leadOportunidad);
				//return Content("response2" + response2);

				if (response2.IsSuccessStatusCode)
				{
					var responseStream3 = await response2.Content.ReadAsStringAsync();
					var objSC = JsonConvert.DeserializeObject<OportunidadDTO>(responseStream3);
					//scId = (int) objSC.id;
					ok = true;
					// return Content("response3" + responseStream3);
				}
				else
				{
					var responseStream5 = await response2.Content.ReadAsStringAsync();
					ok = false;
				}
			}
			else
			{
				var responseStream2 = await response.Content.ReadAsStringAsync();
				ok = false;
			}
			return ok;
		}

	}
}
