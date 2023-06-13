using confinancia.Models;
using confinancia.Models.Graph;
using confinancia.Models.JsonDTO;
using confinancia.Services.Token;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;

namespace confinancia.Controllers
{
	public class GraphController : Controller
	{
		private readonly IGetToken _getToken;

		public GraphController(IGetToken getToken)
		{
			_getToken = getToken;
		}


		[Consumes("application/x-www-form-urlencoded")]
		public async Task<IActionResult> Getoutlook([FromForm] IFormCollection value)
		{
			string code = value.First().Value;
			string redirect = "Graph/GetOutlook";
			string accesToken = await _getToken.GetTokenMGraph(code, redirect);
			ViewBag.ImageData = await ImgProfile(accesToken);
			var model = await GetMeGraph(accesToken);
			return View(model);
		}

		[Consumes("application/x-www-form-urlencoded")]
		public async Task<IActionResult> GetTeams([FromForm] IFormCollection value)
		{
			string code = value.First().Value;
			string redirect = "Graph/GetTeams";
			string accesToken = await _getToken.GetTokenMGraph(code, redirect);
			var model = await GetMeGraph(accesToken);
			ViewBag.ImageData = await ImgProfile(accesToken);
			return View(model);
		}



		[HttpGet]
		public async Task<string> ImgProfile(string token)
		{
			var client = new HttpClient();
			var request = new HttpRequestMessage(HttpMethod.Get, "https://graph.microsoft.com/v1.0/me/photo/$value");
			request.Headers.Add("Authorization", "Bearer " + token);
			var response = await client.SendAsync(request);
			if (response.IsSuccessStatusCode)
			{
				var responseStream = await response.Content.ReadAsStreamAsync();
				MemoryStream ms = new MemoryStream();
				responseStream.CopyTo(ms);
				byte[] buffer = ms.ToArray();
				string result = Convert.ToBase64String(buffer);
				return string.Format("data:image/png;base64,{0}", result);
			}
			else
			{
				return "~/assets/images/faces/6.jpg";
			}
		}

		[HttpGet]
		public async Task<MeGraphDTO> GetMeGraph(string token)
		{
			var client = new HttpClient();
			var model = new MeGraphDTO();
			var requestMe = new HttpRequestMessage(HttpMethod.Get, "https://graph.microsoft.com/v1.0/me");
			requestMe.Headers.Add("Authorization", "Bearer " + token);
			var responseMe = await client.SendAsync(requestMe);
			if (responseMe.IsSuccessStatusCode)
			{
				var responseStreamMe = await responseMe.Content.ReadAsStringAsync();
				model = JsonConvert.DeserializeObject<MeGraphDTO>(responseStreamMe);
			}
			return model;
		}
	}
}
