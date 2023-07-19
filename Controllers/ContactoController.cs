using System.Diagnostics;
using frontend.Models;
using frontend.Models.JsonDTO;
using frontend.Services.Contacto;
using frontend.Services.Token;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace noa.Controllers;

public class ContactoController : Controller
{
	#region CONSTRUCTOR
	private readonly IGetToken _getToken;
	private readonly IConfiguration _configuration;
	private readonly IContactoServices _contactoServices;

	public ContactoController(IGetToken getToken, IConfiguration configuration, IContactoServices contactoServices)
    {
		_getToken = getToken;
		_configuration = configuration;
		_contactoServices = contactoServices;
	}
	#endregion

	[Route("/Contacto")]
    [HttpGet]
    public async Task<IActionResult> Index()
    {
		var model = new List<BannerDTO>();
		var token = await _getToken.GetTokenV();
		if (token == "")
		{
			return NotFound();
		}
		if (token.Length == 177)
		{
			#region Banner Principal
			var client = new HttpClient();
			var request = new HttpRequestMessage(HttpMethod.Get, "https://api2valuezbpm.azurewebsites.net/api/archivo/empresaProyectoArchivoSubCategoria?Pagina=1&RegistrosPorPagina=100&EmpresaId=" + _configuration.GetSection("LandingPage:BannerContacto:Empresa").Value + "&ProyectoId=" + _configuration.GetSection("LandingPage:BannerContacto:Proyecto").Value + "&Agrupacion=" + _configuration.GetSection("LandingPage:BannerContacto:Agrupacion").Value + "&ArchivoSubcategoriaId=" + _configuration.GetSection("LandingPage:BannerContacto:SubCategoria:Superior").Value + "&OrigenId=0");
			request.Headers.Add("Authorization", "Bearer " + token);
			var response = await client.SendAsync(request);
			if (response.IsSuccessStatusCode)
			{
				var posicion = 0;
				var responseStream = await response.Content.ReadAsStringAsync();
				var lstBanners = JsonConvert.DeserializeObject<List<BannerDTO>>(responseStream);
				foreach (var item in lstBanners)
				{
					item.Posicion = posicion;
					posicion++;
				}
				model = lstBanners;
			}
			else
			{
				var bannerDefault = new BannerDTO()
				{
					Posicion = 0,
					UrlSoporte = "https://storageaccountisaac.blob.core.windows.net/apivaluezdocumental/1/2/bannerprincipal/2/1/0/acc366a7-1d50-4576-b783-89217db748e9"
				};
				model.Add(bannerDefault);
			}
			#endregion
		}
		return View(model);
    }

	[HttpGet]
	public IActionResult ServicioLanding()
	{
		return View();
	}

	[HttpGet]
	public async Task<bool> CreateLead(string name, string email, string phone, int reparacion)
	{
		var obj = new LeadCampaniaDTO()
		{
			email = email,
			name = name,
			reparacion = reparacion == 1 ? true : false,
			phone = phone
		};
		return await _contactoServices.CreateLead(obj);
	}
}