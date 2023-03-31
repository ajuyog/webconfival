using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class Calendar2Controller : Controller
{

    private readonly ILogger<Calendar2Controller> _logger;

    public Calendar2Controller(ILogger<Calendar2Controller> logger)
    {
        _logger = logger;
    }

    [Route("/calender2")]
    public IActionResult Index()
    {
        return View();
    }
}