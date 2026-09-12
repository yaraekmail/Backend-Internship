namespace CardiacPatientMonitoring.Api.DTOs;

// Represents the data required to create a new patient.
public class CreatePatientRequest
{
    // Stores the patient's first name.
    public string FirstName { get; set; } = string.Empty;

    // Stores the patient's last name.
    public string LastName { get; set; } = string.Empty;

    // Stores the patient's date of birth.
    public DateTime DateOfBirth { get; set; }

    // Stores the patient's gender.
    public string Gender { get; set; } = string.Empty;

    // Stores the patient's phone number.
    public string? Phone { get; set; }

    // Stores the patient's email address.
    public string? Email { get; set; }

    // Stores the patient's address.
    public string? Address { get; set; }

    // Stores the patient's city.
    public string? City { get; set; }

    // Stores the patient's state or region.
    public string? State { get; set; }
}