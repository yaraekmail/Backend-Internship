namespace CardiacPatientMonitoring.Api.Models;

// Represents the data required to register a new patient account.
public class RegisterRequest
{
    // Stores the patient's email address.
    public string Email { get; set; } = string.Empty;

    // Stores the patient's password.
    public string Password { get; set; } = string.Empty;

    // Stores the patient's first name.
    public string FirstName { get; set; } = string.Empty;

    // Stores the patient's last name.
    public string LastName { get; set; } = string.Empty;

    // Stores the patient's date of birth.
    public DateTime DateOfBirth { get; set; }

    // Stores the patient's gender.
    public string Gender { get; set; } = string.Empty;
}