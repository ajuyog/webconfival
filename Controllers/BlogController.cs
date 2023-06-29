using confinancia.Models;
using confinancia.Models.JsonDTO;
using confinancia.Services.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Reflection;

namespace noa.Controllers;

public class BlogController : Controller
{
    #region CONSTRUCTOR
    private readonly IGetToken _getToken;
    public BlogController(IGetToken getToken)
    {
        _getToken = getToken;
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
	public async Task<bool> Publicar(string titulo, IFormFile imagenPrincipal, string contenido, string categoria, List<string> lstCategorias, List<IFormFile> lstGaleria)
	{
		var array = lstCategorias.FirstOrDefault().Split(",");
        var categoriaPrincipal = Convert.ToInt32(categoria.Replace('"', ' ').Trim());
		int[] numeros = new int[1];
		numeros[numeros.Length - 1] = categoriaPrincipal;
		var obj = new BlogDTO();
		if (lstCategorias[0] == "[]")
		{
			obj.Titulo = titulo;
			obj.Contenido = contenido;
			obj.Estado = false;
			obj.Publicacion = DateTime.Now;
			obj.Categoriass = new List<CategoriaDTO>();
            obj.Categoriass.Add(new CategoriaDTO() 
			{ 
				Id = Convert.ToInt32(categoria)
			});
		}
		else
		{
			foreach (var item in array)
			{
				var valor = Convert.ToInt32(item.Replace('[', ' ').Replace(']', ' ').Replace('"', ' ').Trim());
				Array.Resize(ref numeros, numeros.Length + 1);
				numeros[numeros.Length -1] = valor;
			}
			obj.Titulo = titulo;
			obj.Contenido = contenido;
            obj.Estado = false;
            obj.Publicacion = DateTime.Now;
            obj.Categoriass = new List<CategoriaDTO>();
            var count = 0;
            foreach (var item in numeros)
            {
				obj.Categoriass.Add(new CategoriaDTO() { Id = numeros[count] });
				count++;
            }
		}
		var json = JsonConvert.SerializeObject(obj);
		var client = new HttpClient();
		var request = new HttpRequestMessage(HttpMethod.Post, "https://apileadconfival.azurewebsites.net/api/blog");
		request.Headers.Add("XApiKey", "H^qP[7p#$18EXbV(lIP5xu+tCe-kgCM&{i_V,=(&");
		var content = new StringContent(json, null, "application/json");
		request.Content = content;
		var response = await client.SendAsync(request);
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
