using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class NavigationController : Controller
{

    private readonly ILogger<NavigationController> _logger;

    public NavigationController(ILogger<NavigationController> logger)
    {
        _logger = logger;
    }

    [Route("/navigation")]
    public IActionResult Index()
    {
        return View();
    }
}