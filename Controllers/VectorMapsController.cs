using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class VectorMapsController : Controller
{

    private readonly ILogger<VectorMapsController> _logger;

    public VectorMapsController(ILogger<VectorMapsController> logger)
    {
        _logger = logger;
    }

    [Route("/maps")]
    public IActionResult Index()
    {
        return View();
    }
}