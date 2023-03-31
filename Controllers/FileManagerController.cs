using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class FileManagerController : Controller
{

    private readonly ILogger<FileManagerController> _logger;

    public FileManagerController(ILogger<FileManagerController> logger)
    {
        _logger = logger;
    }

    [Route("/file-manager-1")]
    public IActionResult Index()
    {
        return View();
    }
}