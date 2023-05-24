using confinancia.Models.JsonDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace confinancia.Controllers
{
	public class BannerController : Controller
	{
		#region CONSTRUCTOR
		private readonly IConfiguration _configuration;
		public BannerController(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		#endregion

		[Authorize]
		[HttpGet]
		public IActionResult Get()
		{
			return View();

		}

		[Authorize]
		[HttpGet]
		public IActionResult Create()
		{
			var token = Request.Cookies[_configuration.GetSection("Variables:Cookie").Value];
			ViewBag.Token = token;
			return View();
		}

		[Authorize]
		[HttpPost]
		public int Save(IFormFile obj, string name, string URL)
		{
			//var file = HttpContext.Request.Form.Files.First();
			var x = 6;
			var y = 7;
			return x + y;
		}


	}
}
