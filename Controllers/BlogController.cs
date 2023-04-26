using System.Diagnostics;
using confinancia.Models;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class BlogController : Controller
{

    private readonly ILogger<BlogController> _logger;

    public BlogController(ILogger<BlogController> logger)
    {
        _logger = logger;
    }

    [Route("/blog")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult GetById(int idBlog)
    {
        // Esta es el API - esta pendiente

        var model = new BlogDTO()
        {
            Id = idBlog,
            Titulo = "�Es posible vender mi Sentencia de Nulidad?",
            ImgBlog = "/assets/images/photos/blogmain2.jpg",
            Autor = "Autor desconocido",
            FechaPublicacion = DateTime.Now,
            ImgAutor = "/assets/images/users/4.jpg",
            Contenido = "Es decir, en una sentencia de nulidad se discute la legalidad o veracidad de un documento, que, en este caso, es un acto administrativo. En consecuencia, el afectado solicita reparaci�n del da�o.",
            IntroUno = "Iniciemos por dar claridad al concepto. La nulidad y restablecimiento del derecho se caracteriza porque se ejerce para obtener el reconocimiento de una situaci�n jur�dica en particular y la adopci�n de las medidas adecuadas para su pleno restablecimiento o reparaci�n. Esta acci�n solo se puede ejercer por quien demuestre un inter�s, es decir, por quien se considere afectado en su derecho.",
            IntroDos = "Amparado por la ley, el afectado, a trav�s de un instrumento busca desvirtuar la legalidad de un acto administrativo. Como consecuencia, obtiene una indemnizaci�n de los perjuicios que el acto haya podido causar durante el tiempo que estuvo vigente. ",
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