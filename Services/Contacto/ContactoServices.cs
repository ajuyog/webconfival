using frontend.Models;
using frontend.Services.Token;
using Newtonsoft.Json;

namespace frontend.Services.Contacto
{
	public interface IContactoServices
	{
		Task<bool> CreateLead(LeadCampaniaDTO obj);
	}
	public class ContactoServices: IContactoServices
	{
		#region CONSTRUCTOR
		private readonly IGetToken _getToken;
		public ContactoServices(IGetToken getToken)
        {
			_getToken = getToken;
		}
        #endregion

        public async Task<bool> CreateLead(LeadCampaniaDTO obj)
		{
			var result = false;
			var token = await _getToken.GetTokenV();
			var json = JsonConvert.SerializeObject(obj);
			var client = new HttpClient();
			var request = new HttpRequestMessage(HttpMethod.Post, "https://api2valuezbpm.azurewebsites.net/api/LeadCampanha");
			request.Headers.Add("Authorization", "Bearer " + token);
			var content = new StringContent(json, null, "application/json");
			request.Content = content;
			var response = await client.SendAsync(request);
			if(response.IsSuccessStatusCode) 
			{
				result = true;
			}
			return result;
		}
	}
}
