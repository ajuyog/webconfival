using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class ProductDetailscontroller : Controller
{

    private readonly ILogger<ProductDetailscontroller> _logger;

    public ProductDetailscontroller(ILogger<ProductDetailscontroller> logger)
    {
        _logger = logger;
    }

    [Route("/products-details")]
    public IActionResult Index()
    {
        return View();
    }
}