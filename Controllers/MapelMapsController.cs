using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class MapelMapsController : Controller
{

    private readonly ILogger<MapelMapsController> _logger;

    public MapelMapsController(ILogger<MapelMapsController> logger)
    {
        _logger = logger;
    }

    [Route("/maps2")]
    public IActionResult Index()
    {
        return View();
    }
}