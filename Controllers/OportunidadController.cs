using confinancia.Models;
using confinancia.Models.JsonDTO;
using confinancia.Services.Graph;
using confinancia.Services.Token;
using confinancia.Services.Utilidaddes;
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
        private readonly ISendMail _sendMail;
        private readonly IMail _mail;

        public OportunidadController(IConfiguration configuration, IGetToken getToken, ISendMail sendMail, IMail mail )
		{
			_configuration = configuration;
			_getToken = getToken;
            _sendMail = sendMail;
            _mail = mail;
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

		//	var token = _getToken.GetTokenV();
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
		//	var token = _getToken.GetTokenV();
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
		public async Task<string> SaveForm()
		{
			var token = await _getToken.GetTokenV();
			var formOportunidad = await HttpContext.Request.ReadFormAsync();

			var persona = new PersonaDTO()
			{
				nombres = formOportunidad["nombres-ok"],
				apellidos = formOportunidad["apellidos-ok"],
				codSecundario1 = formOportunidad["no-documento-ok"],
				correoElectronico = formOportunidad["correo-ok"],
				numeroContacto = formOportunidad["no-contacto-ok"],
				fechaNacimiento = formOportunidad["fNacimiento-ok"] == "" ? new DateTime(1900, 1, 1) : DateTime.Parse(HttpContext.Request.Form["fNacimiento-ok"]),
				fechaExpedicion = formOportunidad["fExpedicion-ok"] == "" ? new DateTime(1900, 1, 1) : DateTime.Parse(HttpContext.Request.Form["fExpedicion-ok"]),
				tipoDocumento = formOportunidad["tp-documento-ok"],
				politicaTratamientoDatos = true,
				estado = true
			};
			var leadOportunidad = new OportunidadDTO()
			{
				tipoProvidenciaId = Convert.ToInt32(formOportunidad["fallo"]),
				medioControlId = Convert.ToInt32(formOportunidad["medio-control"]),
				regimenId = Convert.ToInt32(formOportunidad["tipo-regimen"]),
				corporacionId = Convert.ToInt32(formOportunidad["corporacion"]),
				entidadPagaduriaId = Convert.ToInt32(formOportunidad["entidad-pagaduria"]),
				fechaEjecutoria = formOportunidad["f-ejecutoria"] == "" ? DateTime.ParseExact("1900-01-01 14:00:00,531", "yyyy-MM-dd HH:mm:ss,fff", System.Globalization.CultureInfo.InvariantCulture) : DateTime.Parse(formOportunidad["f-ejecutoria"]),
				numeroRadicado = formOportunidad["numero-radicado-user"].ToString().Replace("-", ""),
				cuentaCobro = formOportunidad["cuenta-cobro-user"],
				demandante = formOportunidad["demandante"]

			};
			var primeraInstancia = formOportunidad.Files["primera-instancia-file"];
			var segundaInstancia = formOportunidad.Files["segunda-instancia-file"];
			var LeadoportunidadId = 0;
			var resultJS = "error";
			var httpClient = new HttpClient();
			httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
			var response = await httpClient.PostAsJsonAsync("https://api2valuezbpm.azurewebsites.net/api/leadPersona", persona);
			if (response.IsSuccessStatusCode)
			{
				var responseStream = await response.Content.ReadAsStringAsync();
				var lead = JsonConvert.DeserializeObject<PersonaDTO>(responseStream);
				leadOportunidad.leadPersonaId = lead.id;
				var response2 = await httpClient.PostAsJsonAsync("https://api2valuezbpm.azurewebsites.net/api/leadOportunidad", leadOportunidad);
				if (response2.IsSuccessStatusCode)
				{
					var responseStream3 = await response2.Content.ReadAsStringAsync();
					var resultLead = JsonConvert.DeserializeObject<OportunidadDTO>(responseStream3);
					LeadoportunidadId = (int)resultLead.id;
                    if (LeadoportunidadId > 0)
					{
						leadOportunidad.id = (int)resultLead.id;
						if(primeraInstancia != null)
						{
							MultipartFormDataContent formDataPI = new MultipartFormDataContent();
							formDataPI.Add(new StringContent(LeadoportunidadId.ToString()), "codArchivo");
							Stream streamPDF = primeraInstancia.OpenReadStream();
							if (streamPDF != null)
							{
								var contentPDF = new StreamContent(streamPDF);
								contentPDF.Headers.ContentType = MediaTypeHeaderValue.Parse(primeraInstancia.ContentType);
								formDataPI.Add(contentPDF, "UrlSoporte", primeraInstancia.Name);
							}
							httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
							var responseArchivoPI = await httpClient.PostAsync("https://api2valuezbpm.azurewebsites.net/api/archivo?EmpresaId=" + _configuration.GetSection("LandingPage:CotizadorLead:Empresa").Value + "&ProyectoId=" + _configuration.GetSection("LandingPage:CotizadorLead:Proyecto").Value + "&Agrupacion=" + _configuration.GetSection("LandingPage:CotizadorLead:Agrupacion").Value + "&ArchivoCategoriaId=" + _configuration.GetSection("LandingPage:CotizadorLead:Categoria").Value + "&ArchivoSubcategoriaId=" + _configuration.GetSection("LandingPage:CotizadorLead:SubCategoria:PrimeraInstancia").Value, formDataPI);
							if (responseArchivoPI.IsSuccessStatusCode)
							{
								resultJS = "success";
							}
							else
							{
								var responseStreamError = await response.Content.ReadAsStringAsync();
                                resultJS = "error";
							}
						}
						if(segundaInstancia != null)
						{
							MultipartFormDataContent formDataSI = new MultipartFormDataContent();
							formDataSI.Add(new StringContent(LeadoportunidadId.ToString()), "codArchivo");
							Stream streamPDF = segundaInstancia.OpenReadStream();
							if (streamPDF != null)
							{
								var contentPDF = new StreamContent(streamPDF);
								contentPDF.Headers.ContentType = MediaTypeHeaderValue.Parse(segundaInstancia.ContentType);
								formDataSI.Add(contentPDF, "UrlSoporte", segundaInstancia.Name);
							}
							httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
							var responseArchivoSI = await httpClient.PostAsync("https://api2valuezbpm.azurewebsites.net/api/archivo?EmpresaId=" + _configuration.GetSection("LandingPage:CotizadorLead:Empresa").Value + "&ProyectoId=" + _configuration.GetSection("LandingPage:CotizadorLead:Proyecto").Value + "&Agrupacion=" + _configuration.GetSection("LandingPage:CotizadorLead:Agrupacion").Value + "&ArchivoCategoriaId=" + _configuration.GetSection("LandingPage:CotizadorLead:Categoria").Value + "&ArchivoSubcategoriaId=" + _configuration.GetSection("LandingPage:CotizadorLead:SubCategoria:SegundaInstancia").Value, formDataSI);
							if (responseArchivoSI.IsSuccessStatusCode)
							{
								resultJS = "success";
							}
							else
							{
								var responseStreamError = await response.Content.ReadAsStringAsync();
                                resultJS = "error";

                            }
                        }
					}
                    resultJS = "success";
                    _mail.SendSuccess(persona, leadOportunidad);
                }
                else
				{
					var responseStream5 = await response2.Content.ReadAsStringAsync();
                    if (responseStream5.Contains("Ya existe un lead con numero de radicado")) { 
						resultJS = "existing";
						_mail.SendError(persona, leadOportunidad);
					}
				}
			}
			else
			{
				var responseStream2 = await response.Content.ReadAsStringAsync();
                resultJS = "error";
            }
			return resultJS;
		}


    }
}
