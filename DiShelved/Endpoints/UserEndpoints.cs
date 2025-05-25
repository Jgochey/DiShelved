using DiShelved.Interfaces;
using DiShelved.Models;

namespace DiShelved.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder routes)
    {
        // Create User
        routes.MapPost("/Users", async (User User, IUserService repo) =>
        {
            var createdUser = await repo.CreateUserAsync(User); 
            return Results.Created($"/api/Users/{createdUser.Id}", createdUser);
        })
        .WithName("CreateUser")
        .Produces<User>(StatusCodes.Status201Created);





        // Testing purposes, remove later.
        // Get All Users
        routes.MapGet("/Users", async (IUserService repo) =>
        {
            return await repo.GetAllUsersAsync();
        })
        .WithName("GetAllUsers")
        .Produces<List<User>>(StatusCodes.Status200OK);




        // Delete User
        routes.MapDelete("/Users/{id}", async (int id, IUserService repo) =>
        {
            var deletion = await repo.DeleteUserAsync(id);
            return deletion ? Results.NoContent() : Results.NotFound();
            
        })
        .WithName("DeleteUser")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound);
    }
}
