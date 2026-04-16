// ─────────────────────────────────────────────────────────────────
// Program.cs — Clean entry point
// No matter how many APIs you add, this file NEVER grows.
// Each feature registers itself via extension methods.
// ─────────────────────────────────────────────────────────────────

using App_with_yml_dot.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// ── Services ──────────────────────────────────────────────────────
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();

var app = builder.Build();

// ── Middleware ────────────────────────────────────────────────────
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ── Health Check ──────────────────────────────────────────────────
app.MapHealthChecks("/health");

// ── API Route Groups ──────────────────────────────────────────────
// This is ALL you ever add here — one line per feature.
// Each feature manages its own routes internally.
app.MapProductEndpoints();
app.MapOrderEndpoints();
app.MapUserEndpoints();
// app.MapPaymentEndpoints();     ← just uncomment when ready
// app.MapReportEndpoints();      ← just uncomment when ready
// app.MapNotificationEndpoints();

// ─────────────────────────────────────────────────────────────────
app.Run();
