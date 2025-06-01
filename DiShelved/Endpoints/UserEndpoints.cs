using DiShelved.Interfaces;
using DiShelved.Models;

namespace DiShelved.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder routes)
    {
        // Get User by UID
        routes.MapGet("/Users/Uid/{uid}", async (string uid, IUserService repo) =>
        {
            var user = await repo.GetUserByUidAsync(uid);
            if (user == null)
                return Results.BadRequest("User not found for this UID");
            return Results.Ok(user);
        });

        // Create User
        routes.MapPost("/Users", async (User User, IUserService repo) =>
        {
            var createdUser = await repo.CreateUserAsync(User);
            return Results.Created($"/api/Users/{createdUser.Id}", createdUser);
        })
        .WithName("CreateUser")
        .Produces<User>(StatusCodes.Status201Created);

        // Get User by ID
        routes.MapGet("/Users/{id}", async (int id, IUserService repo) =>
        {
            var user = await repo.GetUserByIdAsync(id);
            return user is not null ? Results.Ok(user) : Results.NotFound();
        });

        // Get all Users
        routes.MapGet("/Users", async (IUserService repo) =>
        {
            var users = await repo.GetAllUsersAsync();
            return Results.Ok(users);
        });

    }
}
