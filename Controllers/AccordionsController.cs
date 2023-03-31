using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class AccordionsController : Controller
{

    private readonly ILogger<AccordionsController> _logger;

    public AccordionsController(ILogger<AccordionsController> logger)
    {
        _logger = logger;
    }

    [Route("/accordions")]
    public IActionResult Index()
    {
        return View();
    }
}