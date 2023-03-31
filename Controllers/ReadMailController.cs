using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class ReadMailController : Controller
{

    private readonly ILogger<ReadMailController> _logger;

    public ReadMailController(ILogger<ReadMailController> logger)
    {
        _logger = logger;
    }

    [Route("/mail-read")]
    public IActionResult Index()
    {
        return View();
    }
}