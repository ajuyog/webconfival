using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class ScrollController : Controller
{

    private readonly ILogger<ScrollController> _logger;

    public ScrollController(ILogger<ScrollController> logger)
    {
        _logger = logger;
    }

    [Route("/scroll")]
    public IActionResult Index()
    {
        return View();
    }
}