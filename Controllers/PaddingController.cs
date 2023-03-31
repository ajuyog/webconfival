using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class PaddingController : Controller
{

    private readonly ILogger<PaddingController> _logger;

    public PaddingController(ILogger<PaddingController> logger)
    {
        _logger = logger;
    }

    [Route("/padding")]
    public IActionResult Index()
    {
        return View();
    }
}