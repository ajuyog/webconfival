using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class ColorsController : Controller
{

    private readonly ILogger<ColorsController> _logger;

    public ColorsController(ILogger<ColorsController> logger)
    {
        _logger = logger;
    }

    [Route("/colors")]
    public IActionResult Index()
    {
        return View();
    }
}