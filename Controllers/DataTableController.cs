using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class DataTableController : Controller
{

    private readonly ILogger<DataTableController> _logger;

    public DataTableController(ILogger<DataTableController> logger)
    {
        _logger = logger;
    }

    [Route("/datatable")]
    public IActionResult Index()
    {
        return View();
    }
}