using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class FileAttachmentController : Controller
{

    private readonly ILogger<FileAttachmentController> _logger;

    public FileAttachmentController(ILogger<FileAttachmentController> logger)
    {
        _logger = logger;
    }

    [Route("/file-attachment")]
    public IActionResult Index()
    {
        return View();
    }
}