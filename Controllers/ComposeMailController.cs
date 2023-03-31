using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class ComposeMailController : Controller
{

    private readonly ILogger<ComposeMailController> _logger;

    public ComposeMailController(ILogger<ComposeMailController> logger)
    {
        _logger = logger;
    }

    [Route("/mail-compose")]
    public IActionResult Index()
    {
        return View();
    }
}