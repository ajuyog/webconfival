using confinancia.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Reflection;

namespace confinancia.Controllers
{
	public class ComentariosController : Controller
	{
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
	}
}
