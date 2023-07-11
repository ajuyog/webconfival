using System.Diagnostics;
using System.Reflection;
using System.Security.Claims;
using frontend.Models;
using frontend.Models.JsonDTO;
using frontend.Services.Token;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Kiota.Abstractions.Extensions;
using Newtonsoft.Json;
using Tavis.UriTemplates;
using static System.Collections.Specialized.BitVector32;

namespace noa.Controllers;

public class LandingPageController : Controller
{

    #region CONSTRUCTOR
    private readonly IConfiguration _configuration;
    private readonly IGetToken _getToken;
	private readonly UserManager<IdentityUser> _userManager;
	private readonly SignInManager<IdentityUser> _signInManager;

	public LandingPageController(IConfiguration configuration, IGetToken getToken, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _configuration = configuration;
        _getToken = getToken;
		_userManager = userManager;
		_signInManager = signInManager;
	}
    #endregion

    /// <summary>
    /// Devuelve la vista Inicio de la LandingPage
    /// </summary>
    /// <param name="mensaje"></param>
    /// <returns></returns>
    [Route("/")]
    [HttpGet]
    public async Task<IActionResult> Index(string mensaje = null)
    {
        if(mensaje != null)
        {
            ViewData["mensaje"] = mensaje;
        }
        var model = new List<BannerDTO>();
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

            #region TipoActor 
            var requestTipoActor = new HttpRequestMessage(HttpMethod.Get, "https://api2valuezbpm.azurewebsites.net/api/TipoActor/landing?Pagina=1&RegistrosPorPagina=10");
            requestTipoActor.Headers.Add("Authorization", "Bearer " + token);
            var responseTipoActor = await client.SendAsync(requestTipoActor);
            if (responseTipoActor.IsSuccessStatusCode)
            {
                var responseStream = await responseTipoActor.Content.ReadAsStringAsync();
                var lstActores = JsonConvert.DeserializeObject<List<DropDownListDTO>>(responseStream);
                foreach (var item in lstActores)
                {
                    item.Nombre = item.Nombre.ToLower();
                    var mayus = item.Nombre[0].ToString().ToUpper();
                    item.Nombre = mayus + item.Nombre.Substring(1, item.Nombre.Length - 1);
                }
                ViewBag.TipoActores = lstActores;
            }
            else
            {
                ViewBag.TipoActores = new List<DropDownListDTO>();
            }
            #endregion

            #region Banner Principal
            var requestBannerSuperior = new HttpRequestMessage(HttpMethod.Get, "https://api2valuezbpm.azurewebsites.net/api/archivo/empresaProyectoArchivoSubCategoria?Pagina=1&RegistrosPorPagina=100&EmpresaId=" + _configuration.GetSection("LandingPage:BannerInicio:Empresa").Value + "&ProyectoId=" + _configuration.GetSection("LandingPage:BannerInicio:Proyecto").Value + "&Agrupacion=" + _configuration.GetSection("LandingPage:BannerInicio:Agrupacion").Value + "&ArchivoSubcategoriaId=" + _configuration.GetSection("LandingPage:BannerInicio:SubCategoria:Superior").Value + "&OrigenId=0");
            requestBannerSuperior.Headers.Add("Authorization", "Bearer " + token);
            var responseBannerSuperior = await client.SendAsync(requestBannerSuperior);
            if (responseBannerSuperior.IsSuccessStatusCode)
            {
                var posicion = 0;
                var responseStream = await responseBannerSuperior.Content.ReadAsStringAsync();
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

    /// <summary>
    /// Devuelve una lista de Corporacion para el formulario de oportunidad
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
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

    /// <summary>
    /// Devuelve la vista de servicios de la landingPage
    /// </summary>
    /// <param name="seccion"></param>
    /// <returns></returns>
    [HttpGet]
    public IActionResult ServicioLanding(string seccion)
    {
        ViewBag.Seccion = seccion;
        return View();
    }

    /// <summary>
    /// Devuelve la vista de Politica de tratamiento de datos
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult DataPolicy()
    {
        return View();
    }

    /// <summary>
    /// Permite el login en la intranet de webConfival con Microsoft
    /// </summary>
    /// <param name="proveedor"></param>
    /// <param name="urlRetorno"></param>
    /// <returns></returns>
	[HttpGet]
	public ChallengeResult SignIn(string proveedor, string? urlRetorno = null)
	{
        var proveedor2 = "Microsoft";
		var urlRedireccion = Url.Action("RegistrarUsuarioExterno", values: new { urlRetorno });
		var propiedades = _signInManager.ConfigureExternalAuthenticationProperties(proveedor2, urlRedireccion);
		return new ChallengeResult(proveedor2, propiedades);
	}

    /// <summary>
    /// Obtiene los tokens de Microsoft y realiza redirect a la Intranet
    /// </summary>
    /// <param name="urlRetorno"></param>
    /// <param name="remoteError"></param>
    /// <returns></returns>
    [HttpGet]
	public async Task<IActionResult> RegistrarUsuarioExterno(string? urlRetorno = null, string? remoteError = null)
	{
        urlRetorno = "~/Home/Index";
		var mensaje = "";

		if (remoteError != null)
		{
			mensaje = $"Error from external provider: {remoteError}";
			return RedirectToAction("Index", routeValues: new { mensaje });
		}

		var info = await _signInManager.GetExternalLoginInfoAsync();
		if (info == null)
		{
			mensaje = "Error loading external login information.";
			return RedirectToAction("Index", routeValues: new { mensaje });
		}

		string email = "";

		if (info.Principal.HasClaim(c => c.Type == ClaimTypes.Email))
		{
			email = info.Principal.FindFirstValue(ClaimTypes.Email)!;
		}
		else
		{
			mensaje = "Error leyendo el email del usuario del proveedor.";
			return RedirectToAction("Index", routeValues: new { mensaje });
		}

		var usuario = new IdentityUser() { UserName = email, Email = email };

		// Optener Tocken de Microsoft
		var props = new AuthenticationProperties();
		props.StoreTokens(info.AuthenticationTokens);
		props.IsPersistent = true;


		var resultadoLoginExterno = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: true, bypassTwoFactor: true);
		if (resultadoLoginExterno.Succeeded)
		{
			await _signInManager.SignInAsync(usuario, props, info.LoginProvider);
			return LocalRedirect(urlRetorno);
		}

		var resultadoCrearUsuario = await _userManager.CreateAsync(usuario);
		if (!resultadoCrearUsuario.Succeeded)
		{
			mensaje = resultadoCrearUsuario.Errors.First().Description;
			return RedirectToAction("Index", routeValues: new { mensaje });
		}

		var resultadoAgregarLogin = await _userManager.AddLoginAsync(usuario, info);
		if (resultadoAgregarLogin.Succeeded)
		{
			await _signInManager.SignInAsync(usuario, props, info.LoginProvider);
			return LocalRedirect(urlRetorno);
		}

		mensaje = "Ha ocurrido un error agregando el login.";
		return RedirectToAction("Index", "Home", routeValues: new { mensaje });
	}

    /// <summary>
    /// Permite el desloguear al usuario y redireciona a la landingPage
    /// </summary>
    /// <param name="signOutType"></param>
    /// <returns></returns>
	[HttpPost]
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