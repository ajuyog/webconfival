using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class CarouselController : Controller
{

    private readonly ILogger<CarouselController> _logger;

    public CarouselController(ILogger<CarouselController> logger)
    {
        _logger = logger;
    }

    [Route("/carousel")]
    public IActionResult Index()
    {
        return View();
    }
}