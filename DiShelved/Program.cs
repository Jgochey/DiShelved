using Microsoft.EntityFrameworkCore;
using DiShelved.Data;
using DiShelved.Models;
using Microsoft.AspNetCore.Http.Json;
using System.Text.Json.Serialization;
using DiShelved.Interfaces;
using DiShelved.Repositories;
using DiShelved.Services;
using DiShelved.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        // Add logging to see which environment we're in
        var environment = builder.Environment.EnvironmentName;
        Console.WriteLine($"Environment: {environment}");
        Console.WriteLine($"IsDevelopment: {builder.Environment.IsDevelopment()}");
        
        if (builder.Environment.IsDevelopment())
        {
            Console.WriteLine("Configuring CORS for Development");
            policy.WithOrigins("http://localhost:3000")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        }
        else
        {
            Console.WriteLine("Configuring CORS for Production");
            // Production CORS - be more explicit and add fallbacks
            policy.WithOrigins(
                    "https://dishelved.netlify.app",
                    "https://dishelved.onrender.com" // Also allow your backend domain for testing
                )
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        }
    });
});

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IContainerRepository, ContainerRepository>();
builder.Services.AddScoped<IContainerService, ContainerService>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IItemCategoryRepository, ItemCategoryRepository>();
builder.Services.AddScoped<IItemCategoryService, ItemCategoryService>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddDbContext<DiShelvedDbContext>(options =>
{
    // Use DATABASE_URL if available (Render), otherwise use connection string
    var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL") 
        ?? builder.Configuration.GetConnectionString("DiShelvedDbConnectionString");
    options.UseNpgsql(connectionString);
});

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

// Add CORS before other middleware and add logging
app.Use(async (context, next) =>
{
    Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");
    Console.WriteLine($"Origin: {context.Request.Headers.Origin}");
    await next();
    Console.WriteLine($"Response Status: {context.Response.StatusCode}");
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Move CORS before HTTPS redirection
app.UseCors();

// Only use HTTPS redirection in development
if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.MapCategoryEndpoints();
app.MapContainerEndpoints();
app.MapItemEndpoints();
app.MapItemCategoryEndpoints();
app.MapLocationEndpoints();
app.MapUserEndpoints();


app.Run();
