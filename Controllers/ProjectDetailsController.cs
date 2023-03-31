using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class ProjectDetailsController : Controller
{

    private readonly ILogger<ProjectDetailsController> _logger;

    public ProjectDetailsController(ILogger<ProjectDetailsController> logger)
    {
        _logger = logger;
    }

    [Route("/projects-details")]
    public IActionResult Index()
    {
        return View();
    }
}