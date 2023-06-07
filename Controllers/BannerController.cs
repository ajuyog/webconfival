using confinancia.Models;
using confinancia.Models.JsonDTO;
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
		public BannerController(IConfiguration configuration)
		{
			_configuration = configuration;
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
		public async Task<string> SaveStorage(IFormFile obj, string name, string URL)
		{
			var url = "";
			var token = Request.Cookies[_configuration.GetSection("Variables:Cookie").Value];
			var client = new HttpClient();
			MultipartFormDataContent form = new MultipartFormDataContent();
			form.Add(new StringContent("1"), "codArchivo");
			Stream streamPDF = obj.OpenReadStream();
			if (streamPDF != null)
			{
				var contentPDF = new StreamContent(streamPDF);
				contentPDF.Headers.ContentType = MediaTypeHeaderValue.Parse(obj.ContentType);
				form.Add(contentPDF, "UrlSoporte", obj.Name);
			}
			client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
			//var response = await client.PostAsync("https://api2valuezbpm.azurewebsites.net/api/repositorioArchivo?Agrupacion1=" + _configuration.GetSection("LandingPage:BannerInicio:A1").Value + "&Agrupacion2=" + _configuration.GetSection("LandingPage:BannerInicio:A2").Value + "&Agrupacion3=" + _configuration.GetSection("LandingPage:BannerInicio:A3").Value + "&Agrupacion4=" + _configuration.GetSection("LandingPage:BannerInicio:A4").Value + "&Agrupacion5=" + _configuration.GetSection("LandingPage:BannerInicio:A5").Value, form);
			var response = await client.PostAsync("https://api2valuezbpm.azurewebsites.net/api/repositorioArchivo?Agrupacion1=" + _configuration.GetSection("LandingPage:Blog:A1").Value + "&Agrupacion2=" + _configuration.GetSection("LandingPage:Blog:A2").Value + "&Agrupacion3=" + _configuration.GetSection("LandingPage:Blog:A3").Value + "&Agrupacion4=" + _configuration.GetSection("LandingPage:Blog:A4").Value + "&Agrupacion5=" + _configuration.GetSection("LandingPage:Blog:A5").Value, form);

			if (response.IsSuccessStatusCode)
			{
				var responseStream = await response.Content.ReadAsStringAsync();
				var arrayJson = responseStream.Split(",");
				url = arrayJson[1].Replace("url", "").Trim().Replace('"', ' ').Trim().Remove(0, 1).Trim();
				return url;
			}
			else
			{
				return url;
			}
		}

		[Authorize]
		[HttpGet]
		public async Task<bool> SaveDB(string urlStorage)
		{
			var token = Request.Cookies[_configuration.GetSection("Variables:Cookie").Value];
			var obj = new CargueMediaDTO()
			{
				Id = 59,
				ProyectoId = 2,
				MenuId = 22,
				Ruta = "/" + _configuration.GetSection("LandingPage:BannerInicio:A1").Value + "/" + _configuration.GetSection("LandingPage:BannerInicio:A2").Value + "/" + _configuration.GetSection("LandingPage:BannerInicio:A3").Value + "/" + _configuration.GetSection("LandingPage:BannerInicio:A4").Value + "/" + _configuration.GetSection("LandingPage:BannerInicio:A5").Value,
				UrlMedia = urlStorage,
				Estado = true
			};
			var json = JsonConvert.SerializeObject(obj);
			var client = new HttpClient();
			var request = new HttpRequestMessage(HttpMethod.Post, "https://api2valuezbpm.azurewebsites.net/api/mediaUpload");
			request.Headers.Add("Authorization", "Bearer " + token);
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
	}
}
