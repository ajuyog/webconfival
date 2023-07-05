using Azure;
using frontend.Models;
using frontend.Models.JsonDTO;
using frontend.Services.Graph;
using frontend.Services.Token;
using frontend.Services.Utilidaddes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using Microsoft.Graph.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;

namespace frontend.Controllers
{
	public class OportunidadController : Controller
	{
		#region CONSTRUCTOR
		private readonly IConfiguration _configuration;
		private readonly IGetToken _getToken;
        private readonly IMail _mail;
        public OportunidadController(IConfiguration configuration, IGetToken getToken, ISendMail sendMail, IMail mail )
		{
			_configuration = configuration;
			_getToken = getToken;
            _mail = mail;
        }
		#endregion

		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public async Task<bool> SendOTP(string entrada)
		{
			var pattern = "(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|\"(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21\\x23-\\x5b\\x5d-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])*\")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\\[(?:(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9]))\\.){3}(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9])|[a-z0-9-]*[a-z0-9]:(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21-\\x5a\\x53-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])+)\\])";
			Regex validMail = new Regex(pattern);

			var token = _getToken.GetTokenV();
			if (token.Result == "")
			{
				return false;
			}
			if (token.Result.Length == 177)
			{
				var client = new HttpClient();
				var request = new HttpRequestMessage(HttpMethod.Post, "https://api2valuezbpm.azurewebsites.net/api/otp/enviar?longitud=" + _configuration.GetSection("OTP:Longitud").Value + "&caducidadSg=" + _configuration.GetSection("OTP:Caducidad").Value);
				request.Headers.Add("Authorization", "Bearer " + token.Result);
				var obj = new EnvioOTPDTO()
				{
					FuenteOTPId = validMail.IsMatch(entrada) == true ? Convert.ToInt32(_configuration.GetSection("OTP:FuenteOTPMail").Value) : Convert.ToInt32(_configuration.GetSection("OTP:FuenteOTPSMS").Value),
					EmpresaId = Convert.ToInt32(_configuration.GetSection("OTP:EmpresaId").Value),
					Entrada = validMail.IsMatch(entrada) == true ? entrada : "57" + entrada,
				};
				var json = JsonConvert.SerializeObject(obj);
				var content = new StringContent(json, null, "application/json");
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
			return false;
		}

		//[HttpGet]
		//public bool SendOTP(string entrada)
		//{
		//	return true;
		//}

		[HttpGet]
		public async Task<string> ValidOTP(string llave, string entrada)
		{
			var pattern = "(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|\"(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21\\x23-\\x5b\\x5d-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])*\")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\\[(?:(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9]))\\.){3}(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9])|[a-z0-9-]*[a-z0-9]:(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21-\\x5a\\x53-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])+)\\])";
			Regex validMail = new Regex(pattern);
			var token = _getToken.GetTokenV();
			if (token.Result == "")
			{
				return "error";
			}
			if (token.Result.Length == 177)
			{
				var client = new HttpClient();
				var request = new HttpRequestMessage(HttpMethod.Post, "https://api2valuezbpm.azurewebsites.net/api/otp/validatemailotp");
				request.Headers.Add("Authorization", "Bearer " + token.Result);
				var obj = new ValidatOTPDTO()
				{
					FuenteOTPId = validMail.IsMatch(entrada) == true ? Convert.ToInt32(_configuration.GetSection("OTP:FuenteOTPMail").Value) : Convert.ToInt32(_configuration.GetSection("OTP:FuenteOTPSMS").Value),
					EmpresaId = Convert.ToInt32(_configuration.GetSection("OTP:EmpresaId").Value),
					Entrada = validMail.IsMatch(entrada) == true ? entrada : "57" + entrada,
					Llave = llave
				};
				var json = JsonConvert.SerializeObject(obj);
				var content = new StringContent(json, null, "application/json");
				request.Content = content;
				var response = await client.SendAsync(request);
				if (response.IsSuccessStatusCode)
				{
					return "success";
				}
				else
				{
					var responseStream = await response.Content.ReadAsStringAsync();
					return responseStream;
				}
			}
			return "error";
		}

		//[HttpGet]
		//public string ValidOTP(string llave, string entrada)
		//{
		//	return "success";
		//}

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
            var result = "error";
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

			var createPerson = await CreatePerson(persona);
			if (createPerson == null) { return result; }

            leadOportunidad.leadPersonaId = createPerson.id;
            var createLead = await CreateLead(leadOportunidad);
			if (createLead == "error") { return result; }
            if (createLead == "existe") 
			{
                _mail.SendError(persona, leadOportunidad);
				result = "existing";
                return result; 
			}
            if (primeraInstancia != null)
            {
                var instancia = "primeraInstancia";
                var createPrimeraInstancia = await CreateInstancia(primeraInstancia, Convert.ToInt32(createLead), instancia);
                if (!createPrimeraInstancia) { return result; }
            }
            if (segundaInstancia != null)
            {
                var instancia = "segundaInstancia";
                var createSegundaInstancia = await CreateInstancia(primeraInstancia, Convert.ToInt32(createLead), instancia);
                if (!createSegundaInstancia) { return result; }
            }
            _mail.SendSuccess(persona, leadOportunidad);
            result = "success";
			return result;
		}

