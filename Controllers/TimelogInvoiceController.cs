using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class TimelogInvoiceController : Controller
{

    private readonly ILogger<TimelogInvoiceController> _logger;

    public TimelogInvoiceController(ILogger<TimelogInvoiceController> logger)
    {
        _logger = logger;
    }

    [Route("/invoice-timelog")]
    public IActionResult Index()
    {
        return View();
    }
}