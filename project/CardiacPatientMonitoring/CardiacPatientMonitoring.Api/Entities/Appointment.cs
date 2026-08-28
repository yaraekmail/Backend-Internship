namespace CardiacPatientMonitoring.Api.Entities;

// Represents a scheduled appointment for a patient.
public class Appointment
{
    // Unique identifier for the appointment.
    public int Id { get; set; }

    // Foreign key that identifies the patient associated with the appointment.
   public Guid PatientId { get; set; }

    // Stores the date and time of the appointment.
    public DateTime AppointmentDate { get; set; }

    // Stores the reason or purpose of the appointment.
    public string Reason { get; set; } = string.Empty;

    // Stores the current status of the appointment.
    public string Status { get; set; } = string.Empty;

    // Stores additional notes about the appointment.
    public string? Notes { get; set; }

    // Navigation property that connects the appointment to its patient.
    public Patient Patient { get; set; } = null!;
}