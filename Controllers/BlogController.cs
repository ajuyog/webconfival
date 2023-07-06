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

	[HttpGet]
	public async Task<IActionResult> Index() 
	{
        var model = new List<BlogDTO>();
        var token = await _getToken.GetTokenV();
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, "https://api2valuezbpm.azurewebsites.net/api/blog");
        request.Headers.Add("Authorization", "Bearer " + token);
        var response = await client.SendAsync(request);
		if (response.IsSuccessStatusCode)
		{
            var responseStream = await response.Content.ReadAsStringAsync();
            model = JsonConvert.DeserializeObject<List<BlogDTO>>(responseStream);
        }
		return View(model);
	}

	/// <summary>
	///  Devuelve la vista de Blog por Id
	/// </summary>
	/// <param name="idBlog"></param>
	/// <returns></returns>
	[HttpGet]
    public async Task<IActionResult> GetById(int idBlog)
    {
		var model = new BlogDTO();

		var client = new HttpClient();
		var token = await _getToken.GetTokenV();

		var request = new HttpRequestMessage(HttpMethod.Get, "https://api2valuezbpm.azurewebsites.net/api/blog/1");
		request.Headers.Add("Authorization", "Bearer " + token);
		var response = await client.SendAsync(request);
		if(response.IsSuccessStatusCode)
		{
			var responseStream = await response.Content.ReadAsStringAsync();
			model = JsonConvert.DeserializeObject<BlogDTO>(responseStream);
		}
		return View(model);
	}

	/// <summary>
	/// Consulta los tres blogs mas recientes por idCategoria
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
    [HttpGet]
    public async Task<List<BlogBetaDTO>> ConsultarTopTres(int id)
    {
		var parametro = id.ToString();
		var client = new HttpClient();
		var request = new HttpRequestMessage(HttpMethod.Get, "https://apileadconfival.azurewebsites.net/api/blog/filtro?categoriaId=" + parametro);
		request.Headers.Add("XApiKey", "H^qP[7p#$18EXbV(lIP5xu+tCe-kgCM&{i_V,=(&");
		var content = new StringContent(string.Empty);
		content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
		request.Content = content;
		var response = await client.SendAsync(request);
		if (response.IsSuccessStatusCode)
		{
			var responseStream = await response.Content.ReadAsStringAsync();
			var lstBlogs = JsonConvert.DeserializeObject<List<BlogBetaDTO>>(responseStream);
			return lstBlogs;
		}
		else
		{
			var lstEmpy = new List<BlogBetaDTO>();
			return lstEmpy;
		}
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
        if(contenido == null || titulo == "null" || imagenPrincipal == null) 
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
        if(!createGaleria) { return result; }

        result = "success";
        return result;
    }


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
}
