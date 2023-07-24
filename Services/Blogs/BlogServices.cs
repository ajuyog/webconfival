using frontend.Models;
using frontend.Models.JsonDTO;
using frontend.Services.Token;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;

namespace frontend.Services.Blogs
{
    public interface IBlogServices
    {
        Task<BlogsDTO> Get(bool admin, int pagina, int registros);
        Task<BlogDTO> CreateBlog(CreateBlogDTO obj);
        Task<bool> CreateGaleria(List<IFormFile> files, int id);
        Task CreateImgAutor();
        Task<bool> CreateImgPrincipal(IFormFile img, int id);
        Task<bool> CreateItemGaleria(IFormFile img, int id);
        Task<List<string>> Galeria(int id);
        Task<string> Imagen(int id);
        string Order(string categoria, List<string> lstCategorias);
		Task<BlogsDTO> GetByCategoria(int idCategoria, int pagina, int registros);
		Task<BlogDTO> GetById(int id);
		Task<bool> Approve(int id);
    }
    public class BlogServices: IBlogServices
    {
        #region CONSTRUCTOR
        private readonly IConfiguration _configuration;
        private readonly IGetToken _getToken;

        public BlogServices(IConfiguration configuration, IGetToken getToken)
        {
            _configuration = configuration;
            _getToken = getToken;
        }
        #endregion


        public string Order(string categoria, List<string> lstCategorias)
        {
            var inicio = "[";
            var fin = "]";
            var categoriasPost = "";
            var lst = lstCategorias.FirstOrDefault().Split(",");
            if (lst[0] == "[]")
            {
                categoriasPost = inicio + categoria + fin;
            }
            else
            {
                foreach (var item in lst)
                {
                    var valor = Convert.ToInt32(item.Replace('[', ' ').Replace(']', ' ').Replace('"', ' ').Trim());
                    categoriasPost = categoriasPost + valor + ",";
                }
                categoriasPost = categoriasPost.Remove(categoriasPost.Length - 1);
                categoriasPost = inicio + categoriasPost + fin;
            }
            return categoriasPost;
        }

        public async Task<BlogDTO> CreateBlog(CreateBlogDTO obj)
        {
            var blog = new BlogDTO();
            var token = await _getToken.GetTokenV();
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api2valuezbpm.azurewebsites.net/api/blog/");
            request.Headers.Add("Authorization", "Bearer " + token);
            var content = new MultipartFormDataContent();
            content.Add(new StringContent(obj.Titulo), "Titulo");
            content.Add(new StringContent(obj.Contenido), "Contenido");
            content.Add(new StringContent(obj.Categorias), "CategoriaId");
            request.Content = content;
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStringAsync();
                blog = JsonConvert.DeserializeObject<BlogDTO>(responseStream);
            }
            return blog;
        }

