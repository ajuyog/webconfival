using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class Pe7Controller : Controller
{

    private readonly ILogger<Pe7Controller> _logger;

    public Pe7Controller(ILogger<Pe7Controller> logger)
    {
        _logger = logger;
    }

    [Route("/icons7")]
    public IActionResult Index()
    {
        return View();
    }
}