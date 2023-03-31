using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class ChartFlotController : Controller
{

    private readonly ILogger<ChartFlotController> _logger;

    public ChartFlotController(ILogger<ChartFlotController> logger)
    {
        _logger = logger;
    }

    [Route("/chart-flot")]
    public IActionResult Index()
    {
        return View();
    }
}