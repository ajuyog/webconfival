using frontend.Models;
using frontend.Services.Graph;
using frontend.Services.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace frontend.Controllers
{
	public class HomeController : Controller
	{
        #region CONSTRUCTOR
        private readonly IConfiguration _configuration;
        private readonly IGetToken _getToken;
        private readonly IGraphServices _graphServices;

        public HomeController(IConfiguration configuration, IGetToken getToken, IGraphServices graphServices)
        {
            _configuration = configuration;
            _getToken = getToken;
            _graphServices = graphServices;
        }
        #endregion

        /// <summary>
        /// Devuelve la vista de inicio de la Intranet
        /// </summary>
        /// <param name="mensaje"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
		public async Task<IActionResult> Index(string mensaje = null)
		{
			if(mensaje != null) { ViewData["ErrorTokenGraph"] = mensaje; }
            var objToken = await _getToken.GetTokenMicrosoft();
			var model = await _graphServices.GetMeGraph(objToken.access_token);
            model.Photo = await _graphServices.ImgProfile(objToken.access_token);
            ViewBag.Imagen = model.Photo;
            ViewBag.user = model.DisplayName;
			return View(model);
		}
	}
}
