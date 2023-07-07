using Azure;
using frontend.Models;
using frontend.Models.JsonDTO;
using frontend.Services.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Reflection;
using System.Reflection.Metadata;
using Tavis.UriTemplates;

namespace noa.Controllers;

public class BlogController : Controller
{
    #region CONSTRUCTOR
    private readonly IGetToken _getToken;
    private readonly IConfiguration _configuration;

    public BlogController(IGetToken getToken, IConfiguration configuration)
    {
        _getToken = getToken;
        _configuration = configuration;
    }
    #endregion

    /// <summary>
    /// Devuelve la vista de index o principal de Blog
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Index(string pagina)
    {
        if (pagina == null)
        {
            pagina = "1";
        };
        var model = new List<BlogDTO>();
        var token = await _getToken.GetTokenV();
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, "https://api2valuezbpm.azurewebsites.net/api/blog?Pagina=" + pagina +"&RegistrosPorPagina=6");
        request.Headers.Add("Authorization", "Bearer " + token);
        var response = await client.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            var responseStream = await response.Content.ReadAsStringAsync();
            model = JsonConvert.DeserializeObject<List<BlogDTO>>(responseStream);
        }
        if (model != null)
        {
            foreach (var item in model)
            {
                var imagenBlog = await Imagen(item.Id);
                item.Imagen = imagenBlog;
                var galeria = await Galeria(item.Id);
                item.Galeria = galeria;
            }
            // Aca va el paginador 
        }
        ViewBag.Categorias = new List<DropDownListDTO>();
        var requestCategorias = new HttpRequestMessage(HttpMethod.Get, "https://api2valuezbpm.azurewebsites.net/api/categoria/categorias");
        requestCategorias.Headers.Add("Authorization", "Bearer " + token);
        var responseCategorias = await client.SendAsync(requestCategorias);
        if (responseCategorias.IsSuccessStatusCode)
        {
            var responseStreamCategorias = await responseCategorias.Content.ReadAsStringAsync();
            ViewBag.Categorias = JsonConvert.DeserializeObject<List<DropDownListDTO>>(responseStreamCategorias);
        }
        ViewBag.H2 = "Blog principal";

        return View(model);
    }

    /// <summary>
    /// Devuele la vista con los blogs por idCategoria
    /// </summary>
    /// <param name="idCategoria"></param>
    /// <param name="nombre"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetByCategoria(int idCategoria, string nombre)
    {
        var model = new List<BlogDTO>();
        var token = await _getToken.GetTokenV();
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, "https://api2valuezbpm.azurewebsites.net/api/Blog/filtro?CategoriaId=" + idCategoria);
        request.Headers.Add("Authorization", "Bearer " + token);
        var response = await client.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            var responseStream = await response.Content.ReadAsStringAsync();
            model = JsonConvert.DeserializeObject<List<BlogDTO>>(responseStream);
        }
        if (model != null)
        {
            foreach (var item in model)
            {
                var imagenBlog = await Imagen(item.Id);
                item.Imagen = imagenBlog;
                var galeria = await Galeria(item.Id);
                item.Galeria = galeria;
            }
        }
        ViewBag.Categorias = new List<DropDownListDTO>();
        var requestCategorias = new HttpRequestMessage(HttpMethod.Get, "https://api2valuezbpm.azurewebsites.net/api/categoria/categorias");
        requestCategorias.Headers.Add("Authorization", "Bearer " + token);
        var responseCategorias = await client.SendAsync(requestCategorias);
        if (responseCategorias.IsSuccessStatusCode)
        {
            var responseStreamCategorias = await responseCategorias.Content.ReadAsStringAsync();
            ViewBag.Categorias = JsonConvert.DeserializeObject<List<DropDownListDTO>>(responseStreamCategorias);
        }
        ViewBag.H2 = nombre;
        return View("~/Views/Blog/Index.cshtml", model);
    }

    /// <summary>
    ///  Devuelve la vista de Blog por Id
    /// </summary>
    /// <param name="idBlog"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetById(int id)
    {
        var model = new BlogDTO();

        var client = new HttpClient();
        var token = await _getToken.GetTokenV();

        var request = new HttpRequestMessage(HttpMethod.Get, "https://api2valuezbpm.azurewebsites.net/api/blog/" + id.ToString());
        request.Headers.Add("Authorization", "Bearer " + token);
        var response = await client.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            var responseStream = await response.Content.ReadAsStringAsync();
            model = JsonConvert.DeserializeObject<BlogDTO>(responseStream);
        }
        if (model != null)
        {
            model.Imagen = await Imagen(model.Id);
            model.Galeria = await Galeria(model.Id);
        }
        return View(model);
    }

    /// <summary>
    /// Devuleve la vista de Crear Blogs
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var model = new List<CategoriaDTO>();
        var token = await _getToken.GetTokenV();
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, "https://api2valuezbpm.azurewebsites.net/api/Categoria/categorias");
        request.Headers.Add("Authorization", "Bearer " + token);
        var response = await client.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            var responseStream = await response.Content.ReadAsStringAsync();
            var lstCategorias = JsonConvert.DeserializeObject<List<CategoriaDTO>>(responseStream);
            model = lstCategorias.ToList();
        }
        return View(model);
    }

    /// <summary>
    /// Devuelve la lista de categorias 
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [HttpGet]
    public async Task<List<CategoriaDTO>> ConsultarCategorias(int id)
    {
        var list = new List<CategoriaDTO>();
        var token = await _getToken.GetTokenV();
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, "https://api2valuezbpm.azurewebsites.net/api/Categoria/categorias");
        request.Headers.Add("Authorization", "Bearer " + token);
        var response = await client.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            var responseStream = await response.Content.ReadAsStringAsync();
            var lstCategorias = JsonConvert.DeserializeObject<List<CategoriaDTO>>(responseStream);
            list = lstCategorias.ToList();
        }
        return list;
    }

    /// <summary>
    /// Permite crear un blog
    /// </summary>
    /// <param name="titulo"></param>
    /// <param name="contenido"></param>
    /// <param name="lstCategorias"></param>
    /// <param name="categoria"></param>
    /// <returns></returns>
    [Authorize]
    [HttpPost]
    public async Task<string> Publicar(string titulo, IFormFile imagenPrincipal, string contenido, string categoria, List<string> lstCategorias, List<IFormFile> lstGaleria)
    {
        var result = "error";
        if (contenido == null || titulo == "null" || imagenPrincipal == null)
        {
            result = "campos null";
            return result;
        }
        var Categorias = Order(categoria, lstCategorias);
        var blog = new CreateBlogDTO()
        {
            Titulo = titulo,
            Contenido = contenido,
            Categorias = Categorias
        };
        var createBlog = await CreateBlog(blog);
        if (createBlog == null) { return result; }

        var createImgPrincipal = await CreateImgPrincipal(imagenPrincipal, createBlog.Id);
        if (!createImgPrincipal) { return result; }

        var createGaleria = await CreateGaleria(lstGaleria, createBlog.Id);
        if (!createGaleria) { return result; }

        result = "success";
        return result;
    }

    /// <summary>
    /// Consulta los tres blogs mas recientes por idCategoria
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<List<BlogDTO>> TopCategoria(int id)
    {
        var model = new List<BlogDTO>();
        var token = await _getToken.GetTokenV();
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, "https://api2valuezbpm.azurewebsites.net/api/Blog/filtro?CategoriaId=" + id.ToString());
        request.Headers.Add("Authorization", "Bearer " + token);
        var response = await client.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            var responseStream = await response.Content.ReadAsStringAsync();
            model = JsonConvert.DeserializeObject<List<BlogDTO>>(responseStream);
        }
        if(model != null)
        {
            foreach (var item in model)
            {
                var imagenBlog = await Imagen(item.Id);
                item.Imagen = imagenBlog;
                var galeria = await Galeria(item.Id);
                item.Galeria = galeria;
            }
        }
        return model;
    }

    // Servicios //
    // Crear Blog
    [HttpGet]
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

    [HttpPost]
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

    [HttpGet]
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

    [HttpGet]
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

    [HttpGet]
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

    // Servicios //
    // Imagen

    [HttpPost]
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

    [HttpGet]
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

    [HttpGet]
    public async Task<BlogDTO> SeeGallery(int id)
    {
		var obj = new BlogDTO();

		var client = new HttpClient();
		var token = await _getToken.GetTokenV();

		var request = new HttpRequestMessage(HttpMethod.Get, "https://api2valuezbpm.azurewebsites.net/api/blog/" + id.ToString());
		request.Headers.Add("Authorization", "Bearer " + token);
		var response = await client.SendAsync(request);
		if (response.IsSuccessStatusCode)
		{
			var responseStream = await response.Content.ReadAsStringAsync();
			obj = JsonConvert.DeserializeObject<BlogDTO>(responseStream);
		}
		if (obj != null)
		{
			obj.Imagen = await Imagen(obj.Id);
			obj.Galeria = await Galeria(obj.Id);
		}
        if(obj == null)
        {
            obj.Galeria = new List<string>();
            obj.Galeria.Add("/assets/images/photos/blogmain2.jpg");
			obj.Galeria.Add("/assets/images/photos/blogmain2.jpg");
			obj.Galeria.Add("/assets/images/photos/blogmain2.jpg");
			obj.Galeria.Add("/assets/images/photos/blogmain2.jpg");
		}
		return obj;
	}
}
