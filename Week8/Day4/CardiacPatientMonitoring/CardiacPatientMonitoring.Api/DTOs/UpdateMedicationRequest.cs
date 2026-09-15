namespace CardiacPatientMonitoring.Api.DTOs;

// Represents the data required to update an existing medication record.
public class UpdateMedicationRequest
{
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
}
