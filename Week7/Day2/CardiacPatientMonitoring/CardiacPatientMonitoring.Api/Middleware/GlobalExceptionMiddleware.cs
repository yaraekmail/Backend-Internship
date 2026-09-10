// Provides access to HTTP request and response information.
using Microsoft.AspNetCore.Http;

// Provides the ProblemDetails class for standardized API errors.
using Microsoft.AspNetCore.Mvc;

// Provides logging functionality through ILogger.
using Microsoft.Extensions.Logging;

namespace CardiacPatientMonitoring.Api.Middleware;

// Handles unexpected exceptions for the whole API.
public class GlobalExceptionMiddleware
{
    // Represents the next middleware in the request pipeline.
    private readonly RequestDelegate _next;

    // Used to record exception details on the server.
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    // Receives the next middleware and the logger through dependency injection.
    public GlobalExceptionMiddleware(
        RequestDelegate next,
        ILogger<GlobalExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    // Runs for every HTTP request that passes through this middleware.
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // Passes the request to the next part of the pipeline.
            await _next(context);
        }
        catch (Exception ex)
        {
            // Logs the full exception details on the server.
            _logger.LogError(
                ex,
                "Unhandled exception while processing request {RequestPath}",
                context.Request.Path);

            // Returns a safe standardized error response to the client.
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            // Tells the client that the response is JSON.
            context.Response.ContentType = "application/problem+json";

            // Creates the standardized ProblemDetails response.
            var problemDetails = new ProblemDetails
            {
                Title = "An unexpected error occurred.",
                Status = StatusCodes.Status500InternalServerError
            };

            // Sends ProblemDetails to the client.
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }
}