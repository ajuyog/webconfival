using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class DisplayController : Controller
{

    private readonly ILogger<DisplayController> _logger;

    public DisplayController(ILogger<DisplayController> logger)
    {
        _logger = logger;
    }

    [Route("/display")]
    public IActionResult Index()
    {
        return View();
    }
}