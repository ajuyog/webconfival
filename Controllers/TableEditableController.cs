using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class TableEditableController : Controller
{

    private readonly ILogger<TableEditableController> _logger;

    public TableEditableController(ILogger<TableEditableController> logger)
    {
        _logger = logger;
    }

    [Route("/table-editable")]
    public IActionResult Index()
    {
        return View();
    }
}