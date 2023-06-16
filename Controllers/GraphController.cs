using AutoMapper;
using Azure;
using Azure.Identity;
using confinancia.Models;
using confinancia.Models.Graph;
using confinancia.Models.JsonDTO;
using confinancia.Services.Token;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Reflection;
using Tavis.UriTemplates;

namespace confinancia.Controllers
{
	public class GraphController : Controller
	{
        #region CONSTRUCTOR
        private readonly IGetToken _getToken;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public GraphController(IGetToken getToken, IConfiguration configuration, IMapper mapper)
		{
			_getToken = getToken;
            _configuration = configuration;
            _mapper = mapper;
        }
        #endregion

        #region Bandeja de entrada
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
			var modelMe = await GetMeGraph(accesToken);
            var client = new HttpClient();
			var modelOutlook = new MessagesGraphDTO();
			var folderId = await GetFolferId(modelMe.Id, accesToken, "Bandeja de entrada");
			var request = new HttpRequestMessage(HttpMethod.Get, "https://graph.microsoft.com/v1.0/Users/" + modelMe.Id + "/mailFolders/" + folderId + "/messages?$skip=" + skip.ToString() + "&count=true");
            request.Headers.Add("Authorization", "Bearer " + accesToken);
            var content = new StringContent(string.Empty);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            request.Content = content;
            var response = await client.SendAsync(request);
			if(response.IsSuccessStatusCode)
			{
				var responseStream = await response.Content.ReadAsStringAsync();
                modelOutlook = JsonConvert.DeserializeObject<MessagesGraphDTO>(responseStream);
                modelOutlook.GivenName = modelMe.GivenName;
                modelOutlook.JobTitle = modelMe.JobTitle;
				var array = responseStream.Split(",");
				modelOutlook.Count = Convert.ToInt32(array[1].ToString().Substring(15));
				modelOutlook.Paginas = (int)Math.Ceiling((double)modelOutlook.Count / 10);
				modelOutlook.BaseUrl = "https://login.microsoftonline.com/" + _configuration.GetSection("Azure:TenantId").Value + "/oauth2/v2.0/authorize?client_id=" + _configuration.GetSection("Azure:ClientId").Value + "&response_type=code&redirect_uri=https://localhost:7191/Graph/GetOutlook&response_mode=form_post&scope=user.read&state=";
				modelOutlook.PaginaActual = Convert.ToInt32(value.ElementAt(1).Value) == 0 ? 1 : Convert.ToInt32(value.ElementAt(1).Value);
				modelOutlook.Folder = "Bandeja de entrada";
                modelOutlook.value.ForEach(x => x.ReceivedDateTime = x.ReceivedDateTime.Substring(0, x.ReceivedDateTime.Length - 4).Replace("T", " ").Trim());

            }
            return View(modelOutlook);
		}
        #endregion

        #region Elementos enviados
        [Consumes("application/x-www-form-urlencoded")]
		public async Task<IActionResult> GetoutlookSent([FromForm] IFormCollection value)
		{
			string code = value.First().Value;
			var skip = (Convert.ToInt32(value.ElementAt(1).Value) == 0 ? 0 : (Convert.ToInt32(value.ElementAt(1).Value) - 1) * 10);
			string redirect = "Graph/GetoutlookSent";
			string accesToken = await _getToken.GetTokenMGraph(code, redirect);
			ViewBag.ImageData = await ImgProfile(accesToken);
			var modelMe = await GetMeGraph(accesToken);
			var client = new HttpClient();
			var modelOutlook = new MessagesGraphDTO();
			var folderId = await GetFolferId(modelMe.Id, accesToken, "Elementos enviados");
            var request = new HttpRequestMessage(HttpMethod.Get, "https://graph.microsoft.com/v1.0/Users/" + modelMe.Id + "/mailFolders/" + folderId + "/messages?$skip=" + skip + "&count=true");
			request.Headers.Add("Authorization", "Bearer " + accesToken);
			var response = await client.SendAsync(request);
			if (response.IsSuccessStatusCode)
			{
				var responseStream = await response.Content.ReadAsStringAsync();
				modelOutlook = JsonConvert.DeserializeObject<MessagesGraphDTO>(responseStream);
				modelOutlook.GivenName = modelMe.GivenName;
				modelOutlook.JobTitle = modelMe.JobTitle;
				var array = responseStream.Split(",");
				modelOutlook.Count = Convert.ToInt32(array[1].ToString().Substring(15));
				modelOutlook.Paginas = (int)Math.Ceiling((double)modelOutlook.Count / 10);
				modelOutlook.BaseUrl = "https://login.microsoftonline.com/" + _configuration.GetSection("Azure:TenantId").Value + "/oauth2/v2.0/authorize?client_id=" + _configuration.GetSection("Azure:ClientId").Value + "&response_type=code&redirect_uri=https://localhost:7191/Graph/GetoutlookSent&response_mode=form_post&scope=user.read&state=";
				modelOutlook.PaginaActual = Convert.ToInt32(value.ElementAt(1).Value) == 0 ? 1 : Convert.ToInt32(value.ElementAt(1).Value);
                modelOutlook.Folder = "Elementos enviados";
				modelOutlook.value.ForEach(x => x.ReceivedDateTime = x.ReceivedDateTime.Substring(0, x.ReceivedDateTime.Length - 4).Replace("T", " ").Trim());
            }
            return View(modelOutlook);
		}
        #endregion

