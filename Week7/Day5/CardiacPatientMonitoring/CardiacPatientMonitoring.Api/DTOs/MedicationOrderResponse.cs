namespace CardiacPatientMonitoring.Api.DTOs;

// Represents the medication order returned by the API.
public class MedicationOrderResponse
{
    // Unique identifier for the order.
    public int Id { get; set; }

    // Identifies the patient who owns the order.
    public Guid PatientId { get; set; }

    // Stores the date and time when the order was created.
    public DateTime OrderDate { get; set; }

    // Stores the calculated total amount of the order.
    public decimal TotalAmount { get; set; }

    // Stores the medications included in the order.
    public List<MedicationOrderItemResponse> Items { get; set; } = new();
}

// Represents one medication item returned inside the order.
public class MedicationOrderItemResponse
{
    // Identifies the catalog medication.
    public int MedicationCatalogItemId { get; set; }

    // Stores the requested quantity.
    public int Quantity { get; set; }

    // Stores the medication unit price.
    public decimal UnitPrice { get; set; }

    // Stores the calculated line total.
    public decimal LineTotal { get; set; }
}
