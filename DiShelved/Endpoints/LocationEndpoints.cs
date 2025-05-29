using DiShelved.Interfaces;
using DiShelved.Models;

namespace DiShelved.Endpoints;

public static class LocationEndpoints
{
    public static void MapLocationEndpoints(this IEndpointRouteBuilder routes)
    {
        // Get Locations By User Id
        routes.MapGet("/Locations/User/{userId}", async (int userId, ILocationService repo) =>
        {
            var locations = await repo.GetLocationsByUserIdAsync(userId);
            return locations is not null ? Results.Ok(locations) : Results.NotFound();
        });

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
        routes.MapDelete("/Locations/{id}", async (
            int id, 
            ILocationService locationService, 
            IContainerService containerService) =>
        {
            var containers = await containerService.GetContainersByLocationIdAsync(id);
            if (containers.Any())
            {
                return Results.Problem("Location has Containers associated with it. Remove any Containers before deleting the Location.");
            }

            var deletion = await locationService.DeleteLocationAsync(id);
            return deletion ? Results.NoContent() : Results.NotFound();
        })
        .WithName("DeleteLocation")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound);
    }
}
