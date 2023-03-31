using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class TypographyController : Controller
{

    private readonly ILogger<TypographyController> _logger;

    public TypographyController(ILogger<TypographyController> logger)
    {
        _logger = logger;
    }

    [Route("/typography")]
    public IActionResult Index()
    {
        return View();
    }
}