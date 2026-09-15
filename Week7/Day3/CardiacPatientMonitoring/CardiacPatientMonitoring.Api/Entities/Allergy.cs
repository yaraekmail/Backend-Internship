namespace CardiacPatientMonitoring.Api.Entities;

// Represents an allergy associated with a patient.
public class Allergy
{
    // Unique identifier for the allergy record.
    public int Id { get; set; }

    // Foreign key that identifies the patient with this allergy.
public Guid PatientId { get; set; }
    // Stores the name of the allergen.
    public string Allergen { get; set; } = string.Empty;

    // Stores the severity of the allergic reaction.
    public string Severity { get; set; } = string.Empty;

    // Stores additional information about the allergy.
    public string? Notes { get; set; }

    // Navigation property that connects the allergy to its patient.
    public Patient Patient { get; set; } = null!;
}