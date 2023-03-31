using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class FormAdvancedController : Controller
{

    private readonly ILogger<FormAdvancedController> _logger;

    public FormAdvancedController(ILogger<FormAdvancedController> logger)
    {
        _logger = logger;
    }

    [Route("/form-advanced")]
    public IActionResult Index()
    {
        return View();
    }
}