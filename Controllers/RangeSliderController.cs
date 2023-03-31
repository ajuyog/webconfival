using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class RangeSliderController : Controller
{

    private readonly ILogger<RangeSliderController> _logger;

    public RangeSliderController(ILogger<RangeSliderController> logger)
    {
        _logger = logger;
    }

    [Route("/rangeslider")]
    public IActionResult Index()
    {
        return View();
    }
}