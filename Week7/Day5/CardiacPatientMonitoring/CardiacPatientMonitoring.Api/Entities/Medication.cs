namespace CardiacPatientMonitoring.Api.Entities;

// Represents a medication prescribed or used by a patient.
public class Medication
{
    // Unique identifier for the medication record.
    public int Id { get; set; }

    // Foreign key that identifies the patient using this medication.
public Guid PatientId { get; set; }
    // Stores the medication name.
    public string Name { get; set; } = string.Empty;

    // Stores the prescribed dosage.
    public string Dosage { get; set; } = string.Empty;

    // Stores how often the medication should be taken.
    public string Frequency { get; set; } = string.Empty;

    // Stores the date when the medication was started.
    public DateTime StartDate { get; set; }

    // Stores the date when the medication was stopped, if applicable.
    public DateTime? EndDate { get; set; }

    // Navigation property that connects the medication to its patient.
    public Patient Patient { get; set; } = null!;
}