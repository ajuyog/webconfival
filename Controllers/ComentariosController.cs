using frontend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Reflection;

namespace frontend.Controllers
{
	public class ComentariosController : Controller
	{
		/// <summary>
		/// Devuelve la vista con el listado de comentarios
		/// </summary>
		/// <returns></returns>
		[Authorize]
		[HttpGet]
		public async Task<IActionResult> Get()
		{
			var model = new List<ComentariosDTO>() { };
			var client = new HttpClient();
			var request = new HttpRequestMessage(HttpMethod.Get, "https://apileadconfival.azurewebsites.net/api/blog/1/comentarios");
			request.Headers.Add("XApiKey", "H^qP[7p#$18EXbV(lIP5xu+tCe-kgCM&{i_V,=(&");
			var content = new StringContent(string.Empty);
			content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
			request.Content = content;
			var response = await client.SendAsync(request);
			if (response.IsSuccessStatusCode)
			{
				var responseStream = await response.Content.ReadAsStringAsync();
				model = JsonConvert.DeserializeObject<List<ComentariosDTO>>(responseStream);
				return View(model);
			}
			else
			{
				return View( model);
			}
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
				BlogId = new BlogDTO()
				{
					Id = 1,
					Titulo = "Un blog por defecto"
				},
				Comentario = "Un comentario realizado al blog por defecto",
				Activo = false,
				FechaPublicacion = DateTime.Now,

			};
			return PartialView("~/Views/Comentarios/_Editar.cshtml", model);
		}

	}
}
