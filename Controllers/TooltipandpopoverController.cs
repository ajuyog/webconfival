using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class TooltipandpopoverController : Controller
{

    private readonly ILogger<TooltipandpopoverController> _logger;

    public TooltipandpopoverController(ILogger<TooltipandpopoverController> logger)
    {
        _logger = logger;
    }

    [Route("/tooltipandpopover")]
    public IActionResult Index()
    {
        return View();
    }
}