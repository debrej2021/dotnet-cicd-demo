// ─────────────────────────────────────────────────────────────────
// OrderEndpoints.cs
// All /api/orders routes live here.
// Pattern is identical to ProductEndpoints — just a different group.
// ─────────────────────────────────────────────────────────────────

namespace App_with_yml_dot.Endpoints;

public static class OrderEndpoints
{
    public static void MapOrderEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/orders")
                       .WithTags("Orders");

        // GET /api/orders
        group.MapGet("/", GetAllOrders)
             .WithName("GetAllOrders")
             .WithSummary("Get all orders");

        // GET /api/orders/{id}
        group.MapGet("/{id:int}", GetOrderById)
             .WithName("GetOrderById")
             .WithSummary("Get order by ID");

        // POST /api/orders
        group.MapPost("/", CreateOrder)
             .WithName("CreateOrder")
             .WithSummary("Place a new order");

        // PATCH /api/orders/{id}/status
        group.MapPatch("/{id:int}/status", UpdateOrderStatus)
             .WithName("UpdateOrderStatus")
             .WithSummary("Update order status");
    }

    // ── Handlers ──────────────────────────────────────────────────

    static IResult GetAllOrders()
    {
        var orders = new[]
        {
            new { Id = 1, Product = "Product A", Status = "Shipped",   Total = 99.99 },
            new { Id = 2, Product = "Product B", Status = "Pending",   Total = 149.99 }
        };
        return Results.Ok(orders);
    }

    static IResult GetOrderById(int id)
    {
        var order = new { Id = id, Product = "Product A", Status = "Shipped", Total = 99.99 };
        return Results.Ok(order);
    }

    static IResult CreateOrder(CreateOrderRequest request)
    {
        // In real app: save order, trigger payment, send confirmation
        return Results.Created($"/api/orders/1", new { Id = 1, request.ProductId, Status = "Pending" });
    }

    static IResult UpdateOrderStatus(int id, UpdateStatusRequest request)
    {
        return Results.Ok(new { Id = id, request.Status });
    }
}

// ── Request / Response Models ─────────────────────────────────────
public record CreateOrderRequest(int ProductId, int Quantity);
public record UpdateStatusRequest(string Status);
