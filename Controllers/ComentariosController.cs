using frontend.Models;
using frontend.Models.JsonDTO;
using frontend.Services.Comentarios;
using frontend.Services.Graph;
using frontend.Services.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        public ComentariosController(IConfiguration configuration, IGetToken getToken, IGraphServices graphServices, IComentariosServices comentariosServices)
        {
			_configuration = configuration;
			_getToken = getToken;
            _graphServices = graphServices;
            _comentariosServices = comentariosServices;
        }
		#endregion

		[Authorize]
		[HttpGet]
		public async Task<IActionResult> EditComment(int idBlog, string titulo)
		{
			var model = await _comentariosServices.GetByStateFalse(idBlog);
			ViewBag.Titulo = titulo;
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
			var client = new HttpClient();
			var token = await _getToken.GetTokenV();
			var obj = new ComentarioDTO();
			obj.Comentario = comentario;
			var json = JsonConvert.SerializeObject(obj);
			var request = new HttpRequestMessage(HttpMethod.Post, "https://api2valuezbpm.azurewebsites.net/api/blog/" + id + "/comentarios/" + relation);
			request.Headers.Add("Authorization", "Bearer " + token);
			var content = new StringContent(json, null, "application/json");
			request.Content = content;
			var response = await client.SendAsync(request);
			if(response.IsSuccessStatusCode)
			{
				return true;
            }
			return false;
        }
		
		

	}
}
