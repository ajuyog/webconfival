using AutoMapper;
using Azure;
using frontend.Models.Graph;
using frontend.Services.Graph;
using frontend.Services.Token;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;


namespace frontend.Controllers
{
    public class GraphController : Controller
    {
        #region CONSTRUCTOR
        private readonly IGetToken _getToken;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IGraphServices _graphServices;

        public GraphController(IGetToken getToken, IConfiguration configuration, IMapper mapper, IGraphServices graphServices)
        {
            _getToken = getToken;
            _configuration = configuration;
            _mapper = mapper;
            _graphServices = graphServices;
        }
        #endregion

        /// <summary>
        /// Permite obtener los correos segun carpeta y numero de pagina
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="pagina"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetOutlook(string folder, int pagina)
        {
            var mensaje = "";
            var objToken = await _getToken.GetTokenMicrosoft();
            if (objToken == null)
            {
                mensaje = "La sesion se ha cerrado por inactividad, por favor ingresa nuevamente";
                return RedirectToAction("Index", "LandingPage", routeValues: new { mensaje });
            }
            ViewBag.ImageData = await _graphServices.ImgProfile(objToken.access_token);
            var modelMe = await _graphServices.GetMeGraph(objToken.access_token);
            if (modelMe == null)
            {
                mensaje = "Los servicios de Microsoft Grap estan presentando fallos, por favor intenta nuevamente";
                return RedirectToAction("Index", "Home", routeValues: new { mensaje });
            }
            var folderId = await _graphServices.GetFolferId(modelMe.Id, objToken.access_token, folder);
            if (folderId == "")
            {
                mensaje = "Los servicios de Microsoft Grap estan presentando fallos, por favor intenta nuevamente";
                return RedirectToAction("Index", "Home", routeValues: new { mensaje });
            }

            var json = await _graphServices.GetMessages(modelMe.Id, folderId, pagina, objToken.access_token);
            if (json == "")
            {
                mensaje = "Los servicios de Microsoft Grap estan presentando fallos, por favor intenta nuevamente";
                return RedirectToAction("Index", "Home", routeValues: new { mensaje });
            }
            var model = JsonConvert.DeserializeObject<MessagesGraphDTO>(json);
            var array = json.Split(",");
            model.GivenName = modelMe.GivenName;
            model.JobTitle = modelMe.JobTitle;
            model.Count = Convert.ToInt32(array[1].ToString().Substring(15));
            model.Paginas = (int)Math.Ceiling((double)model.Count / 10);
            model.BaseUrl = _configuration["LandingPage:RedirectGraph:https"] + "Graph/Getoutlook?folder=" + folder + "&pagina=";
            model.PaginaActual = pagina;
            model.Folder = folder;
            model.Entorno = _configuration["LandingPage:RedirectGraph:https"];
            model.value.ForEach(x => x.ReceivedDateTime = x.ReceivedDateTime.Substring(0, x.ReceivedDateTime.Length - 4).Replace("T", " ").Trim());
            return View(model);
        }


        #region teams en desarrollo
		[Consumes("application/x-www-form-urlencoded")]
        [Route("/Graph/GetTeams")]
        public async Task<IActionResult> GetTeams([FromForm] IFormCollection value)
        {
            string code = value.First().Value;
            string redirect = "Graph/GetTeams";
            var accesToken = await _getToken.GetTokenMicrosoft();
            var mensaje = "";
            if (accesToken.access_token == "AADSTS54005")
            {
                mensaje = "El codigo de autorizacion ha exprirado por favor ingresa nuevamente";
                return RedirectToAction("Index", "Home", routeValues: new { mensaje });
            }
            if (accesToken.access_token == "AADSTS65001")
            {
                mensaje = "Su perfil actualmente no tiene permisos para acceder a este recurso, comuniquese con el administrador del sistema";
                return RedirectToAction("Index", "Home", routeValues: new { mensaje });
            }
            var modelMe = await GetMeGraph(accesToken.access_token);
            var modelCalendar = new CalendarGraphDTO();
            modelCalendar.GivenName = modelMe.GivenName;
            modelCalendar.JobTitle = modelMe.JobTitle;
            modelCalendar.Folder = "Calendar";
            ViewBag.ImageData = await ImgProfile(accesToken.access_token);
            
            return View(modelCalendar);
        }
        #endregion