        #region teams en desarrollo
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
        #endregion

        #region Settings
        [Consumes("application/x-www-form-urlencoded")]
		public async Task<IActionResult> Settings([FromForm] IFormCollection value)
		{
            string code = value.First().Value;
            string redirect = "Graph/Settings";
            string accesToken = await _getToken.GetTokenMGraph(code, redirect);
            if (accesToken.Contains("administrador del sistema"))
            {
                var mensaje = accesToken;
                return RedirectToAction("Index", "Home", routeValues: new { mensaje });
            }
            ViewBag.ImageData = await ImgProfile(accesToken);
            var modelMe = await GetMeGraph(accesToken);
			var modelSettings = _mapper.Map<SettingsGraphDTO>(modelMe);
			modelSettings.Folder = "Settings";
            ViewBag.Token = accesToken.ToString();
            return View(modelSettings);
		}
        #endregion

        #region Carpeta automatizacion
        [Consumes("application/x-www-form-urlencoded")]
		public async Task<IActionResult> GetoutlookCarpetaAutomatizacion([FromForm] IFormCollection value)
		{
            string code = value.First().Value;
            var skip = (Convert.ToInt32(value.ElementAt(1).Value) == 0 ? 0 : (Convert.ToInt32(value.ElementAt(1).Value) - 1) * 10);
            string redirect = "Graph/GetoutlookCarpetaAutomatizacion";
            string accesToken = await _getToken.GetTokenMGraph(code, redirect);
            ViewBag.ImageData = await ImgProfile(accesToken);
            var modelMe = await GetMeGraph(accesToken);
            var client = new HttpClient();
            var modelOutlook = new MessagesGraphDTO();
            var folderId = await GetFolferId(modelMe.Id, accesToken, "AutomatizacionConfival");
            var request = new HttpRequestMessage(HttpMethod.Get, "https://graph.microsoft.com/v1.0/Users/" + modelMe.Id + "/mailFolders/" + folderId + "/messages?$skip=" + skip + "&count=true");
            request.Headers.Add("Authorization", "Bearer " + accesToken);
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStringAsync();
                modelOutlook = JsonConvert.DeserializeObject<MessagesGraphDTO>(responseStream);
                modelOutlook.GivenName = modelMe.GivenName;
                modelOutlook.JobTitle = modelMe.JobTitle;
                var array = responseStream.Split(",");
                modelOutlook.Count = Convert.ToInt32(array[1].ToString().Substring(15));
                modelOutlook.Paginas = (int)Math.Ceiling((double)modelOutlook.Count / 10);
                modelOutlook.BaseUrl = "https://login.microsoftonline.com/" + _configuration.GetSection("Azure:TenantId").Value + "/oauth2/v2.0/authorize?client_id=" + _configuration.GetSection("Azure:ClientId").Value + "&response_type=code&redirect_uri=https://localhost:7191/Graph/GetoutlookCarpetaAutomatizacion&response_mode=form_post&scope=user.read&state=";
                modelOutlook.PaginaActual = Convert.ToInt32(value.ElementAt(1).Value) == 0 ? 1 : Convert.ToInt32(value.ElementAt(1).Value);
                modelOutlook.Folder = "Carpeta Automatizacion";
                modelOutlook.value.ForEach(x => x.ReceivedDateTime = x.ReceivedDateTime.Substring(0, x.ReceivedDateTime.Length - 4).Replace("T", " ").Trim());
            }
            return View(modelOutlook);
        }
        #endregion


        [HttpGet]
        public async Task<bool> RequestPermissions(string usuario, string mail, List<string> permisos, string mensaje, string hToken)
        {
            bool isSend = false;
            string PermisosString = "";
            var array = permisos.FirstOrDefault().Split(",");
            if (array.Length > 0)
            {
                foreach (var item in array)
                {
                    PermisosString = PermisosString + item.Replace('[', ' ').Replace(']', ' ').Replace('"', ' ').Trim() + ", ";
                }
            }
            var message = new BodyMessageDTO()
            {
                message = new MessageMailDTO()
                {
                    subject = "Solicitud permisos WebConfival",
                    body = new BodyDTO()
                    {
                        contentType = "HTML",
                        content = mensaje == null ? "<p> El usuario " + usuario + " con mail: " + mail + " solicita permiso para: " + PermisosString + "</p>" : "<p> El usuario " + usuario + " con mail: " + mail + " solicita permiso para: " + PermisosString + "</p><p>El usuario ha comentado lo siguiente: " + mensaje + "</p>"
                    },
                    toRecipients = new List<RecipientDTO>()
                    {
                        new RecipientDTO()
                        {
                            emailAddress = new EmailAddressDTO()
                            {
                                address = "automatizacion@confival.com"
                            }
                        }
                    }
                }
            };
            isSend = await SendMail(hToken, message);
            if (isSend)
            {
                return true;
            }
            else
            {
                return false;
            }
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

        [HttpGet]
        public async Task<bool> SendMail(string TokenGraph, BodyMessageDTO correo)
        {
            var Http = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://graph.microsoft.com/v1.0/me/sendMail");
            request.Headers.Add("Authorization", "Bearer " + TokenGraph);
            var content = new StringContent(JsonConvert.SerializeObject(correo), null, "application/json");
            request.Content = content;
            var response = await Http.SendAsync(request);
            if(response.IsSuccessStatusCode)
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
