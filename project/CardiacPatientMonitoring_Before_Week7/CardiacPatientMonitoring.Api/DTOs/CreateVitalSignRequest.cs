namespace CardiacPatientMonitoring.Api.DTOs;

// Represents the data required to create a new vital-sign measurement.
public class CreateVitalSignRequest
{
    // Identifies the patient who owns this measurement.
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
}
