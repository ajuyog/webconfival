using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class PanelsController : Controller
{

    private readonly ILogger<PanelsController> _logger;

    public PanelsController(ILogger<PanelsController> logger)
    {
        _logger = logger;
    }

    [Route("/panels")]
    public IActionResult Index()
    {
        return View();
    }
}