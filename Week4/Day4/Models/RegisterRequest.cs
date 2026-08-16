// Represents the data required to register a new user.
public class RegisterRequest
{
    // User's email address.
    public string Email { get; set; } = string.Empty;

    // User's password.
    public string Password { get; set; } = string.Empty;
}