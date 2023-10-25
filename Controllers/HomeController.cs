using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Assignment_Random_Passcode.Models;

namespace Assignment_Random_Passcode.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        if(HttpContext.Session.GetInt32("Contador") == null)
        {
            HttpContext.Session.SetInt32("Contador", 0 );
        }
        else if(HttpContext.Session.GetInt32("Contador")> 0)
        {
            HttpContext.Session.SetInt32("Contador", 0);
        }
        return View("Index");
    }

    [HttpPost("GenerateRamdon")]
    public IActionResult GenerateRamdon()
    {
        int count = HttpContext.Session.GetInt32("Contador") ?? 0;
        HttpContext.Session.SetInt32("Contador", count + 1);
        string chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        Random random = new Random();
        string result = new string(
            Enumerable.Repeat(chars, 14)
                    .Select(s => s[random.Next(s.Length)])
                    .ToArray());
        ViewBag.Resultado = result;
        return View("Index");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
