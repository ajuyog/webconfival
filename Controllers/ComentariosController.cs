using frontend.Models;
using frontend.Models.JsonDTO;
using frontend.Services.Blogs;
using frontend.Services.Comentarios;
using frontend.Services.Graph;
using frontend.Services.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Net.Http.Headers;
using System.Reflection;

namespace frontend.Controllers
{
	public class ComentariosController : Controller
	{
		#region CONSTRUCTOR
		private readonly IConfiguration _configuration;
		private readonly IGetToken _getToken;
        private readonly IGraphServices _graphServices;
        private readonly IComentariosServices _comentariosServices;
		private readonly IBlogServices _blogServices;

		public ComentariosController(IConfiguration configuration, IGetToken getToken, IGraphServices graphServices, IComentariosServices comentariosServices, IBlogServices blogServices)
        {
			_configuration = configuration;
			_getToken = getToken;
            _graphServices = graphServices;
            _comentariosServices = comentariosServices;
			_blogServices = blogServices;
		}
		#endregion

		[Authorize]
		[HttpGet]
		public async Task<IActionResult> EditComment(int idBlog, string titulo, int pagina, int registros)
		{
			if(pagina == 0) { pagina = 1; }
			registros = 10;
			var model = new ComentariosDTO();
			model = await _comentariosServices.Get(idBlog, pagina, registros);
			model.Paginas = (int)Math.Ceiling((double)model.totalBlogs / registros);
			model.BaseUrl = _configuration["LandingPage:RedirectGraph:https"] + "Comentarios/EditComment?idBlog=" + idBlog + "&titulo=" + titulo + "&pagina=";
			model.PaginaActual = pagina;
			var blog = await _blogServices.GetById(idBlog);
			ViewBag.Titulo = titulo;
			ViewBag.Categoria = blog.Categorias.First().Nombre;
			ViewBag.Publicacion = blog.Publicacion;
			ViewBag.ImagenBlog = await _blogServices.Imagen(idBlog);
			ViewBag.IdBlog = idBlog;
			return View(model);
		}

		[HttpGet]
		public async Task<bool> ApproveComment(int id, int idBlog)
		{
			return await _comentariosServices.ApproveComment(id, idBlog);
		}

		[HttpGet]
		public async Task<bool> Create(int id, string comentario, string relation)
		{
			var result = await _comentariosServices.Create(id, comentario, relation);
			return result;
        }
		
		

	}
}
