using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class ChartEchartController : Controller
{

    private readonly ILogger<ChartEchartController> _logger;

    public ChartEchartController(ILogger<ChartEchartController> logger)
    {
        _logger = logger;
    }

    [Route("/chart-echart")]
    public IActionResult Index()
    {
        return View();
    }
}