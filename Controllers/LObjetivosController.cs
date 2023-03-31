using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class LObjetivosController : Controller
{

    private readonly ILogger<LandingPageController> _logger;

    public LObjetivosController(ILogger<LandingPageController> logger)
    {
        _logger = logger;
    }

    [Route("/Objetivos")]
    public IActionResult Index()
    {
        return View();
    }
}