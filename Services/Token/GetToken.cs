using frontend.Models.JsonDTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Globalization;
using System.Net.Http;

namespace frontend.Services.Token
{

    public interface IGetToken
    {
		Task<string> GetTokenMGraph(string code, string redirect);
        Task<TokenGraphDTO> GetTokenMicrosoft();
        Task<string> GetTokenV();
    }

    public class GetToken: IGetToken
    {
        #region CONSTRUCTOR
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IHttpContextAccessor _contextAccessor;
        public GetToken(IConfiguration configuration, IHttpClientFactory clientFactory, IHttpContextAccessor contextAccessor)
        {
            _configuration = configuration;
            _clientFactory = clientFactory;
            _contextAccessor = contextAccessor;
        }
        #endregion

        [HttpGet]
        public async Task<string> GetTokenV()
        {
            var obj = new LoginTokenDTO()
            {
                Id = _configuration.GetSection("Variables:IdLogin").Value,
                Email = _configuration.GetSection("Variables:Email").Value,
                Password = _configuration.GetSection("Variables:Password").Value,
            };
            var json = JsonConvert.SerializeObject(obj);
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api2valuezbpm.azurewebsites.net/api/cuentas/inicioSesion?secret=" + _configuration.GetSection("Variables:Secret").Value);
            var content = new StringContent(json, null, "application/json");
            request.Content = content;
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<TokenValuezDTO>(responseStream);
                return result.Token;
            }
            else
            {
                var empty = "";
                return empty;
            }
        }

        [HttpGet]
        public async Task<string> GetTokenMGraph(string code, string redirect)
        {
            var Http = new HttpClient();
            string tokenGraph = "";
            TokenGraphDTO TG = new TokenGraphDTO();

            var request = new HttpRequestMessage(HttpMethod.Get, "https://login.microsoftonline.com/" + _configuration.GetSection("Azure:TenantId").Value + "/oauth2/v2.0/token ");
            var collection = new List<KeyValuePair<string, string>>();
            collection.Add(new("client_id", _configuration.GetSection("Azure:ClientId").Value));
            collection.Add(new("scope", "User.Read Mail.Read Calendars.Read"));
            collection.Add(new("code", code));
            collection.Add(new("redirect_uri", "https://localhost:7191/" + redirect));
            collection.Add(new("grant_type", "authorization_code"));
            collection.Add(new("client_secret", _configuration.GetSection("Azure:ClientSecret").Value));
            var content = new FormUrlEncodedContent(collection);
            request.Content = content;
            var response = await Http.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                tokenGraph = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                TG = JsonConvert.DeserializeObject<TokenGraphDTO>(tokenGraph);
            }
            else
            {
                var array = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                if (array.Contains("AADSTS65001"))
				{
					return "AADSTS65001";
				}
				if (array.Contains("AADSTS54005"))
                {
                    return "AADSTS54005";
                }
                return "Ocurrio un error, comuniquese con el administrador del sistema";
            }
            return TG.access_token;
        }

        [HttpGet]
        public async Task<TokenGraphDTO> GetTokenMicrosoft()
        {
            var token = new TokenGraphDTO()
            {
                token_type = await _contextAccessor.HttpContext.GetTokenAsync("token_type"!),
                access_token = await _contextAccessor.HttpContext.GetTokenAsync("access_token"!),
                refresh_token = await _contextAccessor.HttpContext.GetTokenAsync("refresh_token"),
                expires_in = await _contextAccessor.HttpContext.GetTokenAsync("expires_at")
            };

            if(token.access_token != null) 
            {
                var fecha = DateTime.UtcNow;
                DateTime Expires_at = DateTime.Parse(token.expires_in!).ToUniversalTime()!;
                if (fecha < Expires_at) 
                {
                    DateTime taskTime = Expires_at.AddMinutes(-40);
                    if (fecha >= taskTime)
                    {
                        await RefreshToken(token.refresh_token);
                        return token;
                    }
                }
                else
                {
                    return token;
                }
            }
            return token;
        }

        [HttpGet]
        protected async Task<TokenGraphDTO> RefreshToken(string refreshToken)
        {
            var httpClient = _clientFactory.CreateClient();
            // Configura los parámetros necesarios para la solicitud de renovación del token de acceso
            var parameters = new Dictionary<string, string>
            {
                { "grant_type", "refresh_token" },
                { "refresh_token", refreshToken! },
                { "client_id", _configuration["Azure:ClientId"]},
                { "client_secret", _configuration["Azure:ClientSecret"]},
            };
            // Realiza la solicitud al punto de extremo de renovación de token de Microsoft
            var response = await httpClient.PostAsync("https://login.microsoftonline.com/" + _configuration["Azure:TenantId"] + "/oauth2/v2.0/token", new FormUrlEncodedContent(parameters));
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var tokenResult = JsonConvert.DeserializeObject<TokenGraphDTO>(json);
                int seconds = int.Parse(tokenResult!.expires_in!); // Convertir a número entero
                DateTime referenceDateTime = DateTime.UtcNow; // Punto de referencia conocido
                DateTime resultDateTime = referenceDateTime.AddSeconds(seconds);
                //Actualiza la cookie
                var authProps = new AuthenticationProperties();
                authProps.StoreTokens(new[] {
                new AuthenticationToken { Name = "access_token",  Value = tokenResult!.access_token!},
                new AuthenticationToken { Name = "refresh_token", Value = tokenResult!.refresh_token!},
                new AuthenticationToken { Name = "token_type", Value = tokenResult!.token_type!},
                new AuthenticationToken { Name = "expires_at", Value =  resultDateTime.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffzzz", CultureInfo.InvariantCulture)},
                });
                authProps.IsPersistent = true;
                await _contextAccessor.HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, _contextAccessor.HttpContext.User, authProps);
            }
            else
            {
                var responseStream = await response.Content.ReadAsStringAsync();
                var leerError = 7;
            }
            return null!;
        }
    }
}
