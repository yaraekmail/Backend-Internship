namespace CardiacPatientMonitoring.Api.Entities;

// Represents a patient in the cardiac monitoring system.
public class Patient
{
    // Unique identifier for the patient.
    // Guid matches the identifier format used by Synthea.
    public Guid Id { get; set; }

    // Stores the patient's first name.
    public string FirstName { get; set; } = string.Empty;

    // Stores the patient's last name.
    public string LastName { get; set; } = string.Empty;

    // Stores the patient's date of birth.
    public DateTime DateOfBirth { get; set; }

    // Stores the patient's gender.
    public string Gender { get; set; } = string.Empty;

    // Stores the patient's phone number when available.
    public string? Phone { get; set; }

    // Stores the patient's email address when available.
    public string? Email { get; set; }

    // Stores the patient's address.
    public string? Address { get; set; }

    // Stores the patient's city.
    public string? City { get; set; }

    // Stores the patient's state.
    public string? State { get; set; }

    // Collection of vital-sign measurements recorded for the patient.
    public ICollection<VitalSign> VitalSigns { get; set; } = new List<VitalSign>();

    // Collection of medications associated with the patient.
    public ICollection<Medication> Medications { get; set; } = new List<Medication>();

    // Collection of appointments scheduled for the patient.
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    // Collection of medical conditions associated with the patient.
    public ICollection<MedicalCondition> MedicalConditions { get; set; } = new List<MedicalCondition>();

    // Collection of allergies associated with the patient.
    public ICollection<Allergy> Allergies { get; set; } = new List<Allergy>();
}