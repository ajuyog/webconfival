using frontend.Models;
using frontend.Models.JsonDTO;
using frontend.Services.Graph;
using frontend.Services.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.Unicode;

namespace frontend.Controllers
{
	public class BannerController : Controller
	{
		#region CONSTRUCTOR
		private readonly IConfiguration _configuration;
        private readonly IGetToken _getToken;
        private readonly IGraphServices _graphServices;

        public BannerController(IConfiguration configuration, IGetToken getToken, IGraphServices graphServices)
		{
			_configuration = configuration;
            _getToken = getToken;
            _graphServices = graphServices;
        }
		#endregion

		[Authorize]
		[HttpGet]
		public IActionResult Get()
		{
			return View();

		}

		[Authorize]
		[HttpGet]
		public async Task<IActionResult> InicioSuperior()
		{
            var objToken = await _getToken.GetTokenMicrosoft();
            ViewBag.Imagen = await _graphServices.ImgProfile(objToken.access_token);
            var me = await _graphServices.GetMeGraph(objToken.access_token);
            ViewBag.user = me.DisplayName;

            ViewBag.Nombre = "Inicio";
			ViewBag.SubCategoria = "Superior";
			ViewBag.Function = "BannerInicioSuperior()";
			return View("~/Views/Banner/Create.cshtml");
		}

		[Authorize]
		[HttpGet]
		public async Task<IActionResult> ContactoSuperior()
		{
            var objToken = await _getToken.GetTokenMicrosoft();
            ViewBag.Imagen = await _graphServices.ImgProfile(objToken.access_token);
            var me = await _graphServices.GetMeGraph(objToken.access_token);
            ViewBag.user = me.DisplayName;

            ViewBag.Nombre = "Contacto";
			ViewBag.SubCategoria = "Superior";
			ViewBag.Function = "BannerContactoSuperior()";
			return View("~/Views/Banner/Create.cshtml");
		}

		[Authorize]
		[HttpPost]
		public async Task<bool> InicioSuperior(IFormFile obj)
		{
            var token = await _getToken.GetTokenV();
			var client = new HttpClient();
			MultipartFormDataContent form = new MultipartFormDataContent();
			form.Add(new StringContent("0"), "codArchivo");
			Stream streamPDF = obj.OpenReadStream();
			if (streamPDF != null)
			{
				var contentPDF = new StreamContent(streamPDF);
				contentPDF.Headers.ContentType = MediaTypeHeaderValue.Parse(obj.ContentType);
				form.Add(contentPDF, "UrlSoporte", obj.Name);
			}
			client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
			var response = await client.PostAsync("https://api2valuezbpm.azurewebsites.net/api/archivo?EmpresaId=" + _configuration.GetSection("LandingPage:BannerInicio:Empresa").Value + "&ProyectoId=" + _configuration.GetSection("LandingPage:BannerInicio:Proyecto").Value + "&Agrupacion=" + _configuration.GetSection("LandingPage:BannerInicio:Agrupacion").Value + "&ArchivoCategoriaId=" + _configuration.GetSection("LandingPage:BannerInicio:Categoria").Value + "&ArchivoSubcategoriaId=" + _configuration.GetSection("LandingPage:BannerInicio:SubCategoria:Superior").Value, form);
			if (response.IsSuccessStatusCode)
			{
                var responseStream = await response.Content.ReadAsStringAsync();
                return true;
			}
			else
			{
                var responseStreamError = await response.Content.ReadAsStringAsync();
				return false;
            }
        }

		[Authorize]
		[HttpPost]
		public async Task<bool> ContactoSuperior(IFormFile obj)
		{
			var token = await _getToken.GetTokenV();
			var client = new HttpClient();
			MultipartFormDataContent form = new MultipartFormDataContent();
			form.Add(new StringContent("0"), "codArchivo");
			Stream streamPDF = obj.OpenReadStream();
			if (streamPDF != null)
			{
				var contentPDF = new StreamContent(streamPDF);
				contentPDF.Headers.ContentType = MediaTypeHeaderValue.Parse(obj.ContentType);
				form.Add(contentPDF, "UrlSoporte", obj.Name);
			}
			client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
			var response = await client.PostAsync("https://api2valuezbpm.azurewebsites.net/api/archivo?EmpresaId=" + _configuration.GetSection("LandingPage:BannerContacto:Empresa").Value + "&ProyectoId=" + _configuration.GetSection("LandingPage:BannerContacto:Proyecto").Value + "&Agrupacion=" + _configuration.GetSection("LandingPage:BannerContacto:Agrupacion").Value + "&ArchivoCategoriaId=" + _configuration.GetSection("LandingPage:BannerContacto:Categoria").Value + "&ArchivoSubcategoriaId=" + _configuration.GetSection("LandingPage:BannerContacto:SubCategoria:Superior").Value, form);
			if (response.IsSuccessStatusCode)
			{
                var responseStream = await response.Content.ReadAsStringAsync();
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
