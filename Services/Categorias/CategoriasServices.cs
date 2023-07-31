using frontend.Models;
using frontend.Models.JsonDTO;
using frontend.Services.Token;
using Newtonsoft.Json;

namespace frontend.Services.Categorias
{
    public interface ICategoriasServices
    {
        Task<bool> Delete(int id);
        Task<CategoriasDTO> Get(int pagina, int registros, bool perfil);
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

        public async Task<CategoriasDTO> Get(int pagina, int registros, bool perfil)
        {
            if (registros == 0) { registros = 10; }
            var model = new CategoriasDTO();
			var client = new HttpClient();
            var token = await _getToken.GetTokenV();
			var request = new HttpRequestMessage(HttpMethod.Get, "https://api2valuezbpm.azurewebsites.net/api/Categoria/" + perfil + "/categorias?Pagina=" + pagina + "&RegistrosPorPagina=" + registros);
			request.Headers.Add("Authorization", "Bearer " + token);
			var response = await client.SendAsync(request);
			if (response.IsSuccessStatusCode)
			{
				var responseStream = await response.Content.ReadAsStringAsync();
				model = JsonConvert.DeserializeObject<CategoriasDTO>(responseStream);
			}
			return model;
		}

        public async Task<bool> Delete(int id)
        {
            var result = false;
            var token = await _getToken.GetTokenV();
            var obj = new DropDownListDTO();
            obj.Id = id;
            var json = JsonConvert.SerializeObject(obj);
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Delete, "https://api2valuezbpm.azurewebsites.net/api/Categoria/" + id);
            request.Headers.Add("Authorization", "Bearer " + token);
            var content = new StringContent(json, null, "application/json");
            request.Content = content;
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                result = true;
            }
            return result;
        }
	}
}
