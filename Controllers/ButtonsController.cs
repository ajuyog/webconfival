using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class ButtonsController : Controller
{

    private readonly ILogger<ButtonsController> _logger;

    public ButtonsController(ILogger<ButtonsController> logger)
    {
        _logger = logger;
    }

    [Route("/buttons")]
    public IActionResult Index()
    {
        return View();
    }
}