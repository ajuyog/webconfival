using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class BorderController : Controller
{

    private readonly ILogger<BorderController> _logger;

    public BorderController(ILogger<BorderController> logger)
    {
        _logger = logger;
    }

    [Route("/border")]
    public IActionResult Index()
    {
        return View();
    }
}