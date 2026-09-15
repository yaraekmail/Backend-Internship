namespace CardiacPatientMonitoring.Api.Models;

// Represents the data required to authenticate a user.
public class LoginRequest
{
    // Stores the user's email address.
    public string Email { get; set; } = string.Empty;

    // Stores the user's password.
    public string Password { get; set; } = string.Empty;
}