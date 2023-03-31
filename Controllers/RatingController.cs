using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class RatingController : Controller
{

    private readonly ILogger<RatingController> _logger;

    public RatingController(ILogger<RatingController> logger)
    {
        _logger = logger;
    }

    [Route("/rating")]
    public IActionResult Index()
    {
        return View();
    }
}