using confinancia.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace confinancia.Controllers
{
    public class DocumentacionController : Controller
    {

        [Route("/Documentacion")]
        [HttpGet]
        public IActionResult Index(int id)
        {
            return View(id);
        }

        [HttpPost]
        public async Task<IActionResult> Index()
        {
            string token = "";
            string tokenFinal = "";
            Stream streamPDF = null;

            var httpClient = new HttpClient();
            Login oLogin = new Login();
            oLogin.id = "1fd17bc3-8370-4875-9e6b-7c7cf98e52b1";
            oLogin.email = "automatizacion@confival.com";
            oLogin.password = "aaa";

            var response = await httpClient.PostAsJsonAsync("https://api2valuezbpm.azurewebsites.net/api/cuentas/inicioSesion?secret=44c4ec5dec97a44efa4ade06f7eb4b27030ffc980c5d6960c333c4fa5581734f", oLogin);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                string[] splitResponse = content.Split(',');
                string[] splitToken = splitResponse[0].Split(":");

                tokenFinal = splitToken[1];
                token = tokenFinal.Replace("\"", "");
            }
            var idS = HttpContext.Request.Form["id"];
            var category = HttpContext.Request.Form["categoria"];
            var subcategory = HttpContext.Request.Form["subcategoria"];
            var archivo = HttpContext.Request.Form.Files.First();
            int id = int.Parse(idS);
            MultipartFormDataContent form = new MultipartFormDataContent();
            form.Add(new StringContent(idS), "codArchivo");

            streamPDF = archivo.OpenReadStream();
            if (streamPDF != null)
            {
                var contentPDF = new StreamContent(streamPDF);
                contentPDF.Headers.ContentType = MediaTypeHeaderValue.Parse(archivo.ContentType);
                //Console.WriteLine(file.Name);
                form.Add(contentPDF, "UrlSoporte", archivo.Name);
            }
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            response = await httpClient.PostAsync("https://api2valuezbpm.azurewebsites.net/api/repositorioArchivo?Agrupacion1=NOAConfinancia&Agrupacion2=leads&Agrupacion3=DocumentoSolicitud&Agrupacion4=TipoDocumento&Agrupacion5=PreAnalisis", form);


            if (response.IsSuccessStatusCode)
            {
                ViewBag.Mensaje = string.Format("{0} subido correctamente", archivo.Name);

            }
            else
            {
                ViewBag.Mensaje = string.Format("{0}. Error en la carga", archivo.Name);

            }




            return View(id);
            // ViewBag.Message += string.Format("<b>{0}</b> uploaded.<br />", fileName);

        }
    }
}
