using System.Diagnostics;
using System.Reflection;
using frontend.Models;
using frontend.Models.JsonDTO;
using frontend.Services.Token;
using Microsoft.AspNetCore.Authentication;
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
            var requestTipoActor = new HttpRequestMessage(HttpMethod.Get, "https://api2valuezbpm.azurewebsites.net/api/TipoActor");
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

    [HttpGet]
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
            return RedirectToAction("Index", "Home");
        }
        else
        {
            return RedirectToAction("Index", "LandingPage");
        }
    }

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