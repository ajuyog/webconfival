﻿using frontend.Models;
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

            var model = await _categoriasServices.GetAdmin(pagina, registros);
            model.Count = model.TotalCategoria;
			model.Paginas = (int)Math.Ceiling((double)model.Count / registros);
			model.BaseUrl = _configuration["LandingPage:RedirectGraph:https"] + "Categoria/Get?pagina=";
			model.PaginaActual = pagina;
			return View(model);

        }

        /// <summary>
        /// Devuelve la vista para visualiza categorias por atributo Search
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="registros"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        [Authorize, HttpPost]
        public async Task<IActionResult> EditSearch(int pagina, int registros, string search)
        {
            if (pagina == 0) { pagina = 1; }
            registros = 10;
            var objToken = await _getToken.GetTokenMicrosoft();
            ViewBag.Imagen = await _graphServices.ImgProfile(objToken.access_token);
            var me = await _graphServices.GetMeGraph(objToken.access_token);
            ViewBag.user = me.DisplayName;

            var model = await _categoriasServices.GetSearch(pagina, registros, search);
            model.Count = model.TotalCategoria;
            model.Paginas = (int)Math.Ceiling((double)model.Count / registros);
            model.BaseUrl = _configuration["LandingPage:RedirectGraph:https"] + "Categoria/EditSearchGet?search=" + search + "&pagina=";
            model.PaginaActual = pagina;
            return View("~/Views/Categoria/Get.cshtml", model);
        }

        [Authorize, HttpGet]
        public async Task<IActionResult> EditSearchGet(string search, int pagina, int registros)
        {
            if (pagina == 0) { pagina = 1; }
            registros = 10;
            var objToken = await _getToken.GetTokenMicrosoft();
            ViewBag.Imagen = await _graphServices.ImgProfile(objToken.access_token);
            var me = await _graphServices.GetMeGraph(objToken.access_token);
            ViewBag.user = me.DisplayName;

            var model = await _categoriasServices.GetSearch(pagina, registros, search);
            model.Count = model.TotalCategoria;
            model.Paginas = (int)Math.Ceiling((double)model.Count / registros);
            model.BaseUrl = _configuration["LandingPage:RedirectGraph:https"] + "Categoria/EditSearchGet?search=" + search + "&pagina=";
            model.PaginaActual = pagina;
            return View("~/Views/Categoria/Get.cshtml", model);
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
        /// Permite Eliminar una categoria
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<bool> Delete(int id)
        {
            return await _categoriasServices.Delete(id);
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

        [Authorize, HttpGet]
        public async Task<CategoriasAdminDTO> Exists()
        {
            return await _categoriasServices.GetAdmin(1, 1000);

        }

    }
}
