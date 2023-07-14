using frontend.Models;
using frontend.Models.JsonDTO;
using frontend.Services.Token;
using Newtonsoft.Json;

namespace frontend.Services.Comentarios
{
    public interface IComentariosServices
    {
        Task<List<ComentariosDTO>> GetByStateFalse(int idBlog);
    }
    public class ComentariosServices: IComentariosServices
    {
        #region CONSTRUCTOR
        private readonly IGetToken _getToken;
        public ComentariosServices(IGetToken getToken)
        {
            _getToken = getToken;
        }
        #endregion

        public async Task <List<ComentariosDTO>> GetByStateFalse(int idBlog)
        {
            var model = new List<ComentariosDTO>();
            var client = new HttpClient();
            var token = await _getToken.GetTokenV();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://api2valuezbpm.azurewebsites.net/api/blog/" + idBlog + "/comentarios/listFalse");
            request.Headers.Add("Authorization", "Bearer " + token );
            var response = await client.SendAsync(request);
            if(response.IsSuccessStatusCode) 
            {
                var responseStream = await response.Content.ReadAsStringAsync();
                model = JsonConvert.DeserializeObject<List<ComentariosDTO>>(responseStream);
            }
            return model;

        }
    }
}
