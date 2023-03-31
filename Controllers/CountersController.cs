using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class CountersController : Controller
{

    private readonly ILogger<CountersController> _logger;

    public CountersController(ILogger<CountersController> logger)
    {
        _logger = logger;
    }

    [Route("/counters")]
    public IActionResult Index()
    {
        return View();
    }
}