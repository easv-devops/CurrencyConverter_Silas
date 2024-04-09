using ConverterFrontend.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace ConverterFrontendTest;

public class HomeControllerTests : IDisposable
{
    private HomeController? _homeController;

    [SetUp]
    public void Setup()
    {
        ILogger<HomeController> logger = Mock.Of<ILogger<HomeController>>();
        _homeController = new HomeController(logger);
    }
    
    [TearDown]
    public void Dispose()
    {
        _homeController?.Dispose();
    }

    [Test]
    public async Task Index_ThrowsHttpRequestException()
    {
        Assert.ThrowsAsync<HttpRequestException>(() => _homeController?.Index());
    }

    [Test]
    public void Privacy_ReturnsViewResult()
    {
        var result = _homeController?.Privacy();

        Assert.IsInstanceOf<ViewResult>(result);
    }
    
    [Test]
    public void Error_ThrowsNullReferenceException()
    {
        Assert.Throws<NullReferenceException>(() => _homeController?.Error());
    }

    [Test]
    public void ConvertCurrency_Works()
    {
        var result = _homeController?.ConvertCurrency("USD", "EUR", 100);

        Assert.IsInstanceOf<Task<IActionResult>>(result);
    }
}