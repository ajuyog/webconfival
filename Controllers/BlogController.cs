using Azure;
using frontend.Models;
using frontend.Models.JsonDTO;
using frontend.Services.Blogs;
using frontend.Services.Categorias;
using frontend.Services.Graph;
using frontend.Services.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using Newtonsoft.Json;
using System.Collections;
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
    private readonly IGraphServices _graphServices;
    private readonly IBlogServices _blogServices;
    private readonly ICategoriasServices _categoriasServices;

    public BlogController(IGetToken getToken, IConfiguration configuration, IGraphServices graphServices, IBlogServices blogServices, ICategoriasServices categoriasServices)
    {
        _getToken = getToken;
        _configuration = configuration;
        _graphServices = graphServices;
        _blogServices = blogServices;
        _categoriasServices = categoriasServices;
    }
    #endregion

    /// <summary>
    /// Devuelve la vista de index o principal de Blog
    /// </summary>
    /// <returns></returns>
    [Route("/Blog")]
    [Route("/Blog/Index/{pagina}")]
    [HttpGet]
    public async Task<IActionResult> Index(int pagina, int registros)
    {
        if (pagina == 0) { pagina = 1; }
        registros = 10;
        var model = await _blogServices.Get(pagina, registros);
        if (model != null)
        {
            foreach (var item in model.ResultBlog)
            {
                var imagenBlog = await _blogServices.Imagen(item.Id);
                item.Imagen = imagenBlog;
                var galeria = await _blogServices.Galeria(item.Id);
                item.Galeria = galeria;
            }
            model.Count = model.TotalBlog;
            model.Paginas = (int)Math.Ceiling((double)model.Count / registros);
            model.BaseUrl = _configuration["LandingPage:RedirectGraph:https"] + "Blog/Index/";
            model.PaginaActual = pagina;
        }
        ViewBag.Categorias = await _categoriasServices.Get(1, 100, false);
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
    public async Task<IActionResult> GetByCategoria(int idCategoria, int pagina, int registros )
    {
        if (pagina == 0) { pagina = 1; }
        registros = 10;
        var model = await _blogServices.GetByCategoria(idCategoria, pagina, registros);
        if (model != null)
        {
            foreach (var item in model.ResultBlog)
            {
                var imagenBlog = await _blogServices.Imagen(item.Id);
                item.Imagen = imagenBlog;
                var galeria = await _blogServices.Galeria(item.Id);
                item.Galeria = galeria;
            }
            model.Count = model.TotalBlog;
            model.Paginas = (int)Math.Ceiling((double)model.Count / registros);
            model.BaseUrl = _configuration["LandingPage:RedirectGraph:https"] + "Blog/GetByCategoria?idCategoria=" + idCategoria + "&pagina=";
            model.PaginaActual = pagina;
        }
		ViewBag.Categorias = await _categoriasServices.Get(1, 0, false);
        ViewBag.H2 = "Blogs de categoria: " + model.ResultBlog[0].Categorias[0].Nombre;
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
        var model = await _blogServices.GetById(id);
        if (model != null)
        {
            model.Imagen = await _blogServices.Imagen(model.Id);
            model.Galeria = await _blogServices.Galeria(model.Id);
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
        var model = await _categoriasServices.Get(1, 100, true);
        var objToken = await _getToken.GetTokenMicrosoft();
        ViewBag.Imagen = await _graphServices.ImgProfile(objToken.access_token);
        var me = await _graphServices.GetMeGraph(objToken.access_token);
        ViewBag.user = me.DisplayName;
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
        var lst = await _categoriasServices.Get(1, 0, true);
        var lstResult = new List<CategoriaDTO>();
        foreach(var item in  lst)
        {
            if (item.Id != id)
            {
                lstResult.Add(item);
            }
        }
        return lstResult;
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
        var Categorias = _blogServices.Order(categoria, lstCategorias);
        var blog = new CreateBlogDTO()
        {
            Titulo = titulo,
            Contenido = contenido,
            Categorias = Categorias
        };
        var createBlog = await _blogServices.CreateBlog(blog);
        if (createBlog == null) { return result; }

        var createImgPrincipal = await _blogServices.CreateImgPrincipal(imagenPrincipal, createBlog.Id);
        if (!createImgPrincipal) { return result; }

        var createGaleria = await _blogServices.CreateGaleria(lstGaleria, createBlog.Id);
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
        var model = await _blogServices.GetByCategoria(id, 1, 3);
        if (model != null)
        {
            foreach (var item in model.ResultBlog)
            {
                var imagenBlog = await _blogServices.Imagen(item.Id);
                item.Imagen = imagenBlog;
            }
        }
        return model.ResultBlog;
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
            obj.Imagen = await _blogServices.Imagen(obj.Id);
            obj.Galeria = await _blogServices.Galeria(obj.Id);
        }
        if (obj == null)
        {
            obj.Galeria = new List<string>();
            obj.Galeria.Add("/assets/images/photos/blogmain2.jpg");
            obj.Galeria.Add("/assets/images/photos/blogmain2.jpg");
            obj.Galeria.Add("/assets/images/photos/blogmain2.jpg");
            obj.Galeria.Add("/assets/images/photos/blogmain2.jpg");
        }
        return obj;
    }

    /// <summary>
    /// Devuelve la vista para Editar los Blogs
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Edit(int pagina, int registros)
    {
        if (pagina == 0) { pagina = 1; registros = 10; }
        var model = await _blogServices.Get(pagina, registros);
		model.Count = model.TotalBlog;
		model.Paginas = (int)Math.Ceiling((double)model.Count / 10);
		model.BaseUrl = _configuration["LandingPage:RedirectGraph:https"] + "Blog/Edit?pagina=";
		model.PaginaActual = pagina;
		return View(model);
    }
}
