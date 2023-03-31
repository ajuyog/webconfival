using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class TaskListController : Controller
{

    private readonly ILogger<TaskListController> _logger;

    public TaskListController(ILogger<TaskListController> logger)
    {
        _logger = logger;
    }

    [Route("/tasks-list")]
    public IActionResult Index()
    {
        return View();
    }
}