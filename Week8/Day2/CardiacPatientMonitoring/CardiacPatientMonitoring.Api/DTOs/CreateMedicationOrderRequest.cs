namespace CardiacPatientMonitoring.Api.DTOs;

// Represents the data required to create a medication order.
public class CreateMedicationOrderRequest
{
    // Identifies the patient who owns the order.
    public Guid PatientId { get; set; }

    // Stores the medications requested in the order.
    public List<CreateMedicationOrderItemRequest> Items { get; set; } = new();
}

// Represents one medication requested in the order.
public class CreateMedicationOrderItemRequest
{
    // Identifies the medication from the catalog.
    public int MedicationCatalogItemId { get; set; }

    // Stores the requested quantity.
    public int Quantity { get; set; }
}
