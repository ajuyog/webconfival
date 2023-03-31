using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class TreeviewController : Controller
{

    private readonly ILogger<TreeviewController> _logger;

    public TreeviewController(ILogger<TreeviewController> logger)
    {
        _logger = logger;
    }

    [Route("/treeview")]
    public IActionResult Index()
    {
        return View();
    }
}