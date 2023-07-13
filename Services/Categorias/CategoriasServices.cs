using frontend.Models;
using frontend.Models.JsonDTO;
using frontend.Services.Token;
using Newtonsoft.Json;

namespace frontend.Services.Categorias
{
    public interface ICategoriasServices
    {
        Task<List<DropDownListDTO>> Get(int pagina);
    }
    public class CategoriasServices: ICategoriasServices
    {
        #region CONSTRUCTOR
        private readonly IGetToken _getToken;

        public CategoriasServices(IGetToken getToken)
        {
            _getToken = getToken;
        }
        #endregion

        public async Task<List<DropDownListDTO>> Get(int pagina)
        {
            var model = new List<DropDownListDTO>();
            var client = new HttpClient();
            var token = await _getToken.GetTokenV();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://api2valuezbpm.azurewebsites.net/api/categoria/categorias");
            request.Headers.Add("Authorization", "Bearer " + token);
            var responseCategorias = await client.SendAsync(request);
            if (responseCategorias.IsSuccessStatusCode)
            {
                var responseStreamCategorias = await responseCategorias.Content.ReadAsStringAsync();
                model = JsonConvert.DeserializeObject<List<DropDownListDTO>>(responseStreamCategorias);
            }
            return model;
        }
    }
}
