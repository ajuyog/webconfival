using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class TimelineController : Controller
{

    private readonly ILogger<TimelineController> _logger;

    public TimelineController(ILogger<TimelineController> logger)
    {
        _logger = logger;
    }

    [Route("/timeline")]
    public IActionResult Index()
    {
        return View();
    }
}