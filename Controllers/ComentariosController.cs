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
		public async Task<IActionResult> Edit(int idBlog, string titulo, int pagina, int registros)
		{
			if(pagina == 0) { pagina = 1; }
			registros = 10;
			var model = await _comentariosServices.Get(idBlog, pagina, registros);
			model.Count = model.totalBlogs;
			model.Paginas = (int)Math.Ceiling((double)model.Count / registros);
			model.BaseUrl = _configuration["LandingPage:RedirectGraph:https"] + "Comentarios/Edit?idBlog=" + idBlog + "&titulo=" + titulo + "&pagina=";
			model.PaginaActual = pagina;
			var blog = await _blogServices.GetById(idBlog);
			ViewBag.Titulo = titulo;
			ViewBag.Categoria = blog.Categorias.First().Nombre;
			ViewBag.Publicacion = blog.Publicacion;
			ViewBag.ImagenBlog = await _blogServices.Imagen(idBlog);
			ViewBag.IdBlog = idBlog;

			var objToken = await _getToken.GetTokenMicrosoft();
			ViewBag.Imagen = await _graphServices.ImgProfile(objToken.access_token);
			var me = await _graphServices.GetMeGraph(objToken.access_token);
			ViewBag.user = me.DisplayName;

			return View(model);
		}

		[Authorize, HttpGet]
		public async Task<bool> ApproveComment(int id, int idBlog, bool state)
		{
			return await _comentariosServices.ApproveComment(id, idBlog, state);
		}

		[Authorize, HttpGet]
		public async Task<bool> DiscardComment(int id, int idBlog)
		{
			return await _comentariosServices.DiscardComment(id, idBlog);
		}

		[HttpGet]
		public async Task<bool> Create(int id, string comentario, string relation)
		{
			var result = await _comentariosServices.Create(id, comentario, relation);
			return result;
        }

		[Authorize, HttpPost]
		public async Task<IActionResult> EditSearch(int pagina, int registros, string search, int blogId)
		{
            if (pagina == 0) { pagina = 1; }
            registros = 10;
            var model = await _comentariosServices.GetSearch(pagina, registros, search, blogId);
            model.Paginas = (int)Math.Ceiling((double)model.totalBlogs / registros);
            model.BaseUrl = _configuration["LandingPage:RedirectGraph:https"] + "Comentarios/EditSearchGet?search=" + search + "&blogId=" + blogId + "&pagina=";
            model.PaginaActual = pagina;
			model.Count = model.totalBlogs;
			var blog = await _blogServices.GetById(blogId);
            ViewBag.Titulo = blog.Titulo;
            ViewBag.Categoria = blog.Categorias.First().Nombre;
            ViewBag.Publicacion = blog.Publicacion;
            ViewBag.ImagenBlog = await _blogServices.Imagen(blogId);
			ViewBag.IdBlog = blogId;
            return View("~/Views/Comentarios/Edit.cshtml", model);
        }

		[Authorize, HttpGet]
		public async Task<IActionResult> EditSearchGet(string search, int blogId, int pagina, int registros)
		{
			if (pagina == 0) { pagina = 1; }
			registros = 10;
			var model = await _comentariosServices.GetSearch(pagina, registros, search, blogId);
			model.Paginas = (int)Math.Ceiling((double)model.totalBlogs / registros);
			model.BaseUrl = _configuration["LandingPage:RedirectGraph:https"] + "Comentarios/EditSearchGet?search=" + search + "&blogId=" + blogId + "&pagina=";
			model.PaginaActual = pagina;
			model.Count = model.totalBlogs;
			var blog = await _blogServices.GetById(blogId);
			ViewBag.Titulo = blog.Titulo;
			ViewBag.Categoria = blog.Categorias.First().Nombre;
			ViewBag.Publicacion = blog.Publicacion;
			ViewBag.ImagenBlog = await _blogServices.Imagen(blogId);
			ViewBag.IdBlog = blogId;
			return View("~/Views/Comentarios/Edit.cshtml", model);
		}


	}
}
