namespace CardiacPatientMonitoring.Api.Entities;
// Represents a vital-sign measurement recorded for a patient.
public class VitalSign
{
    // Unique identifier for the vital-sign record.
    public int Id { get; set; }

    // Foreign key that identifies the patient who owns this measurement.
    public Guid PatientId { get; set; }

    // Stores the date and time when the measurement was recorded.
    public DateTime RecordedAt { get; set; }

    // Stores the patient's heart rate in beats per minute.
    public int HeartRate { get; set; }

    // Stores the systolic blood pressure value.
    public int SystolicBloodPressure { get; set; }

    // Stores the diastolic blood pressure value.
    public int DiastolicBloodPressure { get; set; }

    // Stores the patient's oxygen saturation percentage.
    public decimal OxygenSaturation { get; set; }

    // Stores the patient's body temperature.
    public decimal Temperature { get; set; }

    // Stores the patient's respiratory rate.
    public int RespiratoryRate { get; set; }

    // Navigation property that connects the vital sign to its patient.
    public Patient Patient { get; set; } = null!;
}