using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class ProjectNewController : Controller
{

    private readonly ILogger<ProjectNewController> _logger;

    public ProjectNewController(ILogger<ProjectNewController> logger)
    {
        _logger = logger;
    }

    [Route("/projects-new")]
    public IActionResult Index()
    {
        return View();
    }
}