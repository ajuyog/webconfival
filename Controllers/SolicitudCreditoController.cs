using confinancia.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace confinancia.Controllers
{
    public class SolicitudCreditoController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync("https://apilead.azurewebsites.net/api/solicitudCredito");
            
             List<SolicitudCreditoDTO> allEP = JsonConvert.DeserializeObject<List<SolicitudCreditoDTO>>(json);
            return View(allEP);
        }
    }
}
