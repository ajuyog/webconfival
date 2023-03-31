using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class IonicIconsController : Controller
{

    private readonly ILogger<IonicIconsController> _logger;

    public IonicIconsController(ILogger<IonicIconsController> logger)
    {
        _logger = logger;
    }

    [Route("/icons5")]
    public IActionResult Index()
    {
        return View();
    }
}