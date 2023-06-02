using confinancia.Models.JsonDTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace confinancia.Services.Token
{
    public class GetToken
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


    }
}
