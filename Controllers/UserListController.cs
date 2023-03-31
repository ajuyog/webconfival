using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class UserListController : Controller
{

    private readonly ILogger<UserListController> _logger;

    public UserListController(ILogger<UserListController> logger)
    {
        _logger = logger;
    }

    [Route("/user-list")]
    public IActionResult Index()
    {
        return View();
    }
}