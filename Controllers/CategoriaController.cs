using frontend.Models;
using frontend.Services.Graph;
using frontend.Services.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace frontend.Controllers
{
	public class CategoriaController : Controller
	{
        private readonly IGetToken _getToken;
        private readonly IGraphServices _graphServices;

        public CategoriaController(IGetToken getToken, IGraphServices graphServices)
        {
            _getToken = getToken;
            _graphServices = graphServices;
        }

        /// <summary>
        /// Devuelve las vista para crear una categoria
        /// </summary>
        /// <returns></returns>
        [Authorize]
		[HttpGet]
		public async Task<IActionResult> AddCategory()
		{
			var model = new CategoriaDTO() { };
			var objToken = await _getToken.GetTokenMicrosoft();
            ViewBag.Imagen = await _graphServices.ImgProfile(objToken.access_token);
            var me = await _graphServices.GetMeGraph(objToken.access_token);
            ViewBag.user = me.DisplayName;
            return View(model);
		}

		/// <summary>
		/// Devuelve la vista para visualizar las categorias
		/// </summary>
		/// <returns></returns>
		[Authorize]
		[HttpGet]
		public async Task<IActionResult> Get()
		{
            var objToken = await _getToken.GetTokenMicrosoft();
            ViewBag.Imagen = await _graphServices.ImgProfile(objToken.access_token);
            var me = await _graphServices.GetMeGraph(objToken.access_token);
            ViewBag.user = me.DisplayName;
			var model = new List<CategoriaDTO>();
			var client = new HttpClient();
			var request = new HttpRequestMessage(HttpMethod.Get, "https://apileadconfival.azurewebsites.net/api/categoria/categorias");
			request.Headers.Add("XApiKey", "H^qP[7p#$18EXbV(lIP5xu+tCe-kgCM&{i_V,=(&");
			var content = new StringContent(string.Empty);
			content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
			request.Content = content;
			var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
			{
				var responseStream = await response.Content.ReadAsStringAsync();
				model = JsonConvert.DeserializeObject<List<CategoriaDTO>>(responseStream);
				return View(model);
			}
			else
			{
				return View(model);
			}
		}

		/// <summary>
		/// Devuelve las vista para editar una categoria
		/// </summary>
		/// <returns></returns>
		[Authorize]
		[HttpGet]
		public async Task<IActionResult> EditCategory(int id)
		{
			var parametro = id.ToString();
			var model = new CategoriaDTO() { };
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
				model = JsonConvert.DeserializeObject<CategoriaDTO>(responseStream);
				return View("~/Views/Categoria/AddCategory.cshtml",  model);
			}
			else
			{
				return View("~/Views/Categoria/AddCategory.cshtml", model);
			}
		}

		[Authorize]
		[HttpGet]
		public async Task<bool> EditCategoryDB(string nombre, string id)
		{
			var obj = new CategoriaDTO()
			{
				Id = Convert.ToInt32(id),
				Nombre = nombre
			};
			var json = JsonConvert.SerializeObject(obj);
			var client = new HttpClient();
			var request = new HttpRequestMessage(HttpMethod.Put, "https://apileadconfival.azurewebsites.net/api/categoria/" + id);
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

		[Authorize]
		[HttpGet]
		public async Task<bool> SaveCategoria(string nombre)
		{
			var obj = new CategoriaDTO()
			{
				Id = 0,
				Nombre = nombre
			};
			var json = JsonConvert.SerializeObject(obj);
			var client = new HttpClient();
			var request = new HttpRequestMessage(HttpMethod.Post, "https://apileadconfival.azurewebsites.net/api/categoria");
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
	}
}
