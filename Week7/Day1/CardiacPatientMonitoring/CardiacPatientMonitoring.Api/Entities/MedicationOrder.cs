namespace CardiacPatientMonitoring.Api.Entities;

// Represents a medication order created for a patient.
public class MedicationOrder
{
    // Unique identifier for the medication order.
    public int Id { get; set; }

    // Identifies the patient who owns this order.
    public Guid PatientId { get; set; }

    // Stores the date and time when the order was created.
    public DateTime OrderDate { get; set; }

    // Stores the calculated total amount of the order.
    public decimal TotalAmount { get; set; }

    // Navigation property for the patient.
    public Patient Patient { get; set; } = null!;

    // Stores the medication items included in this order.
    public ICollection<MedicationOrderItem> Items { get; set; } = new List<MedicationOrderItem>();
}
