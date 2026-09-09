using CardiacPatientMonitoring.Api.DTOs;

namespace CardiacPatientMonitoring.Api.Services;

// Defines the operations supported by the medication order service.
public interface IMedicationOrderService
{
    // Creates a medication order using the required business rules.
    Task<MedicationOrderResponse> CreateOrderAsync(CreateMedicationOrderRequest request);
}