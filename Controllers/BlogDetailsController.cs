using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class BlogDetailsController : Controller
{

    private readonly ILogger<BlogDetailsController> _logger;

    public BlogDetailsController(ILogger<BlogDetailsController> logger)
    {
        _logger = logger;
    }

    [Route("/blog-details")]
    public IActionResult Index()
    {
        return View();
    }
}