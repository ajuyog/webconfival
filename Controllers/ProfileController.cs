using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class ProfileController : Controller
{

    private readonly ILogger<ProfileController> _logger;

    public ProfileController(ILogger<ProfileController> logger)
    {
        _logger = logger;
    }


    [Route("/profile")]
    public IActionResult Index()
    {
        return View();
    }
}