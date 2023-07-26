using frontend.Models;
using frontend.Models.JsonDTO;
using frontend.Services.Token;
using Newtonsoft.Json;
using Tavis.UriTemplates;

namespace frontend.Services.Comentarios
{
    public interface IComentariosServices
    {
		Task<bool> ApproveComment(int id, int idBlog, bool state);
		Task<bool> Create(int idBlog, string comentario, string relation);
		Task<bool> DiscardComment(int id, int idBlog);
		Task<ComentariosDTO> Get(int idBlog, int pagina, int registros);
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

        public async Task <ComentariosDTO> Get(int idBlog, int pagina, int registros)
        {
            var model = new ComentariosDTO();
            var client = new HttpClient();
            var token = await _getToken.GetTokenV();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://api2valuezbpm.azurewebsites.net/api/blog/" + idBlog + "/comentarios/listFalse?Pagina=" + pagina + "&RegistrosPorPagina=" + registros);
            request.Headers.Add("Authorization", "Bearer " + token);
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode) 
            {
                var responseStream = await response.Content.ReadAsStringAsync();
                model = JsonConvert.DeserializeObject<ComentariosDTO>(responseStream);
            }
			return model;

        }

        public async Task<bool> Create(int idBlog, string comentario, string relation)
        {
			var result = false;
			var token = await _getToken.GetTokenV();
			var client = new HttpClient();
			var obj = new CreateCommentDTO();
			obj.Comentario = comentario;
			var json = JsonConvert.SerializeObject(obj);
			var request = new HttpRequestMessage(HttpMethod.Post, "https://api2valuezbpm.azurewebsites.net/api/blog/" + idBlog + "/comentarios/" + relation);
			request.Headers.Add("Authorization", "Bearer " + token);
			var content = new StringContent(json, null, "application/json");
			request.Content = content;
			var response = await client.SendAsync(request);
			if(response.IsSuccessStatusCode) { result = true; }
			return result;
		}

		public async Task<bool> ApproveComment(int id, int idBlog, bool state)
        {
            var result = false;
			if(state == false) 
			{
				var approve = await Patch(id, idBlog, "estado", state);
				if (approve == false) { return result; }
			}
			if(state == true)
			{
				var approve = await Patch(id, idBlog, "estado", state);
				if(approve == false) { return result; }
				var revised = await Patch(id, idBlog, "revisado", state);
				if(revised == false) { return result; }
			}
            result = true;
			return result;
		}

		public async Task<bool> DiscardComment(int id, int idBlog)
		{
			var result = false;
			var disapprove = await Patch(id, idBlog, "revisado", true);
			if (disapprove == false) { return result; }
			result = true;
			return result;
		}


		public async Task<bool> Patch(int id, int idBlog, string attribute, bool state)
        {
            var result = false;
			var client = new HttpClient();
			var token = await _getToken.GetTokenV();
			var lst = new List<PatchComentarioDTO>();
			var obj = new PatchComentarioDTO()
			{
				Op = "replace",
				Path = "/" + attribute,
				Value = state
			};
			lst.Add(obj);
			var json = JsonConvert.SerializeObject(lst);
			var request = new HttpRequestMessage(HttpMethod.Patch, "https://api2valuezbpm.azurewebsites.net/api/blog/" + idBlog + "/comentarios/" + id);
			request.Headers.Add("Authorization", "Bearer " + token);
			var content = new StringContent(json, null, "application/json");
			request.Content = content;
			var response = await client.SendAsync(request);
			if (response.IsSuccessStatusCode) { result = true; }
            return result;
		}



	}
}
