using frontend.Models;
using frontend.Services.Categorias;
using frontend.Services.Graph;
using frontend.Services.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph.Models;
using Microsoft.Win32;
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
		private readonly ICategoriasServices _categoriasServices;
		private readonly IConfiguration _configuration;

		public CategoriaController(IGetToken getToken, IGraphServices graphServices, ICategoriasServices categoriasServices, IConfiguration configuration)
        {
            _getToken = getToken;
            _graphServices = graphServices;
			_categoriasServices = categoriasServices;
			_configuration = configuration;
		}
        #endregion

		/// <summary>
		/// Devuelve la vista para visualizar las categorias
		/// </summary>
		/// <returns></returns>
		[Authorize]
		[HttpGet]
		public async Task<IActionResult> Get(int pagina, int registros)
		{
            if(pagina == 0) { pagina = 1; }
            registros = 10;
            var objToken = await _getToken.GetTokenMicrosoft();
            ViewBag.Imagen = await _graphServices.ImgProfile(objToken.access_token);
            var me = await _graphServices.GetMeGraph(objToken.access_token);
            ViewBag.user = me.DisplayName;

            var lstCategorias = await _categoriasServices.Get(pagina, registros, true);
            var model = new CategoriasDTO();
            model.Categorias = lstCategorias;
            model.Count = model.Categorias.Count();
			model.Paginas = (int)Math.Ceiling((double)model.Count / registros);
			model.BaseUrl = _configuration["LandingPage:RedirectGraph:https"] + "Categoria/Get?pagina=";
			model.PaginaActual = pagina;
			return View(model);

        }

        /// <summary>
        /// Devuelve las vista para crear una categoria
        /// </summary>
        /// <returns></returns>
        [Authorize]
		[HttpGet]
		public async Task<IActionResult> Create()
		{
			var model = new CategoriaDTO() { };
			var objToken = await _getToken.GetTokenMicrosoft();
            ViewBag.Imagen = await _graphServices.ImgProfile(objToken.access_token);
            var me = await _graphServices.GetMeGraph(objToken.access_token);
            ViewBag.user = me.DisplayName;
            return View(model);
		}

		/// <summary>
		/// Devuelve las vista para editar una categoria
		/// </summary>
		/// <returns></returns>
		[Authorize]
		[HttpGet]
		public async Task<IActionResult> Edit(int id)
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
            return View("~/Views/Categoria/Create.cshtml", model);
        }

        /// <summary>
        /// Devuelve la vista para Eliminar una categoria
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nombre"></param>
        /// <returns></returns>
        //[Authorize]
        //[HttpGet]
        //public IActionResult Delete(int id) 
        //{
        //    ViewBag.Nombre = nombre;
        //    return PartialView("~/Views/Shared/_Delete.cshtml");
        //}


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
