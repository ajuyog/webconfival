using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class EditPostController : Controller
{

    private readonly ILogger<EditPostController> _logger;

    public EditPostController(ILogger<EditPostController> logger)
    {
        _logger = logger;
    }

    [Route("/blog-edit")]
    public IActionResult Index()
    {
        return View();
    }
}