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
                    "https://d392wajczib7rj.cloudfront.net", // CloudFront HTTPS endpoint
                    "http://localhost:3000", // For local testing
                    "http://18.220.187.102" // EC2 backend public IP
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
    var connectionString = GetConnectionString(builder.Configuration);
    Console.WriteLine($"Using connection string: {connectionString?.Substring(0, Math.Min(50, connectionString?.Length ?? 0))}..."); // Log first 50 chars for debugging
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

// Handle OPTIONS requests explicitly for CORS preflight
app.Use(async (context, next) =>
{
    if (context.Request.Method == "OPTIONS")
    {
        context.Response.StatusCode = 200;
        await context.Response.CompleteAsync();
        return;
    }
    await next();
});

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

// Auto-migrate database on startup (for production)
if (!app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<DiShelvedDbContext>();
        Console.WriteLine("Running database migrations...");
        try
        {
            context.Database.Migrate();
            Console.WriteLine("Database migrations completed.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Migration error: {ex.Message}");
            throw;
        }
    }
}

app.Run();

// Add this helper method at the very end, just before the final closing brace
static string GetConnectionString(IConfiguration configuration)
{
    // First try to get DATABASE_URL from environment (Render format)
    var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
    
    if (!string.IsNullOrEmpty(databaseUrl))
    {
        Console.WriteLine("Found DATABASE_URL, converting from Render format...");
        
        // Parse DATABASE_URL format: postgresql://username:password@host:port/database
        try
        {
            var uri = new Uri(databaseUrl);
            var userInfo = uri.UserInfo.Split(':');
            var username = userInfo[0];
            var password = userInfo.Length > 1 ? userInfo[1] : "";
            
            // Handle default PostgreSQL port when not specified
            var port = uri.Port == -1 ? 5432 : uri.Port;
            
            var connectionString = $"Host={uri.Host};Port={port};Database={uri.LocalPath.TrimStart('/')};Username={username};Password={password};SSL Mode=Require;Trust Server Certificate=true";
            
            Console.WriteLine($"Converted connection string format successfully");
            Console.WriteLine($"Host: {uri.Host}, Port: {port}, Database: {uri.LocalPath.TrimStart('/')}");
            return connectionString;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error parsing DATABASE_URL: {ex.Message}");
            throw new InvalidOperationException("Invalid DATABASE_URL format", ex);
        }
    }
    
    // Fallback to appsettings.json connection string (for local development)
    var fallbackConnectionString = configuration.GetConnectionString("DiShelvedDbConnectionString");
    Console.WriteLine("Using fallback connection string from appsettings.json");
    return fallbackConnectionString ?? throw new InvalidOperationException("No database connection string found");
}
