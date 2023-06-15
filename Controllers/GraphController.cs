using Azure;
using Azure.Identity;
using confinancia.Models;
using confinancia.Models.Graph;
using confinancia.Models.JsonDTO;
using confinancia.Services.Token;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using Newtonsoft.Json;
using System.Globalization;
using System.Net.Http.Headers;
using System.Reflection;
using Tavis.UriTemplates;

namespace confinancia.Controllers
{
	public class GraphController : Controller
	{
        #region CONSTRUCTOR
        private readonly IGetToken _getToken;
        private readonly IConfiguration _configuration;
        public GraphController(IGetToken getToken, IConfiguration configuration)
		{
			_getToken = getToken;
            _configuration = configuration;
        }
        #endregion

        [Consumes("application/x-www-form-urlencoded")]
		public async Task<IActionResult> Getoutlook([FromForm] IFormCollection value)
		{
			string code = value.First().Value;
			var skip = (Convert.ToInt32(value.ElementAt(1).Value) == 0 ? 0 : (Convert.ToInt32(value.ElementAt(1).Value) - 1) * 10);
			string redirect = "Graph/GetOutlook";
			string accesToken = await _getToken.GetTokenMGraph(code, redirect);
			if(accesToken.Contains("administrador del sistema"))
            {
				var mensaje = accesToken;
                return RedirectToAction("Index", "Home", routeValues: new { mensaje });
            }
            ViewBag.ImageData = await ImgProfile(accesToken);
			var model = await GetMeGraph(accesToken);
			
            var client = new HttpClient();
			var modelDos = new MessagesGraphDTO();

            var request = new HttpRequestMessage(HttpMethod.Get, "https://graph.microsoft.com/v1.0/Users/" + model.Id + "/messages?$skip=" + skip.ToString() + "&count=true");
            request.Headers.Add("Authorization", "Bearer " + accesToken);
            var content = new StringContent(string.Empty);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            request.Content = content;
            var response = await client.SendAsync(request);
			if(response.IsSuccessStatusCode)
			{
				var responseStream = await response.Content.ReadAsStringAsync();
                modelDos = JsonConvert.DeserializeObject<MessagesGraphDTO>(responseStream);
                modelDos.GivenName = model.GivenName;
                modelDos.JobTitle = model.JobTitle;
				var array = responseStream.Split(",");
				modelDos.Count = Convert.ToInt32(array[1].ToString().Substring(15));
				modelDos.Paginas = (int)Math.Ceiling((double)modelDos.Count / 10);
				modelDos.BaseUrl = "https://login.microsoftonline.com/" + _configuration.GetSection("Azure:TenantId").Value + "/oauth2/v2.0/authorize?client_id=" + _configuration.GetSection("Azure:ClientId").Value + "&response_type=code&redirect_uri=https://localhost:7191/Graph/GetOutlook&response_mode=form_post&scope=user.read&state=";
				modelDos.PaginaActual = Convert.ToInt32(value.ElementAt(1).Value) == 0 ? 1 : Convert.ToInt32(value.ElementAt(1).Value);
				modelDos.MensajeBienvenida = ", Revisa tus correos mas recientes.";
                modelDos.value.ForEach(x => x.ReceivedDateTime = x.ReceivedDateTime.Substring(0, x.ReceivedDateTime.Length - 4).Replace("T", " ").Trim());

            }
            return View(modelDos);
		}

		[Consumes("application/x-www-form-urlencoded")]
		public async Task<IActionResult> GetoutlookSent([FromForm] IFormCollection value)
		{
			string code = value.First().Value;
			var skip = (Convert.ToInt32(value.ElementAt(1).Value) == 0 ? 0 : (Convert.ToInt32(value.ElementAt(1).Value) - 1) * 10);
			string redirect = "Graph/GetoutlookSent";
			string accesToken = await _getToken.GetTokenMGraph(code, redirect);
			ViewBag.ImageData = await ImgProfile(accesToken);
			var me = await GetMeGraph(accesToken);

			var client = new HttpClient();
			var modelDos = new MessagesGraphDTO();

			var folderId = await GetFolferId(me.Id, accesToken, "Elementos enviados");


            var request = new HttpRequestMessage(HttpMethod.Get, "https://graph.microsoft.com/v1.0/Users/" + me.Id + "/mailFolders/" + folderId + "/messages?$skip=" + skip + "&count=true");
			request.Headers.Add("Authorization", "Bearer " + accesToken);
			var response = await client.SendAsync(request);
			if (response.IsSuccessStatusCode)
			{
				var responseStream = await response.Content.ReadAsStringAsync();
				modelDos = JsonConvert.DeserializeObject<MessagesGraphDTO>(responseStream);
				modelDos.GivenName = me.GivenName;
				modelDos.JobTitle = me.JobTitle;
				var array = responseStream.Split(",");
				modelDos.Count = Convert.ToInt32(array[1].ToString().Substring(15));
				modelDos.Paginas = (int)Math.Ceiling((double)modelDos.Count / 10);
				modelDos.BaseUrl = "https://login.microsoftonline.com/" + _configuration.GetSection("Azure:TenantId").Value + "/oauth2/v2.0/authorize?client_id=" + _configuration.GetSection("Azure:ClientId").Value + "&response_type=code&redirect_uri=https://localhost:7191/Graph/GetoutlookSent&response_mode=form_post&scope=user.read&state=";
				modelDos.PaginaActual = Convert.ToInt32(value.ElementAt(1).Value) == 0 ? 1 : Convert.ToInt32(value.ElementAt(1).Value);
                modelDos.MensajeBienvenida = ", Revisa tus correos enviados más recientes.";
				modelDos.value.ForEach(x => x.ReceivedDateTime = x.ReceivedDateTime.Substring(0, x.ReceivedDateTime.Length - 4).Replace("T", " ").Trim());
            }
            return View(modelDos);
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

		[HttpGet]
		public async Task<string> GetFolferId(string meId, string token, string folderName)
		{
			var idFolder = "";
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://graph.microsoft.com/v1.0/Users/" + meId + "/mailFolders");
            request.Headers.Add("Authorization", "Bearer " + token);
            var response = await client.SendAsync(request);
            if(response.IsSuccessStatusCode)
			{
                var responseStreamMe = await response.Content.ReadAsStringAsync();
                var lstFolders = JsonConvert.DeserializeObject<MailFoldersDTO>(responseStreamMe);
				var folder =  lstFolders.Value.Where(x => x.DisplayName == folderName).FirstOrDefault();
				return folder.Id;
            }
			return idFolder;
        }
    }
}
