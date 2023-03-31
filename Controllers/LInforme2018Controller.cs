using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class LInforme2018Controller : Controller
{

    private readonly ILogger<LandingPageController> _logger;

    public LInforme2018Controller(ILogger<LandingPageController> logger)
    {
        _logger = logger;
    }

    [Route("/2150-2018")]
    public IActionResult Index()
    {
        return View();
    }
}