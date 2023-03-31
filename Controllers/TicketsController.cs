using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class TicketsController : Controller
{

    private readonly ILogger<TicketsController> _logger;

    public TicketsController(ILogger<TicketsController> logger)
    {
        _logger = logger;
    }

    [Route("/tickets-details")]
    public IActionResult Index()
    {
        return View();
    }
}