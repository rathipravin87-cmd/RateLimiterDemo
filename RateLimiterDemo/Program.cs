using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);
var PermitLimit = builder.Configuration.GetSection("RateLimiting").GetValue<int>("PermitLimit");
var WindowSeconds = builder.Configuration.GetSection("RateLimiting").GetValue<int>("WindowSeconds");
var QueueLimit = builder.Configuration.GetSection("RateLimiting").GetValue<int>("QueueLimit");

builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("dataLimiter", opt =>
    {
        opt.PermitLimit = PermitLimit; // Allow 5 requests
        opt.Window = TimeSpan.FromSeconds(WindowSeconds); // Per 10 seconds
        opt.QueueLimit = QueueLimit; // No queuing
        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    });

    // Custom JSON response when rejected
    options.OnRejected = async (context, cancellationToken) =>
    {
        context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
        context.HttpContext.Response.ContentType = "application/json";

        await context.HttpContext.Response.WriteAsJsonAsync(new
        {
            Allowed = false,
            Message = "Rate limit exceeded",
            Timestamp = DateTime.UtcNow
        }, cancellationToken);
    };
});


builder.Services.AddControllers();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // app.MapOpenApi();
}

app.UseAuthorization();
app.UseRateLimiter();

app.MapControllers();

app.Run();
