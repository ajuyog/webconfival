using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class MeterialIconsController : Controller
{

    private readonly ILogger<MeterialIconsController> _logger;

    public MeterialIconsController(ILogger<MeterialIconsController> logger)
    {
        _logger = logger;
    }

    [Route("/icons2")]
    public IActionResult Index()
    {
        return View();
    }
}