        #region Settings
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> Settings([FromForm] IFormCollection value)
        {
            string code = value.First().Value;
            string redirect = "Graph/Settings";
            var accesToken = await _getToken.GetTokenMicrosoft();
            var mensaje = "";
            if (accesToken.access_token == "AADSTS54005")
            {
                mensaje = "El codigo de autorizacion ha exprirado por favor ingresa nuevamente";
                return RedirectToAction("Index", "Home", routeValues: new { mensaje });
            }
            if (accesToken.access_token == "AADSTS65001")
            {
                mensaje = "Su perfil actualmente no tiene permisos para acceder a este recurso, comuniquese con el administrador del sistema";
                return RedirectToAction("Index", "Home", routeValues: new { mensaje });
            }
            ViewBag.ImageData = await ImgProfile(accesToken.access_token);

            byte[] IV = new byte[Convert.ToInt32(_configuration.GetSection("CalendarGraph:IV").Value)];

            var modelMe = await GetMeGraph(accesToken.access_token);
            var modelSettings = _mapper.Map<SettingsGraphDTO>(modelMe);
            modelSettings.Folder = "Settings";
            modelSettings.Entorno = _configuration.GetSection("LandingPage:RedirectGraph:https").Value;
            return View(modelSettings);
        }
        #endregion

        #region GetEventosCalendar
        [HttpGet]
        public async Task<JsonResult> GetEventosCalendar()
        {
            var token = await _getToken.GetTokenMicrosoft();
            var modelCalendar = new CalendarGraphDTO();
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://graph.microsoft.com/v1.0/me/events?$select=subject,body,bodyPreview,organizer,attendees,start,end,location&$skip=0");
            request.Headers.Add("Authorization", "Bearer " + token.access_token);
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStringAsync();
                modelCalendar = JsonConvert.DeserializeObject<CalendarGraphDTO>(responseStream);
                var jsonEventosCalendar = modelCalendar.Value.Select(x => new EventoCalendarioDTO()
                {
                    Title = x.Subject.ToString(),
                    Start = x.Start.DateTime,
                    End = x.End.DateTime,
                    Color = null
                });
                return Json(jsonEventosCalendar);
            }
            var jsonEventosCalendarEmpty = modelCalendar.Value.Select(x => new EventoCalendarioDTO()
            {
                Title = x.Subject.ToString(),
                Start = x.Start.DateTime,
                End = x.End.DateTime,
                Color = null
            });
            return Json(jsonEventosCalendarEmpty);
        }

        [HttpGet]
        public async Task<JsonResult> GetEventByDateTime(DateTime date)
        {
            var fechaInicio = date.ToString("o").Substring(0, 16);
            var fechaFin = fechaInicio.Replace("00:00", "23:59");
            var result = new CalendarGraphDTO();
           
            var token = await _getToken.GetTokenMicrosoft();
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://graph.microsoft.com/v1.0/me/events?$filter=start/dateTime ge '" + fechaInicio + "' and end/dateTime le '" + fechaFin + "'");
            request.Headers.Add("Authorization", "Bearer " + token.access_token);
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<CalendarGraphDTO>(responseStream);
                return Json(result.Value);
            }
            return Json(null);


        }
        #endregion

        [HttpGet]
        public async Task<bool> RequestPermissions(string usuario, string mail, List<string> permisos, string mensaje)
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
            var token = await _getToken.GetTokenMicrosoft();
            isSend = await _graphServices.SendMail(token.access_token, message);

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
        public async Task<bool> SendMail(string TokenGraph, BodyMessageDTO correo)
        {
            var Http = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://graph.microsoft.com/v1.0/me/sendMail");
            request.Headers.Add("Authorization", "Bearer " + TokenGraph);
            var content = new StringContent(JsonConvert.SerializeObject(correo), null, "application/json");
            request.Content = content;
            var response = await Http.SendAsync(request);
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
