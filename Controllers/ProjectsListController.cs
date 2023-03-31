using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class ProjectsListController : Controller
{

    private readonly ILogger<ProjectsListController> _logger;

    public ProjectsListController(ILogger<ProjectsListController> logger)
    {
        _logger = logger;
    }

    [Route("/projects-list")]
    public IActionResult Index()
    {
        return View();
    }
}