using System.Diagnostics;
using System.Text;
using System.Text.Json;
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
        using var httpClient = new HttpClient();
        var result = await httpClient.GetAsync("http://converter-api:8080/currencyconverter");
        var conversions = await result.Content.ReadFromJsonAsync<CurrencyConversion[]>();
        var model = new IndexViewModel
        {
            Conversions = conversions,
            Conversion = null
        };
        
        
        return View(model);
    }

    public async Task<IActionResult> _History()
    {
        var httpClient = new HttpClient();
        var result = await httpClient.GetAsync("http://converter-api:8080/currencyconverter");
        var conversions = await result.Content.ReadFromJsonAsync<CurrencyConversion[]>();
        return View(conversions);
    }
    
    [HttpPost]
    public async Task<IActionResult> ConvertCurrency(string source, string target, int value)
    {
        var convertedValue = new CurrencyConverter().ConvertCurrency(value, source, target);
        var conversion = new CurrencyConversion(DateTime.Now, source, target, value, convertedValue);

        using var httpClient = new HttpClient();
        var content = new StringContent(JsonSerializer.Serialize(conversion), Encoding.UTF8, "application/json");
        await httpClient.PostAsync("http://converter-api:8080/currencyconverter", content);

        var conversions = await httpClient.GetFromJsonAsync<CurrencyConversion[]>("http://converter-api:8080/currencyconverter");
        return View("Index", new IndexViewModel { Conversions = conversions, Conversion = conversion });
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