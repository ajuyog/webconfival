using AutoMapper;
using confinancia.Models.Graph;
using confinancia.Services.Token;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;


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
        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> Getoutlook([FromForm] IFormCollection value)
        {
            if (value.Count == 0)
            {
                var referesh = "https://login.microsoftonline.com/4003e53b-966b-4b92-9425-eeb681bd62a5/oauth2/v2.0/authorize?client_id=57f0978d-23bc-4172-ae60-d548461c018d&response_type=code&redirect_uri=" + _configuration.GetSection("LandingPage:RedirectGraph:https").Value + "Graph/GetOutlook&response_mode=form_post&scope=user.read&state=0";
                return Redirect(referesh);
            }
            string code = value.First().Value;
            var skip = (Convert.ToInt32(value.ElementAt(1).Value) == 0 ? 0 : (Convert.ToInt32(value.ElementAt(1).Value) - 1) * 10);
            string redirect = "Graph/GetOutlook";
            string accesToken = await _getToken.GetTokenMGraph(code, redirect);
            var mensaje = "";
            if (accesToken == "AADSTS54005")
            {
                mensaje = "El codigo de autorizacion ha exprirado por favor ingresa nuevamente";
                return RedirectToAction("Index", "Home", routeValues: new { mensaje });
            }
            if (accesToken == "AADSTS65001")
            {
                mensaje = "Su perfil actualmente no tiene permisos para acceder a este recurso, comuniquese con el administrador del sistema";
                return RedirectToAction("Index", "Home", routeValues: new { mensaje });
            }
            ViewBag.ImageData = await ImgProfile(accesToken);
            //ViewBag.Entorno = _configuration.GetSection("LandingPage:RedirectGraph:https").Value;
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
            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStringAsync();
                modelOutlook = JsonConvert.DeserializeObject<MessagesGraphDTO>(responseStream);
                modelOutlook.GivenName = modelMe.GivenName;
                modelOutlook.JobTitle = modelMe.JobTitle;
                var array = responseStream.Split(",");
                modelOutlook.Count = Convert.ToInt32(array[1].ToString().Substring(15));
                modelOutlook.Paginas = (int)Math.Ceiling((double)modelOutlook.Count / 10);
                modelOutlook.BaseUrl = "https://login.microsoftonline.com/" + _configuration.GetSection("Azure:TenantId").Value + "/oauth2/v2.0/authorize?client_id=" + _configuration.GetSection("Azure:ClientId").Value + "&response_type=code&redirect_uri=" + _configuration.GetSection("LandingPage:RedirectGraph:https").Value + "Graph/GetOutlook&response_mode=form_post&scope=user.read&state=";
                modelOutlook.PaginaActual = Convert.ToInt32(value.ElementAt(1).Value) == 0 ? 1 : Convert.ToInt32(value.ElementAt(1).Value);
                modelOutlook.Folder = "Bandeja de entrada";
                modelOutlook.Entorno = _configuration.GetSection("LandingPage:RedirectGraph:https").Value;
                modelOutlook.value.ForEach(x => x.ReceivedDateTime = x.ReceivedDateTime.Substring(0, x.ReceivedDateTime.Length - 4).Replace("T", " ").Trim());

            }
            return View(modelOutlook);
        }
        #endregion

        #region Elementos enviados
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> GetoutlookSent([FromForm] IFormCollection value)
        {
            if (value.Count == 0)
            {
                var referesh = "https://login.microsoftonline.com/4003e53b-966b-4b92-9425-eeb681bd62a5/oauth2/v2.0/authorize?client_id=57f0978d-23bc-4172-ae60-d548461c018d&response_type=code&redirect_uri=" + _configuration.GetSection("LandingPage:RedirectGraph:https").Value + "Graph/GetoutlookSent&response_mode=form_post&scope=user.read&state=0";
                return Redirect(referesh);
            }
            string code = value.First().Value;
            var skip = (Convert.ToInt32(value.ElementAt(1).Value) == 0 ? 0 : (Convert.ToInt32(value.ElementAt(1).Value) - 1) * 10);
            string redirect = "Graph/GetoutlookSent";
            string accesToken = await _getToken.GetTokenMGraph(code, redirect);
            var mensaje = "";
            if (accesToken == "AADSTS54005")
            {
                mensaje = "El codigo de autorizacion ha exprirado por favor ingresa nuevamente";
                return RedirectToAction("Index", "Home", routeValues: new { mensaje });
            }
            if (accesToken == "AADSTS65001")
            {
                mensaje = "Su perfil actualmente no tiene permisos para acceder a este recurso, comuniquese con el administrador del sistema";
                return RedirectToAction("Index", "Home", routeValues: new { mensaje });
            }
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
                modelOutlook.BaseUrl = "https://login.microsoftonline.com/" + _configuration.GetSection("Azure:TenantId").Value + "/oauth2/v2.0/authorize?client_id=" + _configuration.GetSection("Azure:ClientId").Value + "&response_type=code&redirect_uri=" + _configuration.GetSection("LandingPage:RedirectGraph:https").Value + "Graph/GetoutlookSent&response_mode=form_post&scope=user.read&state=";
                modelOutlook.PaginaActual = Convert.ToInt32(value.ElementAt(1).Value) == 0 ? 1 : Convert.ToInt32(value.ElementAt(1).Value);
                modelOutlook.Folder = "Elementos enviados";
                modelOutlook.Entorno = _configuration.GetSection("LandingPage:RedirectGraph:https").Value;
                modelOutlook.value.ForEach(x => x.ReceivedDateTime = x.ReceivedDateTime.Substring(0, x.ReceivedDateTime.Length - 4).Replace("T", " ").Trim());
            }
            return View(modelOutlook);
        }
        #endregion

        #region Carpeta automatizacion
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> GetoutlookCarpetaAutomatizacion([FromForm] IFormCollection value)
        {
            if (value.Count == 0)
            {
                var referesh = "https://login.microsoftonline.com/4003e53b-966b-4b92-9425-eeb681bd62a5/oauth2/v2.0/authorize?client_id=57f0978d-23bc-4172-ae60-d548461c018d&response_type=code&redirect_uri=" + _configuration.GetSection("LandingPage:RedirectGraph:https").Value + "Graph/GetoutlookCarpetaAutomatizacion&response_mode=form_post&scope=user.read&state=0";
                return Redirect(referesh);
            }
            string code = value.First().Value;
            var skip = (Convert.ToInt32(value.ElementAt(1).Value) == 0 ? 0 : (Convert.ToInt32(value.ElementAt(1).Value) - 1) * 10);
            string redirect = "Graph/GetoutlookCarpetaAutomatizacion";
            string accesToken = await _getToken.GetTokenMGraph(code, redirect);
            var mensaje = "";
            if (accesToken == "AADSTS54005")
            {
                mensaje = "El codigo de autorizacion ha exprirado por favor ingresa nuevamente";
                return RedirectToAction("Index", "Home", routeValues: new { mensaje });
            }
            if (accesToken == "AADSTS65001")
            {
                mensaje = "Su perfil actualmente no tiene permisos para acceder a este recurso, comuniquese con el administrador del sistema";
                return RedirectToAction("Index", "Home", routeValues: new { mensaje });
            }
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
                modelOutlook.BaseUrl = "https://login.microsoftonline.com/" + _configuration.GetSection("Azure:TenantId").Value + "/oauth2/v2.0/authorize?client_id=" + _configuration.GetSection("Azure:ClientId").Value + "&response_type=code&redirect_uri=" + _configuration.GetSection("LandingPage:RedirectGraph:https").Value + "Graph/GetoutlookCarpetaAutomatizacion&response_mode=form_post&scope=user.read&state=";
                modelOutlook.PaginaActual = Convert.ToInt32(value.ElementAt(1).Value) == 0 ? 1 : Convert.ToInt32(value.ElementAt(1).Value);
                modelOutlook.Folder = "Carpeta Automatizacion";
                modelOutlook.Entorno = _configuration.GetSection("LandingPage:RedirectGraph:https").Value;
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
            var mensaje = "";
            if (accesToken == "AADSTS54005")
            {
                mensaje = "El codigo de autorizacion ha exprirado por favor ingresa nuevamente";
                return RedirectToAction("Index", "Home", routeValues: new { mensaje });
            }
            if (accesToken == "AADSTS65001")
            {
                mensaje = "Su perfil actualmente no tiene permisos para acceder a este recurso, comuniquese con el administrador del sistema";
                return RedirectToAction("Index", "Home", routeValues: new { mensaje });
            }
            var modelMe = await GetMeGraph(accesToken);
            var modelCalendar = new CalendarGraphDTO();
            modelCalendar.GivenName = modelMe.GivenName;
            modelCalendar.JobTitle = modelMe.JobTitle;
            modelCalendar.Folder = "Calendar";
            ViewBag.ImageData = await ImgProfile(accesToken);
            byte[] IV = new byte[Convert.ToInt32(_configuration.GetSection("CalendarGraph:IV").Value)];
            var tokenEncriptado = Encriptar(accesToken, _configuration.GetSection("CalendarGraph:Password").Value, IV);
            CookieOptions options = new CookieOptions()
            {
                Expires = DateTime.Now.AddHours(1)
            };
            Response.Cookies.Append(_configuration.GetSection("CalendarGraph:Name").Value, tokenEncriptado, options);
            return View(modelCalendar);
        }
        #endregion

        #region Settings
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> Settings([FromForm] IFormCollection value)
        {
            string code = value.First().Value;
            string redirect = "Graph/Settings";
            string accesToken = await _getToken.GetTokenMGraph(code, redirect);
            var mensaje = "";
            if (accesToken == "AADSTS54005")
            {
                mensaje = "El codigo de autorizacion ha exprirado por favor ingresa nuevamente";
                return RedirectToAction("Index", "Home", routeValues: new { mensaje });
            }
            if (accesToken == "AADSTS65001")
            {
                mensaje = "Su perfil actualmente no tiene permisos para acceder a este recurso, comuniquese con el administrador del sistema";
                return RedirectToAction("Index", "Home", routeValues: new { mensaje });
            }
            ViewBag.ImageData = await ImgProfile(accesToken);
            var modelMe = await GetMeGraph(accesToken);
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
            var tokenEncrypted = Request.Cookies[_configuration.GetSection("CalendarGraph:Name").Value];
            byte[] myByteArray = new byte[Convert.ToInt32(_configuration.GetSection("CalendarGraph:IV").Value)];
            var tokenDencrypted = Desencriptar(tokenEncrypted, _configuration.GetSection("CalendarGraph:Password").Value, myByteArray);

            var modelCalendar = new CalendarGraphDTO();
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://graph.microsoft.com/v1.0/me/events?$select=subject,body,bodyPreview,organizer,attendees,start,end,location&$skip=0");
            request.Headers.Add("Authorization", "Bearer " + tokenDencrypted);
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
            var tokenEncrypted = Request.Cookies[_configuration.GetSection("CalendarGraph:Name").Value];
            byte[] myByteArray = new byte[Convert.ToInt32(_configuration.GetSection("CalendarGraph:IV").Value)];
            var tokenDencrypted = Desencriptar(tokenEncrypted, _configuration.GetSection("CalendarGraph:Password").Value, myByteArray);
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://graph.microsoft.com/v1.0/me/events?$filter=start/dateTime ge '" + fechaInicio + "' and end/dateTime le '" + fechaFin + "'");
            request.Headers.Add("Authorization", "Bearer " + tokenDencrypted);
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

            var tokenEncrypted = Request.Cookies[_configuration.GetSection("CalendarGraph:Name").Value];
            byte[] myByteArray = new byte[Convert.ToInt32(_configuration.GetSection("CalendarGraph:IV").Value)];
            var tokenDencrypted = Desencriptar(tokenEncrypted, _configuration.GetSection("CalendarGraph:Password").Value, myByteArray);

            isSend = await SendMail(tokenDencrypted, message);
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
            if (response.IsSuccessStatusCode)
            {
                var responseStreamMe = await response.Content.ReadAsStringAsync();
                var lstFolders = JsonConvert.DeserializeObject<MailFoldersDTO>(responseStreamMe);
                var folder = lstFolders.Value.Where(x => x.DisplayName == folderName).FirstOrDefault();
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
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpGet]
        public string Encriptar(string token, string password, byte[] IV) 
        {
            byte[] key = Encoding.UTF8.GetBytes(password);
            var aes = Aes.Create();
            aes.Key = key;
            aes.IV = IV;

            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write);

            byte[] tokenByte = Encoding.UTF8.GetBytes(token);
            cryptoStream.Write(tokenByte, 0, tokenByte.Length);
            cryptoStream.FlushFinalBlock();

            byte[] encrypted = memoryStream.ToArray();
            return Convert.ToBase64String(encrypted);
        }

        [HttpGet]
        public string Desencriptar(string tokenEncriptado, string password, byte[] IV)
        {
            byte[] key = Encoding.UTF8.GetBytes(password);
            var aes = Aes.Create();
            aes.Key = key;
            aes.IV = IV;

            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write);

            byte[] tokenEncrypt = Convert.FromBase64String(tokenEncriptado);
            cryptoStream.Write(tokenEncrypt, 0, tokenEncrypt.Length);
            cryptoStream.FlushFinalBlock();

            byte[] Decrypted = memoryStream.ToArray();
            return Encoding.UTF8.GetString(Decrypted);
        }
    }
}
