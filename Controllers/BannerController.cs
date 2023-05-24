using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

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
		public async Task<bool> Save(IFormFile obj, string name, string URL)
		{
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
			var response = await client.PostAsync("https://api2valuezbpm.azurewebsites.net/api/repositorioArchivo?Agrupacion1=" + _configuration.GetSection("LandingPage:BannerInicio:A1").Value + "&Agrupacion2=" + _configuration.GetSection("LandingPage:BannerInicio:A2").Value + "&Agrupacion3=" + _configuration.GetSection("LandingPage:BannerInicio:A3").Value + "&Agrupacion4=" + _configuration.GetSection("LandingPage:BannerInicio:A4").Value + "&Agrupacion5=" + _configuration.GetSection("LandingPage:BannerInicio:A5").Value, form);
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
