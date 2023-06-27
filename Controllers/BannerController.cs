using confinancia.Models;
using confinancia.Models.JsonDTO;
using confinancia.Services.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.Unicode;

namespace confinancia.Controllers
{
	public class BannerController : Controller
	{
		#region CONSTRUCTOR
		private readonly IConfiguration _configuration;
        private readonly IGetToken _getToken;

        public BannerController(IConfiguration configuration, IGetToken getToken)
		{
			_configuration = configuration;
            _getToken = getToken;
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
		public IActionResult Create()
		{
			return View();
		}

		[Authorize]
		[HttpPost]
		public async Task<bool> Upload(IFormFile obj)
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
			var response = await client.PostAsync("https://api2valuezbpm.azurewebsites.net/api/archivo?EmpresaId=" + _configuration.GetSection("LandingPage:Banner:Empresa").Value + "&ProyectoId=" + _configuration.GetSection("LandingPage:Banner:Proyecto").Value + "&Agrupacion=" + _configuration.GetSection("LandingPage:Banner:SubCategoriaSuperior:Agrupacion").Value + "&ArchivoCategoriaId=" + _configuration.GetSection("LandingPage:Banner:ArchivoCategoria").Value + "&ArchivoSubcategoriaId=" + _configuration.GetSection("LandingPage:Banner:SubCategoriaSuperior:Id").Value, form);
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
