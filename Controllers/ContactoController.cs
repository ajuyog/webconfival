using System.Diagnostics;
using confinancia.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace noa.Controllers;

public class ContactoController : Controller
{

    private readonly ILogger<ContactoController> _logger;
    public ContactoController(ILogger<ContactoController> logger)
    {
        _logger = logger;
    }

    [Route("/Contacto")]
    [HttpGet]
    public IActionResult Index()
    {
		
        return View();
    }

	[HttpGet]
	public IActionResult ServicioLanding()
	{
		return View();
	}
}