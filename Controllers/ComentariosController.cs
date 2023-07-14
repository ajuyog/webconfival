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

		/// <summary>
		/// Devuelve la vista con el listado de comentarios
		/// </summary>
		/// <returns></returns>
		[Authorize]
		[HttpGet]
		//public async Task<IActionResult> Get()
		//{
		//	var objToken = await _getToken.GetTokenMicrosoft();
  //          ViewBag.Imagen = await _graphServices.ImgProfile(objToken.access_token);
  //          var me = await _graphServices.GetMeGraph(objToken.access_token);
  //          ViewBag.user = me.DisplayName;

		//	var model = await _comentariosServices.GetByStateFalse()
			



		//	if (response.IsSuccessStatusCode)
		//	{
		//		var responseStream = await response.Content.ReadAsStringAsync();
		//		model = JsonConvert.DeserializeObject<List<ComentariosDTO>>(responseStream);
		//		return View(model);
		//	}
		//	else
		//	{
		//		return View( model);
		//	}
  //      }

		/// <summary>
		/// Permite crear un comentario mediante API RestFull
		/// </summary>
		/// <param name="id"></param>
		/// <param name="comentario"></param>
		/// <returns></returns>
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
		
		[Authorize]
		[HttpGet]
		public IActionResult Editar(int id)
		{
			var model = new ComentariosDTO()
			{
				Id = 1,
				Autor = new AutorDTO()
				{
					Id = 1,
					Correo = "correo@hotmail.com",
					ImagenAutor = new ImagenesDTO()
					{
						URLImagen = "/assets/images/photos/11.jpg",
					},
					Nombre = "Autor desconocido 25"
				},
				
				Comentario = "Un comentario realizado al blog por defecto",
				Activo = false,
				FechaPublicacion = DateTime.Now,

			};
			return PartialView("~/Views/Comentarios/_Editar.cshtml", model);
		}

	}
}
