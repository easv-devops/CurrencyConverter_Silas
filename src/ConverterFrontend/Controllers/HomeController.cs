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
        var httpClient = new HttpClient();
        var result = await httpClient.GetAsync("http://converter-api:8080/currencyconverter");
        var conversions = await result.Content.ReadFromJsonAsync<CurrencyConversion[]>();
        var model = new IndexViewModel
        {
            Conversions = conversions,
            Result = 0
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
        var currencyConverter = new CurrencyConverter();
        var convertedValue = currencyConverter.ConvertCurrency(value, source, target);

        var httpClient = new HttpClient();
        var conversion = new CurrencyConversion(DateTime.Now, source, target, value, convertedValue);
        var content = new StringContent(JsonSerializer.Serialize(conversion), Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync("http://converter-api:8080/currencyconverter", content);
        if (response.IsSuccessStatusCode)
        {
            var responseResult = await response.Content.ReadFromJsonAsync<CurrencyConversion>();
            // Save the result to the database or do something else with it
        }

        // Get the history of conversions
        var historyResult = await httpClient.GetAsync("http://converter-api:8080/currencyconverter");
        var conversions = await historyResult.Content.ReadFromJsonAsync<CurrencyConversion[]>();

        // Create a new instance of IndexViewModel and set the Result property
        var model = new IndexViewModel
        {
            Result = convertedValue,
            Conversions = conversions
        };

        // Pass the model to the View
        return View("Index", model);
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