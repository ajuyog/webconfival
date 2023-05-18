using confinancia.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace confinancia.Controllers
{
	public class CategoriaController : Controller
	{
		/// <summary>
		/// Devuelve las vista para crear una categoria
		/// </summary>
		/// <returns></returns>
		[Authorize]
		[HttpGet]
		public IActionResult AddCategory()
		{
			var model = new CategoriaDTO() { };
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
	}
}
