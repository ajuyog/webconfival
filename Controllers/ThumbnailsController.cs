using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class ThumbnailsController : Controller
{

    private readonly ILogger<ThumbnailsController> _logger;

    public ThumbnailsController(ILogger<ThumbnailsController> logger)
    {
        _logger = logger;
    }

    [Route("/thumbnails")]
    public IActionResult Index()
    {
        return View();
    }
}