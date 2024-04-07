using ConverterAPI;
using Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/currencyconverter", () =>
    {
        var databaseService = new DatabaseService();
        return databaseService.GetConversions();
    })
    .WithName("GetCurrencyConversions")
    .WithOpenApi();

app.MapPost("/currencyconverter", (CurrencyConversion conversion) =>
    {
        var databaseService = new DatabaseService();
        databaseService.SaveConversion(conversion);
        return Results.Ok("Conversion saved successfully");
    })
    .WithName("SaveCurrencyConversion")
    .WithOpenApi();

app.Run();

