// Represents the data sent by the client when attempting to log in.
public class LoginRequest
{
    // Stores the email address entered by the user.
    public string Email { get; set; } = string.Empty;

    // Stores the password entered by the user.
    public string Password { get; set; } = string.Empty;
}