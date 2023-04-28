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
		// Pendiente API que me traiga una lst de blogs por categoria //
		var lstBlogsPorCategoria = new List<BlogDTO>()
		{
			new BlogDTO()
			{
				Id = 1,
				Titulo = "�Es posible vender mi Sentencia de Nulidad?",
				ImgBlog = "/assets/images/photos/blogmain2.jpg",
				Autor = new AutorDTO()
				{
					Id= 1,
					Img = "/assets/images/photos/11.jpg",
					Nombre = "Autor Desconocido 1",
					Descripcion = "Esta es una descripci�n del autor desconocido 1"
				} ,
				FechaPublicacion = DateTime.Now,
				Categorias = new List<CategoriaDTO>()
				{
					new CategoriaDTO(){ Id = 1, Nombre = "Economia"},
					new CategoriaDTO(){ Id = 2, Nombre = "Sentencias"},
					new CategoriaDTO(){ Id = 3, Nombre = "Indicadores economicos"},
					new CategoriaDTO(){ Id = 4, Nombre = "Juridicos"}
				},
				Contenido = "Es decir, en una sentencia de nulidad se discute la legalidad o veracidad de un documento, que, en este caso, es un acto administrativo. En consecuencia, el afectado solicita reparaci�n del da�o.",
				IntroUno = "Iniciemos por dar claridad al concepto. La nulidad y restablecimiento del derecho se caracteriza porque se ejerce para obtener el reconocimiento de una situaci�n jur�dica en particular y la adopci�n de las medidas adecuadas para su pleno restablecimiento o reparaci�n. Esta acci�n solo se puede ejercer por quien demuestre un inter�s, es decir, por quien se considere afectado en su derecho.",
				IntroDos = "Amparado por la ley, el afectado, a trav�s de un instrumento busca desvirtuar la legalidad de un acto administrativo. Como consecuencia, obtiene una indemnizaci�n de los perjuicios que el acto haya podido causar durante el tiempo que estuvo vigente. ",
				Comentarios = new List<ComentariosDTO>()
				{
					new ComentariosDTO()
					{
						Id = 1,
						Autor = new AutorDTO()
						{
							Id= 2,
							Img = "/assets/images/photos/11.jpg",
							Nombre = "Autor Desconocido 2",
							Descripcion = "Esta es una descripci�n del autor desconocido 2"
						},
						BlogId = 1,
						FechaPublicacion = DateTime.Now,
						Nota = "Este es un comentario",
						SubComentarios = new List<SubComentarioDTO>
						{
							new SubComentarioDTO()
							{
								Id = 1,
								SubComentario = 20,
								Autor = new AutorDTO()
								{
									Id= 3,
									Img = "/assets/images/photos/11.jpg",
									Nombre = "Autor Desconocido 2",
									Descripcion = "Esta es una descripci�n del autor desconocido 3"
								},
								ComentarioId = 1,
								FechaPublicacion= DateTime.Now,
								Nota = "Este es un SubComentario"
							},
							new SubComentarioDTO()
							{
								Id = 2,
								SubComentario = 21,
								Autor = new AutorDTO()
								{
									Id= 4,
									Img = "/assets/images/photos/11.jpg",
									Nombre = "Autor Desconocido 4",
									Descripcion = "Esta es una descripci�n del autor desconocido 4"
								},
								ComentarioId = 1,
								FechaPublicacion= DateTime.Now,
								Nota = "Este es un segundo SubComentario"
							}
						}
					},
					new ComentariosDTO()
					{
						Id = 2,
						Autor = new AutorDTO()
						{
							Id= 5,
							Img = "/assets/images/photos/11.jpg",
							Nombre = "Autor Desconocido 5",
							Descripcion = "Esta es una descripci�n del autor desconocido 5"
						},
						BlogId = 1,
						FechaPublicacion = DateTime.Now,
						Nota = "Este es un comentario",
						SubComentarios = new List<SubComentarioDTO>()
					}
				},
			},
			new BlogDTO()
			{
				Id = 2,
				Titulo = "�Es posible vender mi Sentencia de Nulidad?",
				ImgBlog = "/assets/images/photos/blogmain2.jpg",
				Autor = new AutorDTO()
				{
					Id= 1,
					Img = "/assets/images/photos/11.jpg",
					Nombre = "Autor Desconocido 1",
					Descripcion = "Esta es una descripci�n del autor desconocido 1"
				} ,
				FechaPublicacion = DateTime.Now,
				Categorias = new List<CategoriaDTO>()
				{
					new CategoriaDTO(){ Id = 1, Nombre = "Economia"},
					new CategoriaDTO(){ Id = 2, Nombre = "Sentencias"},
					new CategoriaDTO(){ Id = 3, Nombre = "Indicadores economicos"},
					new CategoriaDTO(){ Id = 4, Nombre = "Juridicos"}
				},
				Contenido = "Es decir, en una sentencia de nulidad se discute la legalidad o veracidad de un documento, que, en este caso, es un acto administrativo. En consecuencia, el afectado solicita reparaci�n del da�o.",
				IntroUno = "Iniciemos por dar claridad al concepto. La nulidad y restablecimiento del derecho se caracteriza porque se ejerce para obtener el reconocimiento de una situaci�n jur�dica en particular y la adopci�n de las medidas adecuadas para su pleno restablecimiento o reparaci�n. Esta acci�n solo se puede ejercer por quien demuestre un inter�s, es decir, por quien se considere afectado en su derecho.",
				IntroDos = "Amparado por la ley, el afectado, a trav�s de un instrumento busca desvirtuar la legalidad de un acto administrativo. Como consecuencia, obtiene una indemnizaci�n de los perjuicios que el acto haya podido causar durante el tiempo que estuvo vigente. ",
				Comentarios = new List<ComentariosDTO>()
				{
					new ComentariosDTO()
					{
						Id = 1,
						Autor = new AutorDTO()
						{
							Id= 2,
							Img = "/assets/images/photos/11.jpg",
							Nombre = "Autor Desconocido 2",
							Descripcion = "Esta es una descripci�n del autor desconocido 2"
						},
						BlogId = 1,
						FechaPublicacion = DateTime.Now,
						Nota = "Este es un comentario",
						SubComentarios = new List<SubComentarioDTO>
						{
							new SubComentarioDTO()
							{
								Id = 1,
								SubComentario = 20,
								Autor = new AutorDTO()
								{
									Id= 3,
									Img = "/assets/images/photos/11.jpg",
									Nombre = "Autor Desconocido 2",
									Descripcion = "Esta es una descripci�n del autor desconocido 3"
								},
								ComentarioId = 1,
								FechaPublicacion= DateTime.Now,
								Nota = "Este es un SubComentario"
							},
							new SubComentarioDTO()
							{
								Id = 2,
								SubComentario = 21,
								Autor = new AutorDTO()
								{
									Id= 4,
									Img = "/assets/images/photos/11.jpg",
									Nombre = "Autor Desconocido 4",
									Descripcion = "Esta es una descripci�n del autor desconocido 4"
								},
								ComentarioId = 1,
								FechaPublicacion= DateTime.Now,
								Nota = "Este es un segundo SubComentario"
							}
						}
					},
					new ComentariosDTO()
					{
						Id = 2,
						Autor = new AutorDTO()
						{
							Id= 5,
							Img = "/assets/images/photos/11.jpg",
							Nombre = "Autor Desconocido 5",
							Descripcion = "Esta es una descripci�n del autor desconocido 5"
						},
						BlogId = 1,
						FechaPublicacion = DateTime.Now,
						Nota = "Este es un comentario",
						SubComentarios = new List<SubComentarioDTO>()
					}
				},
			},
			new BlogDTO()
			{
				Id = 3,
				Titulo = "�Es posible vender mi Sentencia de Nulidad?",
				ImgBlog = "/assets/images/photos/blogmain2.jpg",
				Autor = new AutorDTO()
				{
					Id= 1,
					Img = "/assets/images/photos/11.jpg",
					Nombre = "Autor Desconocido 1",
					Descripcion = "Esta es una descripci�n del autor desconocido 1"
				} ,
				FechaPublicacion = DateTime.Now,
				Categorias = new List<CategoriaDTO>()
				{
					new CategoriaDTO(){ Id = 1, Nombre = "Economia"},
					new CategoriaDTO(){ Id = 2, Nombre = "Sentencias"},
					new CategoriaDTO(){ Id = 3, Nombre = "Indicadores economicos"},
					new CategoriaDTO(){ Id = 4, Nombre = "Juridicos"}
				},
				Contenido = "Es decir, en una sentencia de nulidad se discute la legalidad o veracidad de un documento, que, en este caso, es un acto administrativo. En consecuencia, el afectado solicita reparaci�n del da�o.",
				IntroUno = "Iniciemos por dar claridad al concepto. La nulidad y restablecimiento del derecho se caracteriza porque se ejerce para obtener el reconocimiento de una situaci�n jur�dica en particular y la adopci�n de las medidas adecuadas para su pleno restablecimiento o reparaci�n. Esta acci�n solo se puede ejercer por quien demuestre un inter�s, es decir, por quien se considere afectado en su derecho.",
				IntroDos = "Amparado por la ley, el afectado, a trav�s de un instrumento busca desvirtuar la legalidad de un acto administrativo. Como consecuencia, obtiene una indemnizaci�n de los perjuicios que el acto haya podido causar durante el tiempo que estuvo vigente. ",
				Comentarios = new List<ComentariosDTO>()
				{
					new ComentariosDTO()
					{
						Id = 1,
						Autor = new AutorDTO()
						{
							Id= 2,
							Img = "/assets/images/photos/11.jpg",
							Nombre = "Autor Desconocido 2",
							Descripcion = "Esta es una descripci�n del autor desconocido 2"
						},
						BlogId = 1,
						FechaPublicacion = DateTime.Now,
						Nota = "Este es un comentario",
						SubComentarios = new List<SubComentarioDTO>
						{
							new SubComentarioDTO()
							{
								Id = 1,
								SubComentario = 20,
								Autor = new AutorDTO()
								{
									Id= 3,
									Img = "/assets/images/photos/11.jpg",
									Nombre = "Autor Desconocido 2",
									Descripcion = "Esta es una descripci�n del autor desconocido 3"
								},
								ComentarioId = 1,
								FechaPublicacion= DateTime.Now,
								Nota = "Este es un SubComentario"
							},
							new SubComentarioDTO()
							{
								Id = 2,
								SubComentario = 21,
								Autor = new AutorDTO()
								{
									Id= 4,
									Img = "/assets/images/photos/11.jpg",
									Nombre = "Autor Desconocido 4",
									Descripcion = "Esta es una descripci�n del autor desconocido 4"
								},
								ComentarioId = 1,
								FechaPublicacion= DateTime.Now,
								Nota = "Este es un segundo SubComentario"
							}
						}
					},
					new ComentariosDTO()
					{
						Id = 2,
						Autor = new AutorDTO()
						{
							Id= 5,
							Img = "/assets/images/photos/11.jpg",
							Nombre = "Autor Desconocido 5",
							Descripcion = "Esta es una descripci�n del autor desconocido 5"
						},
						BlogId = 1,
						FechaPublicacion = DateTime.Now,
						Nota = "Este es un comentario",
						SubComentarios = new List<SubComentarioDTO>()
					}
				},
			}
		};

		//var httpClient = new HttpClient();
  //      var json = await httpClient.GetStringAsync("https://apilead.azurewebsites.net/api/entidadpagaduria");

  //      List<EntidadPagaduria> allEP = JsonConvert.DeserializeObject<List<EntidadPagaduria>>(json);
  //      json = await httpClient.GetStringAsync("https://apilead.azurewebsites.net/api/tipoNombramiento");
  //      List<TipoNombramiento> allTN = JsonConvert.DeserializeObject<List<TipoNombramiento>>(json);

  //      ld.EP = allEP;
  //      ld.TN = allTN;
  //      ld.data = "";
        return View(lstBlogsPorCategoria);
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
                ld.data = "Gracias por contactarte, un asesor comercial se estar� comunicando contigo";// responseStream3;
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

}