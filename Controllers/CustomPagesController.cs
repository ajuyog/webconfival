using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using noa.Models;

namespace noa.Controllers;

public class CustomPagesController : Controller
{

    private readonly ILogger<CustomPagesController> _logger;

    public CustomPagesController(ILogger<CustomPagesController> logger)
    {
        _logger = logger;
    }

    [Route("/error404")]
    public IActionResult error404()
    {
        return View("error404","_Layout2");
    }

    [Route("/error501")]
    public IActionResult error501()
    {
        return View("error501","_Layout2");
    }

    [Route("/error500")]
    public IActionResult error500()
    {
        return View("error500","_Layout2");
    }

    [Route("/forgot-password")]
    public IActionResult forgotpassword()
    {
        return View("forgotpassword","_Layout1");
    }

    [Route("/lockscreen")]
    public IActionResult lockscreen()
    {
        return View("lockscreen","_Layout1");
    }

    [Route("/construction")]
    public IActionResult construction()
    {
        return View("construction","_Layout1");
    }

    [Route("/login")]
    public IActionResult login()
    {
        return View("login","_Layout1");
    }

    [Route("/rigister")]
    public IActionResult register()
    {
        return View("register","_Layout1");
    }
    



}