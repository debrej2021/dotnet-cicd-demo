// ─────────────────────────────────────────────────────────────────
// ProductEndpoints.cs
// All /api/products routes live here — Program.cs knows nothing
// about the individual routes, only that products exist.
// ─────────────────────────────────────────────────────────────────

namespace App_with_yml_dot.Endpoints;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        // Group all product routes under /api/products
        var group = app.MapGroup("/api/products")
                       .WithTags("Products");       // groups in Swagger UI

        // GET /api/products
        group.MapGet("/", GetAllProducts)
             .WithName("GetAllProducts")
             .WithSummary("Get all products");

        // GET /api/products/{id}
        group.MapGet("/{id:int}", GetProductById)
             .WithName("GetProductById")
             .WithSummary("Get product by ID");

        // POST /api/products
        group.MapPost("/", CreateProduct)
             .WithName("CreateProduct")
             .WithSummary("Create a new product");

        // PUT /api/products/{id}
        group.MapPut("/{id:int}", UpdateProduct)
             .WithName("UpdateProduct")
             .WithSummary("Update a product");

        // DELETE /api/products/{id}
        group.MapDelete("/{id:int}", DeleteProduct)
             .WithName("DeleteProduct")
             .WithSummary("Delete a product");
    }

    // ── Handlers (keep them here, close to the routes) ────────────

    static IResult GetAllProducts()
    {
        var products = new[]
        {
            new { Id = 1, Name = "Product A", Price = 99.99 },
            new { Id = 2, Name = "Product B", Price = 149.99 }
        };
        return Results.Ok(products);
    }

    static IResult GetProductById(int id)
    {
        var product = new { Id = id, Name = $"Product {id}", Price = 99.99 };
        return Results.Ok(product);
    }

    static IResult CreateProduct(CreateProductRequest request)
    {
        // In real app: save to database
        return Results.Created($"/api/products/1", new { Id = 1, request.Name, request.Price });
    }

    static IResult UpdateProduct(int id, CreateProductRequest request)
    {
        // In real app: update in database
        return Results.Ok(new { Id = id, request.Name, request.Price });
    }

    static IResult DeleteProduct(int id)
    {
        // In real app: delete from database
        return Results.NoContent();
    }
}

// ── Request / Response Models ─────────────────────────────────────
public record CreateProductRequest(string Name, double Price);
