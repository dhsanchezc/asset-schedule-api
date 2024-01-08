using System.Reflection;
using AssetScheduleApi.Models;
using AssetScheduleApi.Services;
using AssetScheduleApi.Services.Interfaces;
using AssetScheduleApi.Utils;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Register custom services
builder.Services.AddScoped<IAssetService, AssetService>();

// Configure MVC Controllers
builder.Services.AddControllers();

// Configure Entity Framework and ApplicationDbContext
// Note: Using In-Memory database for simplicity. Replace with a persistent database for production.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("AssetManagementSystemDb"));

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

// Configure Swagger for API documentation
// Swagger is typically only used in development environments
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddEndpointsApiExplorer();
    //builder.Services.AddSwaggerGen();
    builder.Services.AddSwaggerGen(c =>
    {
        // Set the comments path for the Swagger JSON and UI.
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);
    });
}

var app = builder.Build();

// Configure the HTTP request pipeline.

// Enable Swagger in Development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use HTTPS redirection for security
app.UseHttpsRedirection();

// Use Authorization middleware
app.UseAuthorization();

// Map Controllers
app.MapControllers();

// Run the application
app.Run();
