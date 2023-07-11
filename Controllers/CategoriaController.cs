using frontend.Models;
using frontend.Services.Graph;
using frontend.Services.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace frontend.Controllers
{
	public class CategoriaController : Controller
	{
        #region CONSTRUCTOR
        private readonly IGetToken _getToken;
        private readonly IGraphServices _graphServices;

        public CategoriaController(IGetToken getToken, IGraphServices graphServices)
        {
            _getToken = getToken;
            _graphServices = graphServices;
        }
        #endregion

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
			var token = await _getToken.GetTokenV();

			var model = new List<CategoriaDTO>();
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://api2valuezbpm.azurewebsites.net/api/Categoria/categorias");
            request.Headers.Add("Authorization", "Bearer " + token);
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStringAsync();
                var lstCategorias = JsonConvert.DeserializeObject<List<CategoriaDTO>>(responseStream);
                model = lstCategorias.ToList();
            }
			return View(model);

        }

		/// <summary>
		/// Devuelve las vista para editar una categoria
		/// </summary>
		/// <returns></returns>
		[Authorize]
		[HttpGet]
		public async Task<IActionResult> EditCategory(int id)
		{
            var objToken = await _getToken.GetTokenMicrosoft();
            ViewBag.Imagen = await _graphServices.ImgProfile(objToken.access_token);
            var me = await _graphServices.GetMeGraph(objToken.access_token);
            ViewBag.user = me.DisplayName;

            var token = await _getToken.GetTokenV();
			var model = new CategoriaDTO();
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://api2valuezbpm.azurewebsites.net/api/Categoria/" + id);
            request.Headers.Add("Authorization", "Bearer " + token);
            var response = await client.SendAsync(request);
			if (response.IsSuccessStatusCode)
			{
                var responseStream = await response.Content.ReadAsStringAsync();
                model = JsonConvert.DeserializeObject<CategoriaDTO>(responseStream);
			}
			else
			{
                var responseStream = await response.Content.ReadAsStringAsync();
				var readError = "leer linea anterior";
            }
            return View("~/Views/Categoria/AddCategory.cshtml", model);
        }

        [Authorize]
		[HttpGet]
		public async Task<bool> EditCategoryDB(string nombre, string id)
		{
            var result = false;
            var obj = new CategoriaDTO()
            {
                Nombre = nombre
            };
            var json = JsonConvert.SerializeObject(obj);
            var token = await _getToken.GetTokenV();
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Put, "https://api2valuezbpm.azurewebsites.net/api/Categoria/" + id);
            request.Headers.Add("Authorization", "Bearer " + token);
            var content = new StringContent(json, null, "application/json");
            request.Content = content;
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                result = true;
            }
            return result;

        }

        [Authorize]
		[HttpGet]
		public async Task<bool> SaveCategoria(string nombre)
		{
			var result = false;
			var obj = new CategoriaDTO()
			{
				Nombre = nombre
			};
            var json = JsonConvert.SerializeObject(obj);
            var token = await _getToken.GetTokenV();
            var client = new HttpClient();
			var request = new HttpRequestMessage(HttpMethod.Post, "https://api2valuezbpm.azurewebsites.net/api/Categoria");
			request.Headers.Add("Authorization", "Bearer " + token);
			var content = new StringContent(json, null, "application/json");
			request.Content = content;
			var response = await client.SendAsync(request);
			if(response.IsSuccessStatusCode)
			{
				result = true;
			}
            return result;
        }
    }
}
