using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Models;
using ConverterFrontend.Models;

namespace ConverterFrontend.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var httpClient = new HttpClient();
        var result = await httpClient.GetAsync("http://converter-api:8080/currencyconverter");
        var conversions = await result.Content.ReadFromJsonAsync<CurrencyConversion[]>();
        return View(conversions);
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