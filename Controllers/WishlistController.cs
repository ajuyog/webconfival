using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class WishlistController : Controller
{

    private readonly ILogger<WishlistController> _logger;

    public WishlistController(ILogger<WishlistController> logger)
    {
        _logger = logger;
    }

    [Route("/wishlist")]
    public IActionResult Index()
    {
        return View();
    }
}