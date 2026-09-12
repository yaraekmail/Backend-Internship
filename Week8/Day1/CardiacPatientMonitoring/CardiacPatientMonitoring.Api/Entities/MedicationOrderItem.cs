namespace CardiacPatientMonitoring.Api.Entities;

// Represents one medication item inside a medication order.
public class MedicationOrderItem
{
    // Unique identifier for the order item.
    public int Id { get; set; }

    // Identifies the medication order.
    public int MedicationOrderId { get; set; }

    // Identifies the medication from the catalog.
    public int MedicationCatalogItemId { get; set; }

    // Stores the requested quantity.
    public int Quantity { get; set; }

    // Stores the medication unit price at the time of the order.
    public decimal UnitPrice { get; set; }

    // Stores the calculated line total.
    public decimal LineTotal { get; set; }

    // Navigation property for the medication order.
    public MedicationOrder MedicationOrder { get; set; } = null!;

    // Navigation property for the catalog medication.
    public MedicationCatalogItem MedicationCatalogItem { get; set; } = null!;
}
