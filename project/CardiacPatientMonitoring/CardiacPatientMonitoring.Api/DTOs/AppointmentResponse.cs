namespace CardiacPatientMonitoring.Api.DTOs;

// Represents appointment data returned by the API.
public class AppointmentResponse
{
    // Stores the unique identifier of the appointment.
    public int Id { get; set; }

    // Identifies the patient associated with the appointment.
    public Guid PatientId { get; set; }

    // Stores the date and time of the appointment.
    public DateTime AppointmentDate { get; set; }

    // Stores the reason or purpose of the appointment.
    public string Reason { get; set; } = string.Empty;

    // Stores the current status of the appointment.
    public string Status { get; set; } = string.Empty;

    // Stores additional notes about the appointment.
    public string? Notes { get; set; }
}
