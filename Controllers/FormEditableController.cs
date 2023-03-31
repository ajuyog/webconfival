using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class FormEditableController : Controller
{

    private readonly ILogger<FormEditableController> _logger;

    public FormEditableController(ILogger<FormEditableController> logger)
    {
        _logger = logger;
    }

    [Route("/form-editable")]
    public IActionResult Index()
    {
        return View();
    }
}