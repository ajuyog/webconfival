using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class BackgroundController : Controller
{

    private readonly ILogger<BackgroundController> _logger;

    public BackgroundController(ILogger<BackgroundController> logger)
    {
        _logger = logger;
    }

    [Route("background")]
    public IActionResult Index()
    {
        return View();
    }
}