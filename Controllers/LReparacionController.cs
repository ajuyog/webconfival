using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class LReparacionController : Controller
{

    private readonly ILogger<LandingPageController> _logger;

    public LReparacionController(ILogger<LandingPageController> logger)
    {
        _logger = logger;
    }

    [Route("/Quienes-somos")]
    public IActionResult Index()
    {
        return View();
    }
}