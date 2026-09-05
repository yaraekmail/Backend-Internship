// Provides HTTP status codes such as OK and NotFound.
using System.Net;

// Provides JSON serialization and deserialization.
using System.Net.Http.Json;

// Provides the WebApplicationFactory used to create a test version of the API.
using Microsoft.AspNetCore.Mvc.Testing;

// Provides the PatientResponse DTO from the API project.
using CardiacPatientMonitoring.Api.DTOs;

// Provides xUnit testing features.
using Xunit;

namespace CardiacPatientMonitoring.Tests;

// Contains integration tests for the Patients API.
public class PatientsApiIntegrationTests : IClassFixture<CustomWebApplicationFactory>
{
    // Represents a client that sends HTTP requests to the test API.
    private readonly HttpClient _client;

    // Receives the test API factory from xUnit.
    public PatientsApiIntegrationTests(CustomWebApplicationFactory factory)
    {
        // Creates an HttpClient connected to the test version of the API.
        _client = factory.CreateClient();
    }

    // Tests that a valid JWT token allows access to a protected patient endpoint.
    [Fact]
    public async Task GetPatient_ReturnsOk_WhenUserIsAuthenticated()
    {
        // Sends valid credentials for the seeded Admin user.
        var loginResponse = await _client.PostAsJsonAsync(
            "/api/Auth/login",
            new
            {
                email = "admin@cardiac.local",
                password = "Admin123!"
            });

        // Makes sure the login request succeeded.
        Assert.Equal(HttpStatusCode.OK, loginResponse.StatusCode);

        // Reads the login response as JSON.
        var loginResult = await loginResponse.Content
            .ReadFromJsonAsync<LoginResponse>();

        // Makes sure the API returned a JWT token.
        Assert.NotNull(loginResult);
        Assert.False(string.IsNullOrWhiteSpace(loginResult!.Token));

        // Adds the JWT token to the Authorization header.
        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue(
                "Bearer",
                loginResult.Token);

        // This is the ID of John Smith from the seeded test data.
        var patientId = Guid.Parse(
            "11111111-1111-1111-1111-111111111111");

        // Sends an authenticated GET request to the protected Patients endpoint.
        var response = await _client.GetAsync(
            $"/api/Patients/{patientId}");

        // Checks that the authenticated request was successful.
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        // Reads the patient returned by the API.
        var patient = await response.Content
            .ReadFromJsonAsync<PatientResponse>();

        // Makes sure the API returned a patient.
        Assert.NotNull(patient);

        // Checks that the returned patient has the expected ID.
        Assert.Equal(patientId, patient!.Id);

        // Checks that the returned patient's first name is correct.
        Assert.Equal("John", patient.FirstName);

        // Checks that the returned patient's last name is correct.
        Assert.Equal("Smith", patient.LastName);
    }

    // Tests that the API returns Not Found when the patient does not exist.
    [Fact]
    public async Task GetPatient_ReturnsNotFound_WhenPatientDoesNotExist()
    {
        // Logs in first because the Patients endpoint requires authentication.
        var loginResponse = await _client.PostAsJsonAsync(
            "/api/Auth/login",
            new
            {
                email = "admin@cardiac.local",
                password = "Admin123!"
            });

        // Makes sure the login request succeeded.
        Assert.Equal(HttpStatusCode.OK, loginResponse.StatusCode);

        // Reads the JWT response.
        var loginResult = await loginResponse.Content
            .ReadFromJsonAsync<LoginResponse>();

        // Makes sure a token was returned.
        Assert.NotNull(loginResult);
        Assert.False(string.IsNullOrWhiteSpace(loginResult!.Token));

        // Adds the JWT token to the Authorization header.
        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue(
                "Bearer",
                loginResult.Token);

        // Creates an ID that should not exist in the seeded patient data.
        var patientId = Guid.NewGuid();

        // Sends an authenticated request for a patient that does not exist.
        var response = await _client.GetAsync(
            $"/api/Patients/{patientId}");

        // Checks that the API returned HTTP 404 Not Found.
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    // Represents the JWT response returned by the login endpoint.
    private class LoginResponse
    {
        // Stores the JWT token.
        public string Token { get; set; } = string.Empty;

        // Stores the token expiration time.
        public DateTime Expiration { get; set; }
    }
}