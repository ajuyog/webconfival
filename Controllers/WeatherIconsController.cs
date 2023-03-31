using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class WeatherIconsController : Controller
{

    private readonly ILogger<WeatherIconsController> _logger;

    public WeatherIconsController(ILogger<WeatherIconsController> logger)
    {
        _logger = logger;
    }

    [Route("/icons10")]
    public IActionResult Index()
    {
        return View();
    }
}