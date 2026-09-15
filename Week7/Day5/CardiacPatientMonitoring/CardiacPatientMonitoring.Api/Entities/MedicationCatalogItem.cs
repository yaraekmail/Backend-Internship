namespace CardiacPatientMonitoring.Api.Entities;

// Represents a medication available in the medication catalog.
public class MedicationCatalogItem
{
    // Unique identifier for the catalog medication.
    public int Id { get; set; }

    // Stores the medication name.
    public string Name { get; set; } = string.Empty;

    // Stores the price for one unit of the medication.
    public decimal UnitPrice { get; set; }

    // Stores the number of units currently available in stock.
    public int StockQuantity { get; set; }
    // Stores the database version used to detect concurrent updates.
public byte[] RowVersion { get; set; } = Array.Empty<byte>();
}
