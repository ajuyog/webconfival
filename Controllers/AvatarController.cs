using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class AvatarController : Controller
{

    private readonly ILogger<AvatarController> _logger;

    public AvatarController(ILogger<AvatarController> logger)
    {
        _logger = logger;
    }

    [Route("/avatar")]
    public IActionResult Index()
    {
        return View();
    }
}