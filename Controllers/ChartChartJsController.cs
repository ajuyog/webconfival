using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class ChartChartJsController : Controller
{

    private readonly ILogger<ChartChartJsController> _logger;

    public ChartChartJsController(ILogger<ChartChartJsController> logger)
    {
        _logger = logger;
    }

    [Route("/chart-chartjs")]
    public IActionResult Index()
    {
        return View();
    }
}