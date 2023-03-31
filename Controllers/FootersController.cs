using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class FootersController : Controller
{

    private readonly ILogger<FootersController> _logger;

    public FootersController(ILogger<FootersController> logger)
    {
        _logger = logger;
    }

    [Route("/footers")]
    public IActionResult Index()
    {
        return View();
    }
}