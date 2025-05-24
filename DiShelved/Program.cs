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

// if (builder.Environment.IsDevelopment())
// {
//     builder.Configuration.AddUserSecrets<Program>();
// }

// var connectionString = builder.Configuration.GetConnectionString("DiShelvedDbConnectionString");
// builder.Services.AddDbContext<DiShelvedDbContext>(options => options.UseNpgsql(connectionString));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3000")
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

//Builder services for future repository pattern use
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IContainerRepository, ContainerRepository>();
builder.Services.AddScoped<IContainerService, ContainerService>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IItemService, ItemService>();
// builder.Services.AddScoped<IItemCategoryRepository, ItemCategoryRepository>();
// builder.Services.AddScoped<IItemCategoryService, ItemCategoryService>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<ILocationService, LocationService>();
// builder.Services.AddScoped<IUserRepository, UserRepository>();
// builder.Services.AddScoped<IUserService, UserService>();

// Temp
// allows passing datetimes without time zone data 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
// Temp
// allows our api endpoints to access the database through Entity Framework Core
builder.Services.AddNpgsql<DiShelvedDbContext>(builder.Configuration["DiShelvedDbConnectionString"]);
// Temp
// Set the JSON serializer options
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseHttpsRedirection();

// To be implemented later alongside the Endpoints
app.MapCategoryEndpoints();
app.MapContainerEndpoints();
app.MapItemEndpoints();
// app.MapItemCategoryEndpoints();
app.MapLocationEndpoints();
// app.MapUserEndpoints();


app.Run();
