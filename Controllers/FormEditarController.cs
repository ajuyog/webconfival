using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class FormEditarController : Controller
{

    private readonly ILogger<FormEditarController> _logger;

    public FormEditarController(ILogger<FormEditarController> logger)
    {
        _logger = logger;
    }

    [Route("/wysiwyag")]
    public IActionResult Index()
    {
        return View();
    }
}