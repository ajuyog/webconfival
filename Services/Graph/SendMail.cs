using confinancia.Models.Graph;
using Newtonsoft.Json;

namespace confinancia.Services.Graph
{
    public interface ISendMail
    {
        Task<bool> Send(string TokenGraph, BodyMessageDTO correo);
    }
    public class SendMail: ISendMail
    {
        public async Task<bool> Send(string TokenGraph, BodyMessageDTO correo)
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
