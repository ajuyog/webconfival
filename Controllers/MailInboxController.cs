using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class MailInboxController : Controller
{

    private readonly ILogger<MailInboxController> _logger;

    public MailInboxController(ILogger<MailInboxController> logger)
    {
        _logger = logger;
    }

    [Route("/mail-inbox")]
    public IActionResult Index()
    {
        return View();
    }
}