using CardiacPatientMonitoring.Api.Data;
using CardiacPatientMonitoring.Api.DTOs;
using CardiacPatientMonitoring.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace CardiacPatientMonitoring.Api.Services;

// Implements the business logic for creating medication orders.
public class MedicationOrderService : IMedicationOrderService
{
    // Provides access to the application database.
    private readonly CardiacPatientMonitoringDbContext _context;

    // Initializes the service with the database context.
    public MedicationOrderService(CardiacPatientMonitoringDbContext context)
    {
        _context = context;
    }

    // Creates a medication order using the required business rules.
    public async Task<MedicationOrderResponse> CreateOrderAsync(
        CreateMedicationOrderRequest request)
    {
        // Check that the requested patient exists.
        var patientExists = await _context.Patients
            .AnyAsync(p => p.Id == request.PatientId);

        if (!patientExists)
        {
            throw new InvalidOperationException("Patient not found.");
        }

        // Check that the order contains at least one medication item.
        if (request.Items == null || request.Items.Count == 0)
        {
            throw new InvalidOperationException(
                "The order must contain at least one medication.");
        }

        // Check that every requested quantity is greater than zero.
        if (request.Items.Any(item => item.Quantity <= 0))
        {
            throw new InvalidOperationException(
                "Medication quantity must be greater than zero.");
        }

        // Get all requested medications from the catalog.
        var medicationIds = request.Items
            .Select(item => item.MedicationCatalogItemId)
            .ToList();

        var catalogItems = await _context.MedicationCatalogItems
            .Where(item => medicationIds.Contains(item.Id))
            .ToListAsync();

        // Check that all requested medications exist in the catalog.
        if (catalogItems.Count != medicationIds.Count)
        {
            throw new InvalidOperationException(
                "One or more medications were not found in the catalog.");
        }

        // Check that enough stock is available for every requested medication.
        foreach (var requestedItem in request.Items)
        {
            var catalogItem = catalogItems
                .First(item => item.Id == requestedItem.MedicationCatalogItemId);

            if (catalogItem.StockQuantity < requestedItem.Quantity)
            {
                throw new InvalidOperationException(
                    "Insufficient stock for medication: " + catalogItem.Name);
            }
        }

        // Create a new medication order for the patient.
        var order = new MedicationOrder
        {
            PatientId = request.PatientId,
            OrderDate = DateTime.UtcNow,
            TotalAmount = 0
        };

        // Create the order items and calculate their line totals.
        foreach (var requestedItem in request.Items)
        {
            var catalogItem = catalogItems
                .First(item => item.Id == requestedItem.MedicationCatalogItemId);

            // Calculate the total price for this medication line.
            var lineTotal = catalogItem.UnitPrice * requestedItem.Quantity;

            // Create an order item using the server-side catalog price.
            var orderItem = new MedicationOrderItem
            {
                MedicationCatalogItemId = catalogItem.Id,
                Quantity = requestedItem.Quantity,
                UnitPrice = catalogItem.UnitPrice,
                LineTotal = lineTotal
            };

            // Add the item to the order.
            order.Items.Add(orderItem);

            // Add the line total to the order total.
            order.TotalAmount += lineTotal;
        }
        // Start a database transaction for the complete order operation.
        await using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            // Add the new order and its items to the database.
            _context.MedicationOrders.Add(order);

            // Decrease the stock for every ordered medication.
            foreach (var requestedItem in request.Items)
            {
                var catalogItem = catalogItems
                    .First(item => item.Id == requestedItem.MedicationCatalogItemId);

                catalogItem.StockQuantity -= requestedItem.Quantity;
            }

            // Save the order, order items, and stock changes together.
            await _context.SaveChangesAsync();
            // Commit the transaction after all database operations succeed.
            await transaction.CommitAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            // Roll back the transaction when another request changed the stock.
            await transaction.RollbackAsync();

            throw new InvalidOperationException(
                "The medication stock was changed by another order. Please try again.");
        }
        catch
        {
            // Roll back all changes when any other database operation fails.
            await transaction.RollbackAsync();

            throw;
        }
        // Return the created order as a response DTO.
        return new MedicationOrderResponse
        {
            Id = order.Id,
            PatientId = order.PatientId,
            OrderDate = order.OrderDate,
            TotalAmount = order.TotalAmount,

            // Map each order item to the response DTO.
            Items = order.Items.Select(item => new MedicationOrderItemResponse
            {
                MedicationCatalogItemId = item.MedicationCatalogItemId,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                LineTotal = item.LineTotal
            }).ToList()
        };
    }
}