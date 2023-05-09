using System.Diagnostics;
using confinancia.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace noa.Controllers;

public class LandingPageController : Controller
{

    private readonly ILogger<LandingPageController> _logger;
    public LandingPageController(ILogger<LandingPageController> logger)
    {
        _logger = logger;
    }

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
				ImgBlog = "/assets/images/photos/blogmain2.jpg",
				Autor = new AutorDTO()
				{
					Id= 1,
					Img = "/assets/images/photos/11.jpg",
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
				IntroDos = "Amparado por la ley, el afectado, a través de un instrumento busca desvirtuar la legalidad de un acto administrativo. Como consecuencia, obtiene una indemnización de los perjuicios que el acto haya podido causar durante el tiempo que estuvo vigente. ",
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
							Descripcion = "Esta es una descripción del autor desconocido 2"
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
									Descripcion = "Esta es una descripción del autor desconocido 3"
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
									Descripcion = "Esta es una descripción del autor desconocido 4"
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
							Descripcion = "Esta es una descripción del autor desconocido 5"
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
				Titulo = "¿Es posible vender mi Sentencia de Nulidad?",
				ImgBlog = "/assets/images/photos/blogmain2.jpg",
				Autor = new AutorDTO()
				{
					Id= 1,
					Img = "/assets/images/photos/11.jpg",
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
				IntroDos = "Amparado por la ley, el afectado, a través de un instrumento busca desvirtuar la legalidad de un acto administrativo. Como consecuencia, obtiene una indemnización de los perjuicios que el acto haya podido causar durante el tiempo que estuvo vigente. ",
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
							Descripcion = "Esta es una descripción del autor desconocido 2"
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
									Descripcion = "Esta es una descripción del autor desconocido 3"
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
									Descripcion = "Esta es una descripción del autor desconocido 4"
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
							Descripcion = "Esta es una descripción del autor desconocido 5"
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
				Titulo = "¿Es posible vender mi Sentencia de Nulidad?",
				ImgBlog = "/assets/images/photos/blogmain2.jpg",
				Autor = new AutorDTO()
				{
					Id= 1,
					Img = "/assets/images/photos/11.jpg",
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
				IntroDos = "Amparado por la ley, el afectado, a través de un instrumento busca desvirtuar la legalidad de un acto administrativo. Como consecuencia, obtiene una indemnización de los perjuicios que el acto haya podido causar durante el tiempo que estuvo vigente. ",
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
							Descripcion = "Esta es una descripción del autor desconocido 2"
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
									Descripcion = "Esta es una descripción del autor desconocido 3"
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
									Descripcion = "Esta es una descripción del autor desconocido 4"
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
							Descripcion = "Esta es una descripción del autor desconocido 5"
						},
						BlogId = 1,
						FechaPublicacion = DateTime.Now,
						Nota = "Este es un comentario",
						SubComentarios = new List<SubComentarioDTO>()
					}
				},
			}
		};
        return View(lstBlogsPorCategoria);
    }

	[HttpGet]
	public IActionResult ServicioLanding()
	{
		return View();
	}


}