using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class InvoiceDetailsController : Controller
{

    private readonly ILogger<InvoiceDetailsController> _logger;

    public InvoiceDetailsController(ILogger<InvoiceDetailsController> logger)
    {
        _logger = logger;
    }

    [Route("/invoice-details")]
    public IActionResult Index()
    {
        return View();
    }
}