using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class PositionController : Controller
{

    private readonly ILogger<PositionController> _logger;

    public PositionController(ILogger<PositionController> logger)
    {
        _logger = logger;
    }

    [Route("/position")]
    public IActionResult Index()
    {
        return View();
    }
}