		[HttpPost]
		public async Task<PersonaDTO> CreatePerson(PersonaDTO obj)
		{
			var result = new PersonaDTO();
            var token = await _getToken.GetTokenV();
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await client.PostAsJsonAsync("https://api2valuezbpm.azurewebsites.net/api/leadPersona", obj);
			if (response.IsSuccessStatusCode)
			{
                var responseStream = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<PersonaDTO>(responseStream);
            }
			return result;
        }

		[HttpPost]
		public async Task<string> CreateLead(OportunidadDTO obj)
		{
			var token = await _getToken.GetTokenV();
			var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await client.PostAsJsonAsync("https://api2valuezbpm.azurewebsites.net/api/leadOportunidad", obj);
			if (response.IsSuccessStatusCode)
			{
				var responseStream = await response.Content.ReadAsStringAsync();
				var result = JsonConvert.DeserializeObject<OportunidadDTO>(responseStream);
				return result.id.ToString();
			}
            else
            {
                var responseStream = await response.Content.ReadAsStringAsync();
                if (responseStream.Contains("Ya existe un lead con numero de radicado"))
                {
					return "existe";
				}
				else
				{
					return "error";
				}
            }
		}

		[HttpPost]
		public async Task<bool> CreateInstancia(IFormFile obj, int id, string instancia)
		{
            var client = new HttpClient();
            var token = await _getToken.GetTokenV();
            MultipartFormDataContent formData = new MultipartFormDataContent();
            formData.Add(new StringContent(id.ToString()), "codArchivo");
            Stream streamPDF = obj.OpenReadStream();
            if (streamPDF != null)
            {
                var contentPDF = new StreamContent(streamPDF);
                contentPDF.Headers.ContentType = MediaTypeHeaderValue.Parse(obj.ContentType);
                formData.Add(contentPDF, "UrlSoporte", obj.Name);
            }
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
			var httpString = "";
			if(instancia == "primeraInstancia") { httpString = _configuration.GetSection("LandingPage:CotizadorLead:Empresa").Value + "&ProyectoId=" + _configuration.GetSection("LandingPage:CotizadorLead:Proyecto").Value + "&Agrupacion=" + _configuration.GetSection("LandingPage:CotizadorLead:Agrupacion").Value + "&ArchivoCategoriaId=" + _configuration.GetSection("LandingPage:CotizadorLead:Categoria").Value + "&ArchivoSubcategoriaId=" + _configuration.GetSection("LandingPage:CotizadorLead:SubCategoria:PrimeraInstancia").Value; };
			if(instancia == "segundaInstancia") { httpString = _configuration.GetSection("LandingPage:CotizadorLead:Empresa").Value + "&ProyectoId=" + _configuration.GetSection("LandingPage:CotizadorLead:Proyecto").Value + "&Agrupacion=" + _configuration.GetSection("LandingPage:CotizadorLead:Agrupacion").Value + "&ArchivoCategoriaId=" + _configuration.GetSection("LandingPage:CotizadorLead:Categoria").Value + "&ArchivoSubcategoriaId=" + _configuration.GetSection("LandingPage:CotizadorLead:SubCategoria:SegundaInstancia").Value; };
            var response = await client.PostAsync("https://api2valuezbpm.azurewebsites.net/api/archivo?EmpresaId=" + httpString, formData);
            if (response.IsSuccessStatusCode)
            {
				return true;
            }
            else
            {
                var responseStreamError = await response.Content.ReadAsStringAsync();
                return false;
            }
        }
    }
}
