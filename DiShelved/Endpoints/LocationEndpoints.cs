using DiShelved.Interfaces;
using DiShelved.Models;

namespace DiShelved.Endpoints;

public static class LocationEndpoints
{
    public static void MapLocationEndpoints(this IEndpointRouteBuilder routes)
    {
        // Get All Locations
        routes.MapGet("/Locations", async (ILocationService repo) =>
        {
            return await repo.GetAllLocationsAsync();
        })
        .WithName("GetAllLocations")
        .Produces<List<Location>>(StatusCodes.Status200OK);

        // Get Location By Id
        routes.MapGet("/Locations/{id}", async (int id, ILocationService repo) =>
        {
            var Location = await repo.GetLocationByIdAsync(id);
            return Location is not null ? Results.Ok(Location) : Results.NotFound();
        })
        .WithName("GetLocationById")
        .Produces<Location>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        // Create Location
        routes.MapPost("/Locations", async (Location Location, ILocationService repo) =>
        {
            var createdLocation = await repo.CreateLocationAsync(Location); 
            return Results.Created($"/api/Locations/{createdLocation.Id}", createdLocation);
        })
        .WithName("CreateLocation")
        .Produces<Location>(StatusCodes.Status201Created);

        // Update Location
        routes.MapPut("/Locations/{id}", async (int id, Location Location, ILocationService repo) =>
        {
            var updatedLocation = await repo.UpdateLocationAsync(id, Location);
            return updatedLocation is not null ? Results.Ok(updatedLocation) : Results.NotFound();
        })
        .WithName("UpdateLocation")
        .Produces<Location>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        // Delete Location
        routes.MapDelete("/Locations/{id}", async (int id, ILocationService repo) =>
        {
            var deletion = await repo.DeleteLocationAsync(id);
            return deletion ? Results.NoContent() : Results.NotFound();
            
        })
        .WithName("DeleteLocation")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound);
    }
}
