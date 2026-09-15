namespace CardiacPatientMonitoring.Api.DTOs;

// Represents the data required to update an existing appointment.
public class UpdateAppointmentRequest
{
    // Stores the date and time of the appointment.
    public DateTime AppointmentDate { get; set; }

    // Stores the reason or purpose of the appointment.
    public string Reason { get; set; } = string.Empty;

    // Stores the current status of the appointment.
    public string Status { get; set; } = string.Empty;

    // Stores additional notes about the appointment.
    public string? Notes { get; set; }
}
