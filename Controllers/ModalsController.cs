using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class ModalsController : Controller
{

    private readonly ILogger<ModalsController> _logger;

    public ModalsController(ILogger<ModalsController> logger)
    {
        _logger = logger;
    }

    [Route("/modals")]
    public IActionResult Index()
    {
        return View();
    }
}