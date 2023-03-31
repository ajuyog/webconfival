using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class FontAwesomeController : Controller
{

    private readonly ILogger<FontAwesomeController> _logger;

    public FontAwesomeController(ILogger<FontAwesomeController> logger)
    {
        _logger = logger;
    }

    [Route("/icons")]
    public IActionResult Index()
    {
        return View();
    }
}