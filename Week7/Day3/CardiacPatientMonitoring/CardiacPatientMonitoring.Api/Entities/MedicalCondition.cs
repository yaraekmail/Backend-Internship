namespace CardiacPatientMonitoring.Api.Entities;

// Represents a medical condition associated with a patient.
public class MedicalCondition
{
    // Unique identifier for the medical condition record.
    public int Id { get; set; }

    // Foreign key that identifies the patient with this condition.
public Guid PatientId { get; set; }

    // Stores the name of the medical condition.
    public string Name { get; set; } = string.Empty;

    // Stores the date when the condition was diagnosed.
    public DateTime DiagnosedAt { get; set; }

    // Stores additional information about the condition.
    public string? Notes { get; set; }

    // Navigation property that connects the condition to its patient.
    public Patient Patient { get; set; } = null!;
}