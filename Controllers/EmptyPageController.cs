using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class EmptyPageController : Controller
{

    private readonly ILogger<EmptyPageController> _logger;

    public EmptyPageController(ILogger<EmptyPageController> logger)
    {
        _logger = logger;
    }

    [Route("/empty")]
    public IActionResult Index()
    {
        return View();
    }
}