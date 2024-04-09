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
    private readonly HttpClient _httpClient;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
        _httpClient = new HttpClient();
        SetBaseAddress("http://converter-api:8080/");
    }
    
    public void SetBaseAddress(string baseAddress)
    {
        _httpClient.BaseAddress = new Uri(baseAddress);
    }

    public async Task<IActionResult> Index()
    {
        var result = await _httpClient.GetAsync(_httpClient.BaseAddress + "currencyconverter");
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
        var result = await _httpClient.GetAsync(_httpClient.BaseAddress + "currencyconverter");
        var conversions = await result.Content.ReadFromJsonAsync<CurrencyConversion[]>();
        return View(conversions);
    }

    [HttpPost]
    public async Task<IActionResult> ConvertCurrency(string source, string target, int value)
    {
        var convertedValue = new CurrencyConverter().ConvertCurrency(value, source, target);
        var conversion = new CurrencyConversion(DateTime.Now, source, target, value, convertedValue);

        var content = new StringContent(JsonSerializer.Serialize(conversion), Encoding.UTF8, "application/json");
        await _httpClient.PostAsync(_httpClient.BaseAddress + "currencyconverter", content);

        var conversions = await _httpClient.GetFromJsonAsync<CurrencyConversion[]>(_httpClient.BaseAddress + "currencyconverter");
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