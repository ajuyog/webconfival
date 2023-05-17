using confinancia.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Reflection;

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
            Titulo = "¿Es posible vender mi Sentencia de Nulidad?",
            Autor = new AutorDTO()
            {
                Id= 1,
                ImagenAutor = new ImagenesDTO()
				{ 
					Id = 1,
					URLImagen = "/assets/images/photos/11.jpg",
					Alt = "altPruebas",
					Descripcion = "Descripcion prueba",
					Titulo = "Titulo de la image"
				},
                Nombre = "Autor Desconocido 1",
                Descripcion = "Esta es una descripción del autor desconocido 1"
            } ,
            FechaPublicacion = DateTime.Now,
            Categorias = new List<CategoriaDTO>()
            {
                new CategoriaDTO(){ Id = 1, Nombre = "Sentencias"},
				new CategoriaDTO(){ Id = 2, Nombre = "Juridico"},
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
							Id = 2,
							URLImagen = "/assets/images/photos/11.jpg",
							Alt = "altPruebas dos",
							Descripcion = "Descripcion prueba dos",
							Titulo = "Titulo de la image dos"
						},
						Nombre = "Autor Desconocido 2",
						Descripcion = "Esta es una descripción del autor desconocido 2"
					},
                    FechaPublicacion = DateTime.Now,
                    Contenido = "Este es un comentario",
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
									Id = 3,
									URLImagen = "/assets/images/photos/11.jpg",
									Alt = "altPruebas tres",
									Descripcion = "Descripcion prueba tres",
									Titulo = "Titulo de la image tres"
								},
								Nombre = "Autor Desconocido 3",
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
									Id = 4,
									URLImagen = "/assets/images/photos/11.jpg",
									Alt = "altPruebas cuatro",
									Descripcion = "Descripcion prueba cuatro",
									Titulo = "Titulo de la image cuatro"
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
							Id = 5,
							URLImagen = "/assets/images/photos/11.jpg",
							Alt = "altPruebas cinco",
							Descripcion = "Descripcion prueba cinco",
							Titulo = "Titulo de la image cinco"
						},
						Nombre = "Autor Desconocido 5",
						Descripcion = "Esta es una descripción del autor desconocido 5"
					},
                    FechaPublicacion = DateTime.Now,
                    Contenido = "Este es un comentario",
                    SubComentarios = new List<SubComentarioDTO>()
				}
			},
			Galeria = new List<ImagenesDTO>()
			{ 
				new ImagenesDTO()
				{
					Id = 1,
					Alt = "Alt 1",
					Descripcion = "Descripcion Imagen 1",
					Titulo = "Titulo imagen 1",
					URLImagen = "/assets/images/photos/blogmain2.jpg",
					URLSoporte = null
				},
				new ImagenesDTO()
				{
					Id = 2,
					Alt = "Alt 2",
					Descripcion = "Descripcion Imagen 2",
					Titulo = "Titulo imagen 2",
					URLImagen = "/assets/images/photos/blogmain2.jpg",
					URLSoporte = "www.eltiempo.com"
				},
				new ImagenesDTO()
				{
					Id = 3,
					Alt = "Alt 3",
					Descripcion = "Descripcion Imagen 3",
					Titulo = "Titulo imagen 3",
					URLImagen = "/assets/images/photos/blogmain2.jpg",
					URLSoporte = "www.eltiempo.com"
				}
			}
		};
		return View(model);
	}

    [HttpGet]
    public async Task<BlogsCategoriaDTO> ConsultarPorCategoria(int id)
    {
		var parametro = id.ToString();
		var client = new HttpClient();
		var request = new HttpRequestMessage(HttpMethod.Get, "https://apileadconfival.azurewebsites.net/api/categoria/" + parametro);
		request.Headers.Add("XApiKey", "H^qP[7p#$18EXbV(lIP5xu+tCe-kgCM&{i_V,=(&");
		var content = new StringContent(string.Empty);
		content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
		request.Content = content;
		var response = await client.SendAsync(request);
		if (response.IsSuccessStatusCode)
		{
			var responseStream = await response.Content.ReadAsStringAsync();
			var lstBlogs = JsonConvert.DeserializeObject<BlogsCategoriaDTO>(responseStream);
			lstBlogs.Blogs = lstBlogs.Blogs.Take(3).ToList();
			return lstBlogs;
		}
		else
		{
			var x = new BlogsCategoriaDTO();
			return x;
		}
		//var lstBlogsPorCategoria = new List<BlogDTO>()
		//{
		//	new BlogDTO()
		//	{
		//		Id = 1,
		//		Titulo = "¿Es posible vender mi Sentencia de Nulidad?",
		//		ImgBlog = "/assets/images/photos/blogmain2.jpg",
		//		Autor = new AutorDTO()
		//		{
		//			Id= 1,
		//			ImagenAutor = new ImagenesDTO()
		//			{
		//				Id = 6,
		//				URLImagen = "/assets/images/photos/11.jpg",
		//				Alt = "altPruebas seis",
		//				Descripcion = "Descripcion prueba seis",
		//				Titulo = "Titulo de la image seis"
		//			},
		//			Nombre = "Autor Desconocido 1",
		//			Descripcion = "Esta es una descripción del autor desconocido 1"
		//		} ,
		//		FechaPublicacion = DateTime.Now,
		//		Categorias = new List<CategoriaDTO>()
		//		{
		//			new CategoriaDTO(){ Id = 1, Nombre = "Economia"},
		//			new CategoriaDTO(){ Id = 2, Nombre = "Sentencias"},
		//			new CategoriaDTO(){ Id = 3, Nombre = "Indicadores economicos"},
		//			new CategoriaDTO(){ Id = 4, Nombre = "Juridicos"}
		//		},
		//		Contenido = "Es decir, en una sentencia de nulidad se discute la legalidad o veracidad de un documento, que, en este caso, es un acto administrativo. En consecuencia, el afectado solicita reparación del daño.",
		//		IntroUno = "Iniciemos por dar claridad al concepto. La nulidad y restablecimiento del derecho se caracteriza porque se ejerce para obtener el reconocimiento de una situación jurídica en particular y la adopción de las medidas adecuadas para su pleno restablecimiento o reparación. Esta acción solo se puede ejercer por quien demuestre un interés, es decir, por quien se considere afectado en su derecho.",
		//		Comentarios = new List<ComentariosDTO>()
		//		{
		//			new ComentariosDTO()
		//			{
		//				Id = 1,
		//				Autor = new AutorDTO()
		//				{
		//					Id= 2,
		//					ImagenAutor = new ImagenesDTO()
		//					{
		//						Id = 7,
		//						URLImagen = "/assets/images/photos/11.jpg",
		//						Alt = "altPruebas siete",
		//						Descripcion = "Descripcion prueba siete",
		//						Titulo = "Titulo de la image siete"
		//					},
		//					Nombre = "Autor Desconocido 2",
		//					Descripcion = "Esta es una descripción del autor desconocido 2"
		//				},
		//				FechaPublicacion = DateTime.Now,
		//				Contenido = "Este es un comentario",
		//				SubComentarios = new List<SubComentarioDTO>
		//				{
		//					new SubComentarioDTO()
		//					{
		//						Id = 1,
		//						SubComentario = 20,
		//						Autor = new AutorDTO()
		//						{
		//							Id= 3,
		//							ImagenAutor = new ImagenesDTO()
		//							{
		//								Id = 7,
		//								URLImagen = "/assets/images/photos/11.jpg",
		//								Alt = "altPruebas siete",
		//								Descripcion = "Descripcion prueba siete",
		//								Titulo = "Titulo de la image siete"
		//							},
		//							Nombre = "Autor Desconocido 2",
		//							Descripcion = "Esta es una descripción del autor desconocido 3"
		//						},
		//						ComentarioId = 1,
		//						FechaPublicacion= DateTime.Now,
		//						Contenido = "Este es un SubComentario"
		//					},
		//					new SubComentarioDTO()
		//					{
		//						Id = 2,
		//						SubComentario = 21,
		//						Autor = new AutorDTO()
		//						{
		//							Id= 4,
		//							ImagenAutor = new ImagenesDTO()
		//							{
		//								Id = 8,
		//								URLImagen = "/assets/images/photos/11.jpg",
		//								Alt = "altPruebas ocho",
		//								Descripcion = "Descripcion prueba ocho",
		//								Titulo = "Titulo de la image ocho"
		//							},
		//							Nombre = "Autor Desconocido 4",
		//							Descripcion = "Esta es una descripción del autor desconocido 4"
		//						},
		//						ComentarioId = 1,
		//						FechaPublicacion= DateTime.Now,
		//						Contenido = "Este es un segundo SubComentario"
		//					}
		//				}
		//			},
		//			new ComentariosDTO()
		//			{
		//				Id = 2,
		//				Autor = new AutorDTO()
		//				{
		//					Id= 5,
		//					ImagenAutor = new ImagenesDTO()
		//					{
		//						Id = 9,
		//						URLImagen = "/assets/images/photos/11.jpg",
		//						Alt = "altPruebas nueve",
		//						Descripcion = "Descripcion prueba nueve",
		//						Titulo = "Titulo de la image nueve"
		//					},
		//					Nombre = "Autor Desconocido 5",
		//					Descripcion = "Esta es una descripción del autor desconocido 5"
		//				},
		//				FechaPublicacion = DateTime.Now,
		//				Contenido = "Este es un comentario",
		//				SubComentarios = new List<SubComentarioDTO>()
		//			}
		//		},
		//		Galeria = new List<ImagenesDTO>()
		//		{
		//			new ImagenesDTO()
		//			{
		//				Id = 1,
		//				Alt = "Alt 1",
		//				Descripcion = "Descripcion Imagen 1",
		//				Titulo = "Titulo imagen 1",
		//				URLImagen = "/assets/images/photos/blogmain2.jpg",
		//				URLSoporte = null
		//			},
		//			new ImagenesDTO()
		//			{
		//				Id = 2,
		//				Alt = "Alt 2",
		//				Descripcion = "Descripcion Imagen 2",
		//				Titulo = "Titulo imagen 2",
		//				URLImagen = "/assets/images/photos/blogmain2.jpg",
		//				URLSoporte = "www.eltiempo.com"
		//			},
		//			new ImagenesDTO()
		//			{
		//				Id = 3,
		//				Alt = "Alt 3",
		//				Descripcion = "Descripcion Imagen 3",
		//				Titulo = "Titulo imagen 3",
		//				URLImagen = "/assets/images/photos/blogmain2.jpg",
		//				URLSoporte = "www.eltiempo.com"
		//			}
		//		}
		//	},
		//	new BlogDTO()
		//	{
		//		Id = 2,
		//		Titulo = "¿Es posible vender mi Sentencia de Nulidad?",
		//		ImgBlog = "/assets/images/photos/blogmain2.jpg",
		//		Autor = new AutorDTO()
		//		{
		//			Id= 1,
		//			ImagenAutor = new ImagenesDTO()
		//			{
		//				Id = 10,
		//				URLImagen = "/assets/images/photos/11.jpg",
		//				Alt = "altPruebas diez",
		//				Descripcion = "Descripcion prueba diez",
		//				Titulo = "Titulo de la image diez"
		//			},
		//			Nombre = "Autor Desconocido 1",
		//			Descripcion = "Esta es una descripción del autor desconocido 1"
		//		} ,
		//		FechaPublicacion = DateTime.Now,
		//		Categorias = new List<CategoriaDTO>()
		//		{
		//			new CategoriaDTO(){ Id = 1, Nombre = "Economia"},
		//			new CategoriaDTO(){ Id = 2, Nombre = "Sentencias"},
		//			new CategoriaDTO(){ Id = 3, Nombre = "Indicadores economicos"},
		//			new CategoriaDTO(){ Id = 4, Nombre = "Juridicos"}
		//		},
		//		Contenido = "Es decir, en una sentencia de nulidad se discute la legalidad o veracidad de un documento, que, en este caso, es un acto administrativo. En consecuencia, el afectado solicita reparación del daño.",
		//		IntroUno = "Iniciemos por dar claridad al concepto. La nulidad y restablecimiento del derecho se caracteriza porque se ejerce para obtener el reconocimiento de una situación jurídica en particular y la adopción de las medidas adecuadas para su pleno restablecimiento o reparación. Esta acción solo se puede ejercer por quien demuestre un interés, es decir, por quien se considere afectado en su derecho.",
		//		Comentarios = new List<ComentariosDTO>()
		//		{
		//			new ComentariosDTO()
		//			{
		//				Id = 1,
		//				Autor = new AutorDTO()
		//				{
		//					Id= 2,
		//					ImagenAutor = new ImagenesDTO()
		//					{
		//						Id = 11,
		//						URLImagen = "/assets/images/photos/11.jpg",
		//						Alt = "altPruebas once",
		//						Descripcion = "Descripcion prueba once",
		//						Titulo = "Titulo de la image once"
		//					},
		//					Nombre = "Autor Desconocido 2",
		//					Descripcion = "Esta es una descripción del autor desconocido 2"
		//				},
		//				FechaPublicacion = DateTime.Now,
		//				Contenido = "Este es un comentario",
		//				SubComentarios = new List<SubComentarioDTO>
		//				{
		//					new SubComentarioDTO()
		//					{
		//						Id = 1,
		//						SubComentario = 20,
		//						Autor = new AutorDTO()
		//						{
		//							Id= 3,
		//							ImagenAutor = new ImagenesDTO()
		//							{
		//								Id = 12,
		//								URLImagen = "/assets/images/photos/11.jpg",
		//								Alt = "altPruebas doce",
		//								Descripcion = "Descripcion prueba doce",
		//								Titulo = "Titulo de la image doce"
		//							},
		//							Nombre = "Autor Desconocido 2",
		//							Descripcion = "Esta es una descripción del autor desconocido 3"
		//						},
		//						ComentarioId = 1,
		//						FechaPublicacion= DateTime.Now,
		//						Contenido = "Este es un SubComentario"
		//					},
		//					new SubComentarioDTO()
		//					{
		//						Id = 2,
		//						SubComentario = 21,
		//						Autor = new AutorDTO()
		//						{
		//							Id= 4,
		//							ImagenAutor = new ImagenesDTO()
		//							{
		//								Id = 12,
		//								URLImagen = "/assets/images/photos/11.jpg",
		//								Alt = "altPruebas doce",
		//								Descripcion = "Descripcion prueba doce",
		//								Titulo = "Titulo de la image doce"
		//							},
		//							Nombre = "Autor Desconocido 4",
		//							Descripcion = "Esta es una descripción del autor desconocido 4"
		//						},
		//						ComentarioId = 1,
		//						FechaPublicacion= DateTime.Now,
		//						Contenido = "Este es un segundo SubComentario"
		//					}
		//				}
		//			},
		//			new ComentariosDTO()
		//			{
		//				Id = 2,
		//				Autor = new AutorDTO()
		//				{
		//					Id= 5,
		//					ImagenAutor = new ImagenesDTO()
		//					{
		//						Id = 13,
		//						URLImagen = "/assets/images/photos/11.jpg",
		//						Alt = "altPruebas trece",
		//						Descripcion = "Descripcion prueba trece",
		//						Titulo = "Titulo de la image trece"
		//					},
		//					Nombre = "Autor Desconocido 5",
		//					Descripcion = "Esta es una descripción del autor desconocido 5"
		//				},
		//				FechaPublicacion = DateTime.Now,
		//				Contenido = "Este es un comentario",
		//				SubComentarios = new List<SubComentarioDTO>()
		//			}
		//		},
		//		Galeria = new List<ImagenesDTO>()
		//		{
		//			new ImagenesDTO()
		//			{
		//				Id = 1,
		//				Alt = "Alt 1",
		//				Descripcion = "Descripcion Imagen 1",
		//				Titulo = "Titulo imagen 1",
		//				URLImagen = "/assets/images/photos/blogmain2.jpg",
		//				URLSoporte = null
		//			},
		//			new ImagenesDTO()
		//			{
		//				Id = 2,
		//				Alt = "Alt 2",
		//				Descripcion = "Descripcion Imagen 2",
		//				Titulo = "Titulo imagen 2",
		//				URLImagen = "/assets/images/photos/blogmain2.jpg",
		//				URLSoporte = "www.eltiempo.com"
		//			},
		//			new ImagenesDTO()
		//			{
		//				Id = 3,
		//				Alt = "Alt 3",
		//				Descripcion = "Descripcion Imagen 3",
		//				Titulo = "Titulo imagen 3",
		//				URLImagen = "/assets/images/photos/blogmain2.jpg",
		//				URLSoporte = "www.eltiempo.com"
		//			}
		//		}
		//	},
		//	new BlogDTO()
		//	{
		//		Id = 3,
		//		Titulo = "¿Es posible vender mi Sentencia de Nulidad?",
		//		ImgBlog = "/assets/images/photos/blogmain2.jpg",
		//		Autor = new AutorDTO()
		//		{
		//			Id= 1,
		//			ImagenAutor = new ImagenesDTO()
		//			{
		//				Id = 14,
		//				URLImagen = "/assets/images/photos/11.jpg",
		//				Alt = "altPruebas catorce",
		//				Descripcion = "Descripcion prueba catorce",
		//				Titulo = "Titulo de la image catorce"
		//			},
		//			Nombre = "Autor Desconocido 1",
		//			Descripcion = "Esta es una descripción del autor desconocido 1"
		//		} ,
		//		FechaPublicacion = DateTime.Now,
		//		Categorias = new List<CategoriaDTO>()
		//		{
		//			new CategoriaDTO(){ Id = 1, Nombre = "Economia"},
		//			new CategoriaDTO(){ Id = 2, Nombre = "Sentencias"},
		//			new CategoriaDTO(){ Id = 3, Nombre = "Indicadores economicos"},
		//			new CategoriaDTO(){ Id = 4, Nombre = "Juridicos"}
		//		},
		//		Contenido = "Es decir, en una sentencia de nulidad se discute la legalidad o veracidad de un documento, que, en este caso, es un acto administrativo. En consecuencia, el afectado solicita reparación del daño.",
		//		IntroUno = "Iniciemos por dar claridad al concepto. La nulidad y restablecimiento del derecho se caracteriza porque se ejerce para obtener el reconocimiento de una situación jurídica en particular y la adopción de las medidas adecuadas para su pleno restablecimiento o reparación. Esta acción solo se puede ejercer por quien demuestre un interés, es decir, por quien se considere afectado en su derecho.",
		//		Comentarios = new List<ComentariosDTO>()
		//		{
		//			new ComentariosDTO()
		//			{
		//				Id = 1,
		//				Autor = new AutorDTO()
		//				{
		//					Id= 2,
		//					ImagenAutor = new ImagenesDTO()
		//					{
		//						Id = 15,
		//						URLImagen = "/assets/images/photos/11.jpg",
		//						Alt = "altPruebas quince",
		//						Descripcion = "Descripcion prueba quince",
		//						Titulo = "Titulo de la image quince"
		//					},
		//					Nombre = "Autor Desconocido 2",
		//					Descripcion = "Esta es una descripción del autor desconocido 2"
		//				},
		//				FechaPublicacion = DateTime.Now,
		//				Contenido = "Este es un comentario",
		//				SubComentarios = new List<SubComentarioDTO>
		//				{
		//					new SubComentarioDTO()
		//					{
		//						Id = 1,
		//						SubComentario = 20,
		//						Autor = new AutorDTO()
		//						{
		//							Id= 3,
		//							ImagenAutor = new ImagenesDTO()
		//							{
		//								Id = 16,
		//								URLImagen = "/assets/images/photos/11.jpg",
		//								Alt = "altPruebas dieciseis",
		//								Descripcion = "Descripcion prueba dieciseis",
		//								Titulo = "Titulo de la image dieciseis"
		//							},
		//							Nombre = "Autor Desconocido 2",
		//							Descripcion = "Esta es una descripción del autor desconocido 3"
		//						},
		//						ComentarioId = 1,
		//						FechaPublicacion= DateTime.Now,
		//						Contenido = "Este es un SubComentario"
		//					},
		//					new SubComentarioDTO()
		//					{
		//						Id = 2,
		//						SubComentario = 21,
		//						Autor = new AutorDTO()
		//						{
		//							Id= 4,
		//							ImagenAutor = new ImagenesDTO()
		//							{
		//								Id = 16,
		//								URLImagen = "/assets/images/photos/11.jpg",
		//								Alt = "altPruebas dieciseis",
		//								Descripcion = "Descripcion prueba dieciseis",
		//								Titulo = "Titulo de la image dieciseis"
		//							},
		//							Nombre = "Autor Desconocido 4",
		//							Descripcion = "Esta es una descripción del autor desconocido 4"
		//						},
		//						ComentarioId = 1,
		//						FechaPublicacion= DateTime.Now,
		//						Contenido = "Este es un segundo SubComentario"
		//					}
		//				}
		//			},
		//			new ComentariosDTO()
		//			{
		//				Id = 2,
		//				Autor = new AutorDTO()
		//				{
		//					Id= 5,
		//					ImagenAutor = new ImagenesDTO()
		//					{
		//						Id = 16,
		//						URLImagen = "/assets/images/photos/11.jpg",
		//						Alt = "altPruebas dieciseis",
		//						Descripcion = "Descripcion prueba dieciseis",
		//						Titulo = "Titulo de la image dieciseis"
		//					},
		//					Nombre = "Autor Desconocido 5",
		//					Descripcion = "Esta es una descripción del autor desconocido 5"
		//				},
		//				FechaPublicacion = DateTime.Now,
		//				Contenido = "Este es un comentario",
		//				SubComentarios = new List<SubComentarioDTO>()
		//			}
		//		},
		//		Galeria = new List<ImagenesDTO>()
		//		{
		//			new ImagenesDTO()
		//			{
		//				Id = 1,
		//				Alt = "Alt 1",
		//				Descripcion = "Descripcion Imagen 1",
		//				Titulo = "Titulo imagen 1",
		//				URLImagen = "/assets/images/photos/blogmain2.jpg",
		//				URLSoporte = null
		//			},
		//			new ImagenesDTO()
		//			{
		//				Id = 2,
		//				Alt = "Alt 2",
		//				Descripcion = "Descripcion Imagen 2",
		//				Titulo = "Titulo imagen 2",
		//				URLImagen = "/assets/images/photos/blogmain2.jpg",
		//				URLSoporte = "www.eltiempo.com"
		//			},
		//			new ImagenesDTO()
		//			{
		//				Id = 3,
		//				Alt = "Alt 3",
		//				Descripcion = "Descripcion Imagen 3",
		//				Titulo = "Titulo imagen 3",
		//				URLImagen = "/assets/images/photos/blogmain2.jpg",
		//				URLSoporte = "www.eltiempo.com"
		//			}
		//		}
		//	}
		//};
		//return lstBlogsPorCategoria;


    }

	[HttpGet]
	public async Task<IActionResult> Create()
	{
		var model = new List<CategoriaDTO>();
		var client = new HttpClient();
		var request = new HttpRequestMessage(HttpMethod.Get, "https://apileadconfival.azurewebsites.net/api/categoria");
		request.Headers.Add("XApiKey", "H^qP[7p#$18EXbV(lIP5xu+tCe-kgCM&{i_V,=(&");
		var content = new StringContent(string.Empty);
		content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
		request.Content = content;
		var response = await client.SendAsync(request);
		if (response.IsSuccessStatusCode)
		{
			var responseStream = await response.Content.ReadAsStringAsync();
			var lstCategorias = JsonConvert.DeserializeObject<List<CategoriaDTO>>(responseStream);
			model = lstCategorias.ToList();
		}
		return View(model);
	}

	[HttpGet]
	public async Task<IActionResult> CategoryPartialView()
	{
		var list = new List<CategoriaDTO>();
		var client = new HttpClient();
		var request = new HttpRequestMessage(HttpMethod.Get, "https://apileadconfival.azurewebsites.net/api/categoria");
		request.Headers.Add("XApiKey", "H^qP[7p#$18EXbV(lIP5xu+tCe-kgCM&{i_V,=(&");
		var content = new StringContent(string.Empty);
		content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
		request.Content = content;
		var response = await client.SendAsync(request);
		if (response.IsSuccessStatusCode)
		{
			var responseStream = await response.Content.ReadAsStringAsync();
			var lstCategorias = JsonConvert.DeserializeObject<List<CategoriaDTO>>(responseStream);
			list = lstCategorias.ToList();
		}
		return PartialView("_CategoriasSecundarias.cshtml", list);


		
	}


	[HttpGet]
	public async Task<bool> CreateBlog(string titulo, string contenido, List<string> lstCategorias, string categoria)
	{
		var array = lstCategorias.FirstOrDefault().Split(",");
		var categoriaPrincipal = Convert.ToInt32(categoria.Replace('"', ' ').Trim());
		int[] numeros = new int[1];
		numeros[numeros.Length - 1] = categoriaPrincipal;
		foreach (var item in array)
		{
			var valor = Convert.ToInt32(item.Replace('[', ' ').Replace(']', ' ').Replace('"', ' ').Trim());
			Array.Resize(ref numeros, numeros.Length + 1);
			numeros[numeros.Length -1] = valor;
		}
		var obj = new BlogBetaDTO()
		{
			Titulo = titulo,
			Contenido = contenido,
			CategoriaId = numeros
		};
		var json = JsonConvert.SerializeObject(obj);
		var client = new HttpClient();
		var request = new HttpRequestMessage(HttpMethod.Post, "https://apileadconfival.azurewebsites.net/api/blog");
		request.Headers.Add("XApiKey", "H^qP[7p#$18EXbV(lIP5xu+tCe-kgCM&{i_V,=(&");
		var content = new StringContent(json, null, "application/json");
		request.Content = content;
		var response = await client.SendAsync(request);
		if (response.IsSuccessStatusCode)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	[HttpGet]
	public IActionResult AddCategory()
	{
		var model = new CategoriaDTO() { };
		return View(model);
	}

}