using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class PricingController : Controller
{

    private readonly ILogger<PricingController> _logger;

    public PricingController(ILogger<PricingController> logger)
    {
        _logger = logger;
    }

    [Route("/pricing")]
    public IActionResult Index()
    {
        return View();
    }
}