using System.Diagnostics;
using confinancia.Models;
using confinancia.Models.JsonDTO;
using confinancia.Services.Token;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static System.Collections.Specialized.BitVector32;


namespace noa.Controllers;

public class LandingPageController : Controller
{

	#region CONSTRUCTOR
	private readonly IConfiguration _configuration;
    private readonly IGetToken _getToken;

    public LandingPageController(IConfiguration configuration, IGetToken getToken)
	{
		_configuration = configuration;
        _getToken = getToken;
    }
	#endregion

    [Route("/")]
	[HttpGet]
    public async Task<IActionResult> Index()
    {

		var token = await _getToken.GetTokenV();
		if (token == "")
		{
			return NotFound();
		}
		if (token.Length == 177)
		{
			var client = new HttpClient();

            #region DropDown Regimen
            var request = new HttpRequestMessage(HttpMethod.Get, "https://api2valuezbpm.azurewebsites.net/api/regimen?Pagina=1&RegistrosPorPagina=10");
			request.Headers.Add("Authorization", "Bearer " + token);
			var response = await client.SendAsync(request);
			if (response.IsSuccessStatusCode)
			{
				var responseStream = await response.Content.ReadAsStringAsync();
				var result = JsonConvert.DeserializeObject<List<DropDownListDTO>>(responseStream);
				ViewBag.LstRegimen = result;
			}
			else
			{
                ViewBag.LstRegimen = new List<DropDownListDTO>();
            }
            #endregion

            #region DropDown Tipo Providencia
            var requestProvidencia = new HttpRequestMessage(HttpMethod.Get, "https://api2valuezbpm.azurewebsites.net/api/tipoProvidencia?Pagina=1&RegistrosPorPagina=20");
            requestProvidencia.Headers.Add("Authorization", "Bearer " + token);
            var responseProvidencia = await client.SendAsync(requestProvidencia);
            if (responseProvidencia.IsSuccessStatusCode)
            {
                var responseStream = await responseProvidencia.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<DropDownListDTO>>(responseStream);
                ViewBag.LstProvidencia = result;
            }
            else
            {
                ViewBag.LstProvidencia = new List<DropDownListDTO>();
            }
            #endregion

            #region DropDown Tipo Corporacion
            var requestCorporacion = new HttpRequestMessage(HttpMethod.Get, "https://api2valuezbpm.azurewebsites.net/api/corporacion/tipoCorporacion?Pagina=1&RegistrosPorPagina=10");
            requestCorporacion.Headers.Add("Authorization", "Bearer " + token);
            var responseCorporacion = await client.SendAsync(requestCorporacion);
            if (responseCorporacion.IsSuccessStatusCode)
            {
                var responseStream = await responseCorporacion.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<TipoCorporacionDTO>>(responseStream);
                ViewBag.LstCorporacion = result;
            }
            else
            {
                ViewBag.LstCorporacion = new List<TipoCorporacionDTO>();
            }
            #endregion

            #region DropDown Medio de Control
            var requestMedioControl = new HttpRequestMessage(HttpMethod.Get, "https://api2valuezbpm.azurewebsites.net/api/medioControl?Pagina=1&RegistrosPorPagina=20");
            requestMedioControl.Headers.Add("Authorization", "Bearer " + token);
            var responseMedioControl = await client.SendAsync(requestMedioControl);
            if (responseMedioControl.IsSuccessStatusCode)
            {
                var responseStream = await responseMedioControl.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<DropDownListDTO>>(responseStream);
                ViewBag.LstMedioControl = result;
            }
            else
            {
                ViewBag.LstMedioControl = new List<DropDownListDTO>();
            }
            #endregion

            #region DropDown Entidad pagaduria
            var requestEntidad = new HttpRequestMessage(HttpMethod.Get, "https://api2valuezbpm.azurewebsites.net/api/entidadpagaduria");
            requestEntidad.Headers.Add("Authorization", "Bearer " + token);
            var responseEndidad = await client.SendAsync(requestEntidad);
            if (responseEndidad.IsSuccessStatusCode)
            {
                var responseStream = await responseEndidad.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<DropDownListDTO>>(responseStream);
                ViewBag.LstEntidad = result;
            }
            else
            {
                ViewBag.LstEntidad = new List<DropDownListDTO>();
            }


            #endregion
        }
        return View();
    }

	public async Task<List<DropDownListDTO>> CorporacionId(int id)
	{
        var token = await _getToken.GetTokenV();
        if (token == "")
        {
            return new List<DropDownListDTO>();
        }
		if (token.Length == 177)
		{
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://api2valuezbpm.azurewebsites.net/api/corporacion/" + id + "?Pagina=1&RegistrosPorPagina=100");
            request.Headers.Add("Authorization", "Bearer " + token);
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<DropDownListDTO>>(responseStream);
                return result;
            }
            else
            {
                return new List<DropDownListDTO>();
            }
        }
        return new List<DropDownListDTO>();
    }

	[HttpGet]
	public IActionResult ServicioLanding(string seccion)
	{
		ViewBag.Seccion = seccion;
		return View();
	}

	[HttpGet]
    public IActionResult DataPolicy()
    {
        return View();
    }

    public IActionResult SignIn()
	{
		var props = new AuthenticationProperties();
		props.RedirectUri = "/LandingPage/SignInSuccess";
		return Challenge(props);
	}
	public async Task<IActionResult> SignInSuccess()
	{
		var mail = User.Identities.First().Claims.LastOrDefault().Value;
		var obj = new CuentasLoginDTO()
		{
			Id = _configuration.GetSection("Variables:IdLogin").Value,
			Email = mail,
			password = "123456789"
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
			var tokenSuccess = JsonConvert.DeserializeObject<TokenValuezDTO>(responseStream);
			CookieOptions options = new CookieOptions()
			{
				Expires = DateTime.Now.AddHours(1)
			};
			Response.Cookies.Append(_configuration.GetSection("Variables:Cookie").Value, tokenSuccess.Token, options);
			return RedirectToAction("Index", "Home");
		}
		else
		{
			return RedirectToAction("Index", "LandingPage");
		}
	}

	public IActionResult SignOut(string signOutType)
	{
		if (signOutType == "app")
		{
			HttpContext.SignOutAsync().Wait();
		}
		if (signOutType == "all")
		{
			return Redirect("https://login.microsoftonline.com/common/oauth2/v2.0/logout");
		}
		return RedirectToAction("Index");
	}
}