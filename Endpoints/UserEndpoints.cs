// ─────────────────────────────────────────────────────────────────
// UserEndpoints.cs
// All /api/users routes live here.
// Shows how to add route-level authorization when needed.
// ─────────────────────────────────────────────────────────────────

namespace App_with_yml_dot.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/users")
                       .WithTags("Users");

        // GET /api/users
        group.MapGet("/", GetAllUsers)
             .WithName("GetAllUsers")
             .WithSummary("Get all users");

        // GET /api/users/{id}
        group.MapGet("/{id:int}", GetUserById)
             .WithName("GetUserById")
             .WithSummary("Get user by ID");

        // POST /api/users/register
        group.MapPost("/register", RegisterUser)
             .WithName("RegisterUser")
             .WithSummary("Register a new user");

        // DELETE /api/users/{id}
        group.MapDelete("/{id:int}", DeleteUser)
             .WithName("DeleteUser")
             .WithSummary("Delete a user");
             // .RequireAuthorization()  ← uncomment when auth is added
    }

    // ── Handlers ──────────────────────────────────────────────────

    static IResult GetAllUsers()
    {
        var users = new[]
        {
            new { Id = 1, Name = "Debashis", Email = "d@example.com" },
            new { Id = 2, Name = "John",     Email = "j@example.com" }
        };
        return Results.Ok(users);
    }

    static IResult GetUserById(int id)
    {
        var user = new { Id = id, Name = "Debashis", Email = "d@example.com" };
        return Results.Ok(user);
    }

    static IResult RegisterUser(RegisterUserRequest request)
    {
        // In real app: hash password, save to DB, send welcome email
        return Results.Created($"/api/users/1", new { Id = 1, request.Name, request.Email });
    }

    static IResult DeleteUser(int id)
    {
        return Results.NoContent();
    }
}

// ── Request / Response Models ─────────────────────────────────────
public record RegisterUserRequest(string Name, string Email, string Password);
