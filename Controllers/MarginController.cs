using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class MarginController : Controller
{

    private readonly ILogger<MarginController> _logger;

    public MarginController(ILogger<MarginController> logger)
    {
        _logger = logger;
    }

    [Route("/margin")]
    public IActionResult Index()
    {
        return View();
    }
}