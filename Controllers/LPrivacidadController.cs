using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class LPrivacidadController : Controller
{

    private readonly ILogger<LandingPageController> _logger;

    public LPrivacidadController(ILogger<LandingPageController> logger)
    {
        _logger = logger;
    }

    [Route("/Privacidad")]
    public IActionResult Index()
    {
        return View();
    }
}