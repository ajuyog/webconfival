using confinancia.Models.JsonDTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace confinancia.Services.Token
{

    public interface IGetToken
    {
		Task<string> GetTokenMGraph(string code, string redirect);
		Task<string> GetTokenV();
    }

    public class GetToken: IGetToken
    {
        #region CONSTRUCTOR
        private readonly IConfiguration _configuration;
        public GetToken(IConfiguration configuration)
        {
            _configuration = configuration;
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
            collection.Add(new("scope", "user.read mail.read"));
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
                if (array.Contains("error_description"))
                {
                    return "Su perfil actualmente no tiene permisos para acceder a este recurso, comuniquese con el administrador del sistema";
                }
                return "Ocurrio un error, comuniquese con el administrador del sistema";
            }
            return TG.access_token;
        }
    }
}
