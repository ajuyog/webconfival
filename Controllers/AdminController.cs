using frontend.Services.Graph;
using frontend.Services.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace frontend.Controllers
{
	public class AdminController : Controller
	{
		private readonly IGetToken _getToken;
		private readonly IGraphServices _graphServices;
		private readonly IConfiguration _configuration;
		#region CONSTRUCTOR
		public AdminController(IGetToken getToken, IGraphServices graphServices, IConfiguration configuration)
        {
			_getToken = getToken;
			_graphServices = graphServices;
			_configuration = configuration;
		}
        #endregion

		[Authorize, HttpGet]
		public async Task<IActionResult> Carousel()
		{
			var objToken = await _getToken.GetTokenMicrosoft();
			ViewBag.Imagen = await _graphServices.ImgProfile(objToken.access_token);
			var me = await _graphServices.GetMeGraph(objToken.access_token);
			ViewBag.user = me.DisplayName;
			return View();
		}

		[Authorize]
		[HttpPost]
		public async Task<bool> UploadCarousel(IFormFile obj)
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
			var response = await client.PostAsync("https://api2valuezbpm.azurewebsites.net/api/archivo?EmpresaId=" + _configuration["LandingPage:BannerInicio:Empresa"] + "&ProyectoId=" + _configuration["LandingPage:BannerInicio:Proyecto"] + "&Agrupacion=" + _configuration["LandingPage:BannerInicio:Agrupacion"] + "&ArchivoCategoriaId=" + _configuration["LandingPage:BannerInicio:Categoria"] + "&ArchivoSubcategoriaId=" + _configuration["LandingPage:BannerInicio:SubCategoria:Inferior"], form);
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
