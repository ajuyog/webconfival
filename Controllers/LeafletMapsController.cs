using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class LeafletMapsController : Controller
{

    private readonly ILogger<LeafletMapsController> _logger;

    public LeafletMapsController(ILogger<LeafletMapsController> logger)
    {
        _logger = logger;
    }

    [Route("/maps1")]
    public IActionResult Index()
    {
        return View();
    }
}