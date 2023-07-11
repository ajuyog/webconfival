using frontend.Models.Graph;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace frontend.Services.Graph
{
    public interface IGraphServices
    {
        Task<string> GetFolferId(string meId, string token, string folderName);
        Task<MeGraphDTO> GetMeGraph(string token);
        Task<string> GetMessages(string meId, string folderId, int pagina, string token);
        Task<string> ImgProfile(string token);
        Task<bool> SendMail(string TokenGraph, BodyMessageDTO correo);
    }
    public class GraphServices: IGraphServices
    {
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

        public async Task<string> GetMessages(string meId, string folderId, int pagina, string token)
        {
            var client = new HttpClient();
            var responseString = "";
            var request = new HttpRequestMessage(HttpMethod.Get, "https://graph.microsoft.com/v1.0/Users/" + meId + "/mailFolders/" + folderId + "/messages?$skip=" + ((pagina * 10) - 10) + "&count=true");
            request.Headers.Add("Authorization", "Bearer " + token);
            var content = new StringContent(string.Empty);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            request.Content = content;
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                responseString = await response.Content.ReadAsStringAsync();
                return responseString;
            }
            return responseString;
        }

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

    }

}
