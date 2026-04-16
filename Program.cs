// ─────────────────────────────────────────────────────────────────
// Program.cs — Entry point for your .NET 8 Web API
// This is the equivalent of "if __name__ == '__main__'" in Python
// ─────────────────────────────────────────────────────────────────

var builder = WebApplication.CreateBuilder(args);

// ── Register Services (Dependency Injection) ──────────────────────
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Health check endpoint (used by CI/CD post-deploy checks)
builder.Services.AddHealthChecks();

var app = builder.Build();

// ── Middleware Pipeline ───────────────────────────────────────────
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Health check route — hit this after deploy to verify the app is up
app.MapHealthChecks("/health");
//Helper endpoint to verify the app is running and provide basic info
// ── Root endpoint (quick sanity check) ───────────────────────────
app.MapGet("/", () => new
{
    status = "running",
    app = "ATIE - Adaptive Threat Intelligence Engine",
    version = "1.0.0",
    environment = app.Environment.EnvironmentName
});

// ── Entry Point ───────────────────────────────────────────────────
// This is where your app starts — equivalent to Python's:
// if __name__ == "__main__":
//     main()
app.Run();
