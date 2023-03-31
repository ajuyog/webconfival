using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class SwitcherController : Controller
{

    private readonly ILogger<SwitcherController> _logger;

    public SwitcherController(ILogger<SwitcherController> logger)
    {
        _logger = logger;
    }

    [Route("/switcher")]
    public IActionResult Index()
    {
        return View();
    }
}