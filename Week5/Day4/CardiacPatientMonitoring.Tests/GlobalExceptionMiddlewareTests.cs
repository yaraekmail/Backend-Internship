// Provides HTTP status codes such as InternalServerError.
using System.Net;

// Provides HTTP authorization header support.
using System.Net.Http.Headers;

// Provides JSON serialization and deserialization.
using System.Net.Http.Json;

// Provides the WebApplicationFactory used to create a test version of the API.
using Microsoft.AspNetCore.Mvc.Testing;

// Provides xUnit testing features.
using Xunit;

namespace CardiacPatientMonitoring.Tests;

// Contains integration tests for global exception handling.
public class GlobalExceptionMiddlewareTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    // Creates an HTTP client connected to the test version of the API.
    public GlobalExceptionMiddlewareTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task TestError_ReturnsInternalServerError_WithSafeProblemDetails()
    {
        // Sends a request to the endpoint that deliberately throws an exception.
        var response = await _client.GetAsync("/api/test-error");

        // Confirms that the global middleware returned HTTP 500.
        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);

        // Reads the response body as text.
        var responseBody = await response.Content.ReadAsStringAsync();

        // Confirms that the real exception message is not exposed to the client.
        Assert.DoesNotContain("This is a test exception.", responseBody);

        // Confirms that a stack trace is not exposed to the client.
        Assert.DoesNotContain("System.Exception", responseBody);

        // Confirms that the response uses the ProblemDetails title.
        Assert.Contains("An unexpected error occurred.", responseBody);

        // Confirms that the response contains the 500 status.
        Assert.Contains("500", responseBody);
    }
}