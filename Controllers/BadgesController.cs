using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class BadgesController : Controller
{

    private readonly ILogger<BadgesController> _logger;

    public BadgesController(ILogger<BadgesController> logger)
    {
        _logger = logger;
    }

    [Route("/badge")]
    public IActionResult Index()
    {
        return View();
    }
}