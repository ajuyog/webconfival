using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class FormWizardController : Controller
{

    private readonly ILogger<FormWizardController> _logger;

    public FormWizardController(ILogger<FormWizardController> logger)
    {
        _logger = logger;
    }

    [Route("/form-wizard")]
    public IActionResult Index()
    {
        return View();
    }
}