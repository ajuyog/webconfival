using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class SimpleIconsController : Controller
{

    private readonly ILogger<SimpleIconsController> _logger;

    public SimpleIconsController(ILogger<SimpleIconsController> logger)
    {
        _logger = logger;
    }

    [Route("/icons3")]
    public IActionResult Index()
    {
        return View();
    }
}