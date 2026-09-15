namespace CardiacPatientMonitoring.Api.DTOs;

// Represents the data required to update a medication catalog item.
public class UpdateMedicationCatalogItemRequest
{
    // Stores the medication name.
    public string Name { get; set; } = string.Empty;

    // Stores the price for one unit of the medication.
    public decimal UnitPrice { get; set; }

    // Stores the number of units currently available in stock.
    public int StockQuantity { get; set; }

    // Stores the database version used for concurrency checking.
    public byte[] RowVersion { get; set; } = Array.Empty<byte>();
}