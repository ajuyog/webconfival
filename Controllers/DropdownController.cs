using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class DropdownController : Controller
{

    private readonly ILogger<DropdownController> _logger;

    public DropdownController(ILogger<DropdownController> logger)
    {
        _logger = logger;
    }

    [Route("/dropdown")]
    public IActionResult Index()
    {
        return View();
    }
}