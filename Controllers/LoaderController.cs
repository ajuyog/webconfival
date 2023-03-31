using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class LoaderController : Controller
{

    private readonly ILogger<LoaderController> _logger;

    public LoaderController(ILogger<LoaderController> logger)
    {
        _logger = logger;
    }

    [Route("/loaders")]
    public IActionResult Index()
    {
        return View();
    }
}