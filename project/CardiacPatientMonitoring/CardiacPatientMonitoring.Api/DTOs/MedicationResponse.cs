namespace CardiacPatientMonitoring.Api.DTOs;

// Represents medication data returned by the API.
public class MedicationResponse
{
    // Stores the unique identifier of the medication record.
    public int Id { get; set; }

    // Identifies the patient using this medication.
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
}
