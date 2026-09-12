using System.Diagnostics;
// Gives us access to Stopwatch, which measures how long the request takes.

namespace CardiacPatientMonitoring.Api.Middleware;
// Defines the namespace where our custom middleware belongs.

// Tracks each request with a correlation ID and measures its execution time.
public class RequestTrackingMiddleware
{
    // Stores the next middleware in the ASP.NET Core request pipeline.
    private readonly RequestDelegate _next;

    // Stores the logger that will write request information to the application logs.
    private readonly ILogger<RequestTrackingMiddleware> _logger;

    // Constructor used by ASP.NET Core Dependency Injection.
    public RequestTrackingMiddleware(
        RequestDelegate next,
        ILogger<RequestTrackingMiddleware> logger)
    {
        // Saves the next middleware so we can continue the request pipeline.
        _next = next;

        // Saves the logger so we can write information to the logs.
        _logger = logger;
    }

    // ASP.NET Core calls this method for every request that reaches this middleware.
    public async Task InvokeAsync(HttpContext context)
    {
        // Creates a unique ID for this specific request.
        var correlationId = Guid.NewGuid().ToString();

        // Adds the correlation ID to the response header so the client can see it.
        context.Response.Headers["X-Correlation-ID"] = correlationId;

        // Starts measuring how long the request takes.
        var stopwatch = Stopwatch.StartNew();

        // Passes the request to the next middleware/controller.
        await _next(context);

        // Stops the timer after the request has finished.
        stopwatch.Stop();

        // Writes the request details to the application log.
        _logger.LogInformation(
            "Correlation ID: {CorrelationId} | {Method} {Path} | Status Code: {StatusCode} | Elapsed: {ElapsedMilliseconds} ms",

            // The unique ID of this request.
            correlationId,

            // The HTTP method, such as GET, POST, PUT, or DELETE.
            context.Request.Method,

            // The requested API path.
            context.Request.Path,

            // The HTTP response status code, such as 200, 403, or 404.
            context.Response.StatusCode,

            // The total time taken by the request in milliseconds.
            stopwatch.ElapsedMilliseconds);
    }
}