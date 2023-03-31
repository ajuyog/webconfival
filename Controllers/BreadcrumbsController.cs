using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class BreadcrumbsController : Controller
{

    private readonly ILogger<BreadcrumbsController> _logger;

    public BreadcrumbsController(ILogger<BreadcrumbsController> logger)
    {
        _logger = logger;
    }

    [Route("/breadcrumbs")]
    public IActionResult Index()
    {
        return View();
    }
}