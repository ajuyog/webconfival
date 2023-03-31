using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class LProyectosController : Controller
{

    private readonly ILogger<LandingPageController> _logger;

    public LProyectosController(ILogger<LandingPageController> logger)
    {
        _logger = logger;
    }

    [Route("/Proyectos")]
    public IActionResult Index()
    {
        return View();
    }
}