using confinancia.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace confinancia.Controllers
{
	public class HomeController : Controller
	{
		[Authorize]
		public IActionResult Index()
		{
			var model = new UserDTO()
			{
				Nombre = User.Identities.First().Claims.ElementAtOrDefault(2).Value,
				Apellido = User.Identities.First().Claims.ElementAtOrDefault(3).Value,
				Correo = User.Identities.First().Claims.ElementAtOrDefault(4).Value
			};

			return View(model);
		}
	}
}
