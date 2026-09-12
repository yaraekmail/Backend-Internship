namespace CardiacPatientMonitoring.Api.DTOs;

// Represents the data required to create a medication catalog item.
public class CreateMedicationCatalogItemRequest
{
    // Stores the medication name.
    public string Name { get; set; } = string.Empty;

    // Stores the price for one unit of the medication.
    public decimal UnitPrice { get; set; }

    // Stores the number of units currently available in stock.
    public int StockQuantity { get; set; }
}