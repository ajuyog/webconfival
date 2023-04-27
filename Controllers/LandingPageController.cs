using System.Diagnostics;
using confinancia.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace noa.Controllers;

public class LandingPageController : Controller
{

    private readonly ILogger<LandingPageController> _logger;
    private LandingData ld = new LandingData();

    public LandingPageController(ILogger<LandingPageController> logger)
    {
        _logger = logger;
    }

    [Route("/")]
    [HttpGet]
    public async Task<IActionResult> Index(string mensaje ="")
    {
        var httpClient = new HttpClient();
        var json = await httpClient.GetStringAsync("https://apilead.azurewebsites.net/api/entidadpagaduria");

        List<EntidadPagaduria> allEP = JsonConvert.DeserializeObject<List<EntidadPagaduria>>(json);
        json = await httpClient.GetStringAsync("https://apilead.azurewebsites.net/api/tipoNombramiento");
        List<TipoNombramiento> allTN = JsonConvert.DeserializeObject<List<TipoNombramiento>>(json);

        ld.EP = allEP;
        ld.TN = allTN;
        ld.data = "";
        return View(ld);
    }

    [HttpPost]
    public async Task<IActionResult> Index()
    {
        var httpClient = new HttpClient();
        
        ld.data = "";
        var json = await httpClient.GetStringAsync("https://apilead.azurewebsites.net/api/entidadpagaduria");
        List<EntidadPagaduria> allEP = JsonConvert.DeserializeObject<List<EntidadPagaduria>>(json);
        json = await httpClient.GetStringAsync("https://apilead.azurewebsites.net/api/tipoNombramiento");
        List<TipoNombramiento> allTN = JsonConvert.DeserializeObject<List<TipoNombramiento>>(json);

        ld.EP = allEP;
        ld.TN = allTN;


        Persona objP = new Persona();
        objP.nombres = HttpContext.Request.Form["Nombre"];
        objP.apellidos = HttpContext.Request.Form["Apellido"];
        objP.codSecundario1 = HttpContext.Request.Form["Id"];
        objP.correoElectronico = HttpContext.Request.Form["Email"];
        objP.numeroContacto = HttpContext.Request.Form["Contacto"];
        objP.numeroContactoAdicional = HttpContext.Request.Form["ContactoAdi"];
        objP.politicaTratamientoDatos = true;
        objP.estado = true;

        SolicitudCredito SC = new SolicitudCredito();
        SC.montoSolicitud = Convert.ToInt64(HttpContext.Request.Form["Monto"]);
        SC.estado = true;
        SC.entidadPagaduriaId = Convert.ToInt16(HttpContext.Request.Form["idEp"]);
        SC.tipoNombramientoId = Convert.ToInt16(HttpContext.Request.Form["idTN"]);
        SC.plazoMeses = Convert.ToInt16(HttpContext.Request.Form["Plazog"]);
        //httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");
        //return Content("response" + responseStream3);


        var response = await httpClient.PostAsJsonAsync("https://apilead.azurewebsites.net/api/persona", objP);
        if (response.IsSuccessStatusCode)
        {
            var responseStream = await response.Content.ReadAsStringAsync();
            var objPN= JsonConvert.DeserializeObject<Persona>(responseStream);
            SC.personaId = objPN.id;
            var response2 = await httpClient.PostAsJsonAsync("https://apilead.azurewebsites.net/api/solicitudCredito", SC);
            
            if (response2.IsSuccessStatusCode)
            {
                var responseStream3 = await response2.Content.ReadAsStringAsync();
                ld.data = "Gracias por contactarte, un asesor comercial se estará comunicando contigo";// responseStream3;
                /// return Content("response" + responseStream3);
            }
            else
            {
                ld.data = responseStream;// responseStream3;

            }
            // return Content("response" + responseStream);
        }
        else
        {
            var responseStream2 = await response.Content.ReadAsStringAsync();
            ld.data = responseStream2;// responseStream3;
           // return Content(" basd response" + responseStream2);
        }
        // return Content("Hello, " + HttpContext.Request.Form["Email"] + " " + HttpContext.Request.Form["Datos"] + " x " + HttpContext.Request.Form["idEP"]);
        // return View("Views/LandingPage/index.cshtml", ld);

        return View(ld);
    }
    public IActionResult SignIn()
    {
        var props = new AuthenticationProperties();
        props.RedirectUri = "/LandingPage/SignInSuccess";

        return Challenge(props);
    }
    public IActionResult SignInSuccess()
    {
        return RedirectToAction("Index","SolicitudCredito");
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

    public IActionResult Consultar(int id)
    {
		// Esta es el API - esta pendiente
		var model = new BlogDTO()
		{
			Id = id,
			Titulo = "¿Es posible vender mi Sentencia de Nulidad?",
			ImgBlog = "/assets/images/photos/blogmain2.jpg",
			Autor = "Autor desconocido",
			FechaPublicacion = DateTime.Now,
			ImgAutor = "/assets/images/users/4.jpg",
			Contenido = "Es decir, en una sentencia de nulidad se discute la legalidad o veracidad de un documento, que, en este caso, es un acto administrativo. En consecuencia, el afectado solicita reparación del daño.",
			IntroUno = "Iniciemos por dar claridad al concepto. La nulidad y restablecimiento del derecho se caracteriza porque se ejerce para obtener el reconocimiento de una situación jurídica en particular y la adopción de las medidas adecuadas para su pleno restablecimiento o reparación. Esta acción solo se puede ejercer por quien demuestre un interés, es decir, por quien se considere afectado en su derecho.",
			IntroDos = "Amparado por la ley, el afectado, a través de un instrumento busca desvirtuar la legalidad de un acto administrativo. Como consecuencia, obtiene una indemnización de los perjuicios que el acto haya podido causar durante el tiempo que estuvo vigente. ",
			Comentarios = new List<ComentariosDTO>()
			{
				new ComentariosDTO()
				{
					Id = 1,
					Autor = "Autor desconocido",
					BlogId = 1,
					FechaPublicacion = DateTime.Now,
					ImgAutor = "/assets/images/users/4.jpg",
					Nota = "Este es un comentario",
					SubComentarios = new List<SubComentarioDTO>
					{
						new SubComentarioDTO()
						{
							Id = 1,
							Autor = "Autor desconocido",
							ComentarioId = 1,
							FechaPublicacion= DateTime.Now,
							ImgAutor = "/assets/images/users/4.jpg",
							Nota = "Este es un SubComentario"
						},
						new SubComentarioDTO()
						{
							Id = 2,
							Autor = "Autor desconocido",
							ComentarioId = 1,
							FechaPublicacion= DateTime.Now,
							ImgAutor = "/assets/images/users/4.jpg",
							Nota = "Este es un segundo SubComentario"
						}
					}
				},
				new ComentariosDTO()
				{
					Id = 2,
					Autor = "Autor desconocido",
					BlogId = 1,
					FechaPublicacion = DateTime.Now,
					ImgAutor = "/assets/images/users/4.jpg",
					Nota = "Este es un comentario",
					SubComentarios = new List<SubComentarioDTO>()
				}
			},
		};

		return View(model);
	}
}