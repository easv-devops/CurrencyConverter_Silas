using System.ComponentModel.Design;
using ConverterAPI;

namespace ConverterAPITest;

public class Tests
{
    [Test]
    public void TemperatureTest()
    {
        // Arrange
        var databaseService = new MockDatabaseService();
        
        // Act
        var conversions = databaseService.GetConversions();
        
        // Assert
        Assert.That(conversions.Length, Is.EqualTo(2));
    }
}