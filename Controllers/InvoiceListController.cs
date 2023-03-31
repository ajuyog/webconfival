using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class InvoiceListController : Controller
{

    private readonly ILogger<InvoiceListController> _logger;

    public InvoiceListController(ILogger<InvoiceListController> logger)
    {
        _logger = logger;
    }

    [Route("/invoice-list")]
    public IActionResult Index()
    {
        return View();
    }
}