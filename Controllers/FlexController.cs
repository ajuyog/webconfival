using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class FlexController : Controller
{

    private readonly ILogger<FlexController> _logger;

    public FlexController(ILogger<FlexController> logger)
    {
        _logger = logger;
    }

    [Route("/flex")]
    public IActionResult Index()
    {
        return View();
    }
}