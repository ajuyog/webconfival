using System.Diagnostics;
using confinancia.Models;
using confinancia.Models.JsonDTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace noa.Controllers;

public class LandingPageController : Controller
{

	#region CONSTRUCTOR
	private readonly IConfiguration _configuration;
	public LandingPageController(IConfiguration configuration)
	{
		_configuration = configuration;
	}
	#endregion

	[Route("/")]
    [HttpGet]
    public IActionResult Index()
    {
		// Pendiente API que me traiga una lst de blogs por categoria //
		var lstBlogsPorCategoria = new List<BlogDTO>()
		{
			new BlogDTO()
			{
				Id = 1,
				Titulo = "¿Es posible vender mi Sentencia de Nulidad?",
				Autor = new AutorDTO()
				{
					Id= 1,
					ImagenAutor = new ImagenesDTO()
					{
						Id = 6,
						URLImagen = "/assets/images/photos/11.jpg",
						Alt = "altPruebas seis",
						Descripcion = "Descripcion prueba seis",
						Titulo = "Titulo de la image seis"
					},
					Nombre = "Autor Desconocido 1",
					Descripcion = "Esta es una descripción del autor desconocido 1"
				} ,
				FechaPublicacion = DateTime.Now,
				Categorias = new List<CategoriaDTO>()
				{
					new CategoriaDTO(){ Id = 1, Nombre = "Economia"},
					new CategoriaDTO(){ Id = 2, Nombre = "Sentencias"},
					new CategoriaDTO(){ Id = 3, Nombre = "Indicadores economicos"},
					new CategoriaDTO(){ Id = 4, Nombre = "Juridicos"}
				},
				Contenido = "Es decir, en una sentencia de nulidad se discute la legalidad o veracidad de un documento, que, en este caso, es un acto administrativo. En consecuencia, el afectado solicita reparación del daño.",
				IntroUno = "Iniciemos por dar claridad al concepto. La nulidad y restablecimiento del derecho se caracteriza porque se ejerce para obtener el reconocimiento de una situación jurídica en particular y la adopción de las medidas adecuadas para su pleno restablecimiento o reparación. Esta acción solo se puede ejercer por quien demuestre un interés, es decir, por quien se considere afectado en su derecho.",
				Comentarios = new List<ComentariosDTO>()
				{
					new ComentariosDTO()
					{
						Id = 1,
						Autor = new AutorDTO()
						{
							Id= 2,
							ImagenAutor = new ImagenesDTO()
							{
								Id = 7,
								URLImagen = "/assets/images/photos/11.jpg",
								Alt = "altPruebas siete",
								Descripcion = "Descripcion prueba siete",
								Titulo = "Titulo de la image siete"
							},
							Nombre = "Autor Desconocido 2",
							Descripcion = "Esta es una descripción del autor desconocido 2"
						},
						FechaPublicacion = DateTime.Now,
						Comentario = "Este es un comentario",
						SubComentarios = new List<SubComentarioDTO>
						{
							new SubComentarioDTO()
							{
								Id = 1,
								SubComentario = 20,
								Autor = new AutorDTO()
								{
									Id= 3,
									ImagenAutor = new ImagenesDTO()
									{
										Id = 7,
										URLImagen = "/assets/images/photos/11.jpg",
										Alt = "altPruebas siete",
										Descripcion = "Descripcion prueba siete",
										Titulo = "Titulo de la image siete"
									},
									Nombre = "Autor Desconocido 2",
									Descripcion = "Esta es una descripción del autor desconocido 3"
								},
								ComentarioId = 1,
								FechaPublicacion= DateTime.Now,
								Contenido = "Este es un SubComentario"
							},
							new SubComentarioDTO()
							{
								Id = 2,
								SubComentario = 21,
								Autor = new AutorDTO()
								{
									Id= 4,
									ImagenAutor = new ImagenesDTO()
									{
										Id = 8,
										URLImagen = "/assets/images/photos/11.jpg",
										Alt = "altPruebas ocho",
										Descripcion = "Descripcion prueba ocho",
										Titulo = "Titulo de la image ocho"
									},
									Nombre = "Autor Desconocido 4",
									Descripcion = "Esta es una descripción del autor desconocido 4"
								},
								ComentarioId = 1,
								FechaPublicacion= DateTime.Now,
								Contenido = "Este es un segundo SubComentario"
							}
						}
					},
					new ComentariosDTO()
					{
						Id = 2,
						Autor = new AutorDTO()
						{
							Id= 5,
							ImagenAutor = new ImagenesDTO()
							{
								Id = 9,
								URLImagen = "/assets/images/photos/11.jpg",
								Alt = "altPruebas nueve",
								Descripcion = "Descripcion prueba nueve",
								Titulo = "Titulo de la image nueve"
							},
							Nombre = "Autor Desconocido 5",
							Descripcion = "Esta es una descripción del autor desconocido 5"
						},
						FechaPublicacion = DateTime.Now,
						Comentario = "Este es un comentario",
						SubComentarios = new List<SubComentarioDTO>()
					}
				},
				Galeria = new List<ImagenesDTO>()
				{
					new ImagenesDTO()
					{
						Id = 1,
						URLImagen = "/assets/images/photos/blogmain2.jpg",
					},
					new ImagenesDTO()
					{
						Id = 2,
						URLImagen = "/assets/images/photos/blogmain2.jpg",
					}
				}
			},
			new BlogDTO()
			{
				Id = 2,
				Titulo = "¿Es posible vender mi Sentencia de Nulidad?",
				Autor = new AutorDTO()
				{
					Id= 1,
					ImagenAutor = new ImagenesDTO()
					{
						Id = 10,
						URLImagen = "/assets/images/photos/11.jpg",
						Alt = "altPruebas diez",
						Descripcion = "Descripcion prueba diez",
						Titulo = "Titulo de la image diez"
					},
					Nombre = "Autor Desconocido 1",
					Descripcion = "Esta es una descripción del autor desconocido 1"
				} ,
				FechaPublicacion = DateTime.Now,
				Categorias = new List<CategoriaDTO>()
				{
					new CategoriaDTO(){ Id = 1, Nombre = "Economia"},
					new CategoriaDTO(){ Id = 2, Nombre = "Sentencias"},
					new CategoriaDTO(){ Id = 3, Nombre = "Indicadores economicos"},
					new CategoriaDTO(){ Id = 4, Nombre = "Juridicos"}
				},
				Contenido = "Es decir, en una sentencia de nulidad se discute la legalidad o veracidad de un documento, que, en este caso, es un acto administrativo. En consecuencia, el afectado solicita reparación del daño.",
				IntroUno = "Iniciemos por dar claridad al concepto. La nulidad y restablecimiento del derecho se caracteriza porque se ejerce para obtener el reconocimiento de una situación jurídica en particular y la adopción de las medidas adecuadas para su pleno restablecimiento o reparación. Esta acción solo se puede ejercer por quien demuestre un interés, es decir, por quien se considere afectado en su derecho.",
				Comentarios = new List<ComentariosDTO>()
				{
					new ComentariosDTO()
					{
						Id = 1,
						Autor = new AutorDTO()
						{
							Id= 2,
							ImagenAutor = new ImagenesDTO()
							{
								Id = 11,
								URLImagen = "/assets/images/photos/11.jpg",
								Alt = "altPruebas once",
								Descripcion = "Descripcion prueba once",
								Titulo = "Titulo de la image once"
							},
							Nombre = "Autor Desconocido 2",
							Descripcion = "Esta es una descripción del autor desconocido 2"
						},
						FechaPublicacion = DateTime.Now,
						Comentario = "Este es un comentario",
						SubComentarios = new List<SubComentarioDTO>
						{
							new SubComentarioDTO()
							{
								Id = 1,
								SubComentario = 20,
								Autor = new AutorDTO()
								{
									Id= 3,
									ImagenAutor = new ImagenesDTO()
									{
										Id = 12,
										URLImagen = "/assets/images/photos/11.jpg",
										Alt = "altPruebas doce",
										Descripcion = "Descripcion prueba doce",
										Titulo = "Titulo de la image doce"
									},
									Nombre = "Autor Desconocido 2",
									Descripcion = "Esta es una descripción del autor desconocido 3"
								},
								ComentarioId = 1,
								FechaPublicacion= DateTime.Now,
								Contenido = "Este es un SubComentario"
							},
							new SubComentarioDTO()
							{
								Id = 2,
								SubComentario = 21,
								Autor = new AutorDTO()
								{
									Id= 4,
									ImagenAutor = new ImagenesDTO()
									{
										Id = 12,
										URLImagen = "/assets/images/photos/11.jpg",
										Alt = "altPruebas doce",
										Descripcion = "Descripcion prueba doce",
										Titulo = "Titulo de la image doce"
									},
									Nombre = "Autor Desconocido 4",
									Descripcion = "Esta es una descripción del autor desconocido 4"
								},
								ComentarioId = 1,
								FechaPublicacion= DateTime.Now,
								Contenido = "Este es un segundo SubComentario"
							}
						}
					},
					new ComentariosDTO()
					{
						Id = 2,
						Autor = new AutorDTO()
						{
							Id= 5,
							ImagenAutor = new ImagenesDTO()
							{
								Id = 13,
								URLImagen = "/assets/images/photos/11.jpg",
								Alt = "altPruebas trece",
								Descripcion = "Descripcion prueba trece",
								Titulo = "Titulo de la image trece"
							},
							Nombre = "Autor Desconocido 5",
							Descripcion = "Esta es una descripción del autor desconocido 5"
						},
						FechaPublicacion = DateTime.Now,
						Comentario = "Este es un comentario",
						SubComentarios = new List<SubComentarioDTO>()
					}
				},
				Galeria = new List<ImagenesDTO>()
				{
					new ImagenesDTO()
					{
						Id = 1,
						URLImagen = "/assets/images/photos/blogmain2.jpg",
					},
					new ImagenesDTO()
					{
						Id = 2,
						URLImagen = "/assets/images/photos/blogmain2.jpg",
					}
				}
			},
			new BlogDTO()
			{
				Id = 3,
				Titulo = "¿Es posible vender mi Sentencia de Nulidad?",
				Autor = new AutorDTO()
				{
					Id= 1,
					ImagenAutor = new ImagenesDTO()
					{
						Id = 14,
						URLImagen = "/assets/images/photos/11.jpg",
						Alt = "altPruebas catorce",
						Descripcion = "Descripcion prueba catorce",
						Titulo = "Titulo de la image catorce"
					},
					Nombre = "Autor Desconocido 1",
					Descripcion = "Esta es una descripción del autor desconocido 1"
				} ,
				FechaPublicacion = DateTime.Now,
				Categorias = new List<CategoriaDTO>()
				{
					new CategoriaDTO(){ Id = 1, Nombre = "Economia"},
					new CategoriaDTO(){ Id = 2, Nombre = "Sentencias"},
					new CategoriaDTO(){ Id = 3, Nombre = "Indicadores economicos"},
					new CategoriaDTO(){ Id = 4, Nombre = "Juridicos"}
				},
				Contenido = "Es decir, en una sentencia de nulidad se discute la legalidad o veracidad de un documento, que, en este caso, es un acto administrativo. En consecuencia, el afectado solicita reparación del daño.",
				IntroUno = "Iniciemos por dar claridad al concepto. La nulidad y restablecimiento del derecho se caracteriza porque se ejerce para obtener el reconocimiento de una situación jurídica en particular y la adopción de las medidas adecuadas para su pleno restablecimiento o reparación. Esta acción solo se puede ejercer por quien demuestre un interés, es decir, por quien se considere afectado en su derecho.",
				Comentarios = new List<ComentariosDTO>()
				{
					new ComentariosDTO()
					{
						Id = 1,
						Autor = new AutorDTO()
						{
							Id= 2,
							ImagenAutor = new ImagenesDTO()
							{
								Id = 15,
								URLImagen = "/assets/images/photos/11.jpg",
								Alt = "altPruebas quince",
								Descripcion = "Descripcion prueba quince",
								Titulo = "Titulo de la image quince"
							},
							Nombre = "Autor Desconocido 2",
							Descripcion = "Esta es una descripción del autor desconocido 2"
						},
						FechaPublicacion = DateTime.Now,
						Comentario = "Este es un comentario",
						SubComentarios = new List<SubComentarioDTO>
						{
							new SubComentarioDTO()
							{
								Id = 1,
								SubComentario = 20,
								Autor = new AutorDTO()
								{
									Id= 3,
									ImagenAutor = new ImagenesDTO()
									{
										Id = 16,
										URLImagen = "/assets/images/photos/11.jpg",
										Alt = "altPruebas dieciseis",
										Descripcion = "Descripcion prueba dieciseis",
										Titulo = "Titulo de la image dieciseis"
									},
									Nombre = "Autor Desconocido 2",
									Descripcion = "Esta es una descripción del autor desconocido 3"
								},
								ComentarioId = 1,
								FechaPublicacion= DateTime.Now,
								Contenido = "Este es un SubComentario"
							},
							new SubComentarioDTO()
							{
								Id = 2,
								SubComentario = 21,
								Autor = new AutorDTO()
								{
									Id= 4,
									ImagenAutor = new ImagenesDTO()
									{
										Id = 16,
										URLImagen = "/assets/images/photos/11.jpg",
										Alt = "altPruebas dieciseis",
										Descripcion = "Descripcion prueba dieciseis",
										Titulo = "Titulo de la image dieciseis"
									},
									Nombre = "Autor Desconocido 4",
									Descripcion = "Esta es una descripción del autor desconocido 4"
								},
								ComentarioId = 1,
								FechaPublicacion= DateTime.Now,
								Contenido = "Este es un segundo SubComentario"
							}
						}
					},
					new ComentariosDTO()
					{
						Id = 2,
						Autor = new AutorDTO()
						{
							Id= 5,
							ImagenAutor = new ImagenesDTO()
							{
								Id = 16,
								URLImagen = "/assets/images/photos/11.jpg",
								Alt = "altPruebas dieciseis",
								Descripcion = "Descripcion prueba dieciseis",
								Titulo = "Titulo de la image dieciseis"
							},
							Nombre = "Autor Desconocido 5",
							Descripcion = "Esta es una descripción del autor desconocido 5"
						},
						FechaPublicacion = DateTime.Now,
						Comentario = "Este es un comentario",
						SubComentarios = new List<SubComentarioDTO>()
					}
				},
				Galeria = new List<ImagenesDTO>()
				{
					new ImagenesDTO()
					{
						Id = 1,
						URLImagen = "/assets/images/photos/blogmain2.jpg",
					},
					new ImagenesDTO()
					{
						Id = 2,
						URLImagen = "/assets/images/photos/blogmain2.jpg",
					}
				}
			}
		};
        return View(lstBlogsPorCategoria);
    }

	[HttpGet]
	public IActionResult ServicioLanding(string seccion)
	{
		ViewBag.Seccion = seccion;
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
				Expires = DateTime.Now.AddDays(1)
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