        public async Task<bool> CreateImgPrincipal(IFormFile img, int id)
        {
            var token = await _getToken.GetTokenV();
            var client = new HttpClient();
            MultipartFormDataContent form = new MultipartFormDataContent();
            form.Add(new StringContent(id.ToString()), "codArchivo");
            Stream streamPDF = img.OpenReadStream();
            if (streamPDF != null)
            {
                var contentPDF = new StreamContent(streamPDF);
                contentPDF.Headers.ContentType = MediaTypeHeaderValue.Parse(img.ContentType);
                form.Add(contentPDF, "UrlSoporte", img.Name);
            }
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await client.PostAsync("https://api2valuezbpm.azurewebsites.net/api/archivo?EmpresaId=" + _configuration.GetSection("LandingPage:Blogs:Empresa").Value + "&ProyectoId=" + _configuration.GetSection("LandingPage:Blogs:Proyecto").Value + "&Agrupacion=" + _configuration.GetSection("LandingPage:Blogs:Agrupacion").Value + "&ArchivoCategoriaId=" + _configuration.GetSection("LandingPage:Blogs:Categoria").Value + "&ArchivoSubcategoriaId=" + _configuration.GetSection("LandingPage:Blogs:SubCategoria:ImagenTop").Value, form);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                var responseStreamError = await response.Content.ReadAsStringAsync();
                return false;
            }
        }

        public async Task<bool> CreateGaleria(List<IFormFile> files, int id)
        {
            var result = false;
            foreach (var item in files)
            {
                var create = await CreateItemGaleria(item, id);
                if (!create) { return result; }
            }
            result = true;
            return result;
        }

        public async Task<bool> CreateItemGaleria(IFormFile img, int id)
        {
            var token = await _getToken.GetTokenV();
            var client = new HttpClient();
            MultipartFormDataContent form = new MultipartFormDataContent();
            form.Add(new StringContent(id.ToString()), "codArchivo");
            Stream streamPDF = img.OpenReadStream();
            if (streamPDF != null)
            {
                var contentPDF = new StreamContent(streamPDF);
                contentPDF.Headers.ContentType = MediaTypeHeaderValue.Parse(img.ContentType);
                form.Add(contentPDF, "UrlSoporte", img.Name);
            }
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await client.PostAsync("https://api2valuezbpm.azurewebsites.net/api/archivo?EmpresaId=" + _configuration.GetSection("LandingPage:Blogs:Empresa").Value + "&ProyectoId=" + _configuration.GetSection("LandingPage:Blogs:Proyecto").Value + "&Agrupacion=" + _configuration.GetSection("LandingPage:Blogs:Agrupacion").Value + "&ArchivoCategoriaId=" + _configuration.GetSection("LandingPage:Blogs:Categoria").Value + "&ArchivoSubcategoriaId=" + _configuration.GetSection("LandingPage:Blogs:SubCategoria:Galeria").Value, form);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                var responseStreamError = await response.Content.ReadAsStringAsync();
                return false;
            }
        }

        public async Task CreateImgAutor()
        {
            var token = await _getToken.GetTokenMicrosoft();


            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://graph.microsoft.com/v1.0/me/photo/$value");
            request.Headers.Add("Authorization", "Bearer " + token);
            var response = await client.SendAsync(request);
            var responseStream = await response.Content.ReadAsStreamAsync();
            MemoryStream ms = new MemoryStream();
            responseStream.CopyTo(ms);
            byte[] buffer = ms.ToArray();

            var stream = new MemoryStream(buffer);
            IFormFile file = new FormFile(stream, 0, buffer.Length, "name", "imagen.png")
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/png"
            };
            await CreateItemGaleria(file, 1);

        }

        public async Task<string> Imagen(int id)
        {
            var url = "";
            var token = await _getToken.GetTokenV();
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://api2valuezbpm.azurewebsites.net/api/archivo/empresaProyectoArchivoSubCategoria?Pagina=1&RegistrosPorPagina=100&EmpresaId=" + _configuration.GetSection("LandingPage:Blogs:Empresa").Value + "&ProyectoId=" + _configuration.GetSection("LandingPage:Blogs:Proyecto").Value + "&Agrupacion=" + _configuration.GetSection("LandingPage:Blogs:Agrupacion").Value + "&ArchivoSubcategoriaId=" + _configuration.GetSection("LandingPage:Blogs:SubCategoria:ImagenTop").Value + "&OrigenId=" + id);
            request.Headers.Add("Authorization", "Bearer " + token);
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStringAsync();
                var lstImagenes = JsonConvert.DeserializeObject<List<StorageDTO>>(responseStream);
                url = lstImagenes.Count == 0 ? "/assets/images/photos/blogmain2.jpg" : lstImagenes[0].URLSoporte;
            }
            else
            {
                url = "https://storageaccountisaac.blob.core.windows.net/apivaluezdocumental/1/2/imagenes/1/4/13/76ddcce3-05f8-4871-9811-760cf10e1d38";
            }
            return url;
        }

        public async Task<List<string>> Galeria(int id)
        {
            var url = new List<string>();
            var token = await _getToken.GetTokenV();
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://api2valuezbpm.azurewebsites.net/api/archivo/empresaProyectoArchivoSubCategoria?Pagina=1&RegistrosPorPagina=100&EmpresaId=" + _configuration.GetSection("LandingPage:Blogs:Empresa").Value + "&ProyectoId=" + _configuration.GetSection("LandingPage:Blogs:Proyecto").Value + "&Agrupacion=" + _configuration.GetSection("LandingPage:Blogs:Agrupacion").Value + "&ArchivoSubcategoriaId=" + _configuration.GetSection("LandingPage:Blogs:SubCategoria:Galeria").Value + "&OrigenId=" + id);
            request.Headers.Add("Authorization", "Bearer " + token);
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStringAsync();
                var lstImagenes = JsonConvert.DeserializeObject<List<StorageDTO>>(responseStream);
                if (lstImagenes.Count > 0)
                {
                    foreach (var item in lstImagenes)
                    {
                        url.Add(item.URLSoporte);
                    }
                }
                else
                {
                    url.Add("/assets/images/photos/blogmain2.jpg");
                    url.Add("/assets/images/photos/blogmain2.jpg");
                    url.Add("/assets/images/photos/blogmain2.jpg");
                    url.Add("/assets/images/photos/blogmain2.jpg");
                }
            }
            else
            {
                url.Add("/assets/images/photos/blogmain2.jpg");
                url.Add("/assets/images/photos/blogmain2.jpg");
                url.Add("/assets/images/photos/blogmain2.jpg");
                url.Add("/assets/images/photos/blogmain2.jpg");
            }
            return url;
        }

        public async Task<BlogsDTO> Get(bool admin, int pagina, int registros)
        {
            var model = new BlogsDTO();
            var token = await _getToken.GetTokenV();
            var client = new HttpClient();
			var request = new HttpRequestMessage(HttpMethod.Get, "https://api2valuezbpm.azurewebsites.net/api/Blog/" + admin + "?Pagina=" + pagina + "&RegistrosPorPagina=" + registros);
			request.Headers.Add("Authorization", "Bearer " + token);
			var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStringAsync();
                model = JsonConvert.DeserializeObject<BlogsDTO>(responseStream);
            }
            return model;
        }

        public async Task<BlogsDTO> GetByCategoria(int idCategoria, int pagina, int registros)
        {
            var model = new BlogsDTO();
            var client = new HttpClient();
            var token = await _getToken.GetTokenV();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://api2valuezbpm.azurewebsites.net/api/Blog/filtro?CategoriaId=" + idCategoria + "&Pagina=" + pagina + "&RegistrosPorPagina=" + registros);
            request.Headers.Add("Authorization", "Bearer " + token);
            var response = await client.SendAsync(request);
            if(response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStringAsync();
                model = JsonConvert.DeserializeObject<BlogsDTO>(responseStream);
            }
            return model;
        }

        public async Task<BlogDTO> GetById(int id)
        {
			var model = new BlogDTO();
			var client = new HttpClient();
			var token = await _getToken.GetTokenV();
			var request = new HttpRequestMessage(HttpMethod.Get, "https://api2valuezbpm.azurewebsites.net/api/blog/" + id);
			request.Headers.Add("Authorization", "Bearer " + token);
			var response = await client.SendAsync(request);
			if (response.IsSuccessStatusCode)
			{
				var responseStream = await response.Content.ReadAsStringAsync();
				model = JsonConvert.DeserializeObject<BlogDTO>(responseStream);
			}
            return model;
		}

        public async Task<bool> Approve(int id)
        {
            var result = false;
			var lst = new List<PatchComentarioDTO>();
			var obj = new PatchComentarioDTO()
			{
				Op = "replace",
				Path = "/estado",
				Value = true
			};
			lst.Add(obj);
			var json = JsonConvert.SerializeObject(lst);
			var token = await _getToken.GetTokenV();
			var client = new HttpClient();
			var request = new HttpRequestMessage(HttpMethod.Patch, "https://api2valuezbpm.azurewebsites.net/api/Blog/" + id);
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
