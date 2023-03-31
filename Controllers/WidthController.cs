using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class WidthController : Controller
{

    private readonly ILogger<WidthController> _logger;

    public WidthController(ILogger<WidthController> logger)
    {
        _logger = logger;
    }


    [Route("/width")]
    public IActionResult Index()
    {
        return View();
    }
}