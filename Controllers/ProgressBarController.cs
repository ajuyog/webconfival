using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class ProgressBarController : Controller
{

    private readonly ILogger<ProgressBarController> _logger;

    public ProgressBarController(ILogger<ProgressBarController> logger)
    {
        _logger = logger;
    }

    [Route("/progress")]
    public IActionResult Index()
    {
        return View();
    }
}