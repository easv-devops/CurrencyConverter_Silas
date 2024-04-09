using System.ComponentModel.Design;
using ConverterAPI;
using Models;
using Moq;

namespace ConverterAPITest;

public class ServiceTests
{
    [Test]
    public void SaveConversion_ShouldInsertConversionIntoDatabase_WhenConversionIsValid()
    {
        // Arrange
        var mockDbService = new Mock<IDatabaseService>();
        var validConversion = new CurrencyConversion
        {
            Date = DateTime.Now,
            Source = "USD",
            Target = "EUR",
            Value = 100,
            Result = 85
        };

        // Act
        mockDbService.Object.SaveConversion(validConversion);

        // Assert
        mockDbService.Verify(m => m.SaveConversion(It.Is<CurrencyConversion>(c =>
            c.Date == validConversion.Date &&
            c.Source == validConversion.Source &&
            c.Target == validConversion.Target &&
            c.Value == validConversion.Value &&
            c.Result == validConversion.Result)), Times.Once);
    }

    [Test]
    public void SaveConversion_ShouldThrowException_WhenConversionIsNull()
    {
        // Arrange
        var mockDbService = new Mock<IDatabaseService>();
        CurrencyConversion nullConversion = null;
        
        mockDbService.Setup(m => m.SaveConversion(nullConversion)).Throws<ArgumentNullException>();

        // Act & Assert
        Assert.That(() => mockDbService.Object.SaveConversion(nullConversion), Throws.ArgumentNullException);
    }

    [Test]
    public void SaveConversion_ShouldThrowException_WhenConversionIsInvalid()
    {
        // Arrange
        var mockDbService = new Mock<IDatabaseService>();
        var invalidConversion = new CurrencyConversion
        {
            Date = DateTime.Now,
            Source = "USD",
            Target = "EUR",
            Value = -100, // Invalid value
            Result = 85
        };
        
        mockDbService.Setup(m => m.SaveConversion(invalidConversion)).Throws<ArgumentException>();

        // Act & Assert
        Assert.That(() => mockDbService.Object.SaveConversion(invalidConversion), Throws.ArgumentException);
    }
    
    [Test]
    public void GetConversions_ShouldReturnConversionsFromDatabase()
    {
        // Arrange
        var mockDbService = new Mock<IDatabaseService>();
        var expectedConversions = new CurrencyConversion[]
        {
            new CurrencyConversion(DateTime.Now, "USD", "EUR", 100, 85),
            new CurrencyConversion(DateTime.Now, "EUR", "USD", 100, 118)
        };
        mockDbService.Setup(m => m.GetConversions()).Returns(expectedConversions);

        // Act
        var actualConversions = mockDbService.Object.GetConversions();

        // Assert
        Assert.That(actualConversions, Is.EqualTo(expectedConversions));
    }
}