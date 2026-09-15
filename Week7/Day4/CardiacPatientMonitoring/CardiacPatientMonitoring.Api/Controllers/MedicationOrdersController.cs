using CardiacPatientMonitoring.Api.DTOs;
using CardiacPatientMonitoring.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CardiacPatientMonitoring.Api.Controllers;

// Handles medication order operations.
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MedicationOrdersController : ControllerBase
{
    private readonly IMedicationOrderService _orderService;

    // Receives the medication order service through dependency injection.
    public MedicationOrdersController(IMedicationOrderService orderService)
    {
        _orderService = orderService;
    }

    // Creates a new medication order.
    [HttpPost]
    public async Task<ActionResult<MedicationOrderResponse>> CreateOrder(
        CreateMedicationOrderRequest request)
    {
        // Allow Admin users to create orders for any patient.
        if (!User.IsInRole("Admin"))
        {
            var patientIdClaim = User.FindFirst("patientId")?.Value;

            if (!Guid.TryParse(patientIdClaim, out var currentPatientId))
            {
                return Forbid();
            }

            if (currentPatientId != request.PatientId)
            {
                return Forbid();
            }
        }

        try
        {
            // Create the order using the business logic in the service.
            var response = await _orderService.CreateOrderAsync(request);

            // Return the created order.
            return Ok(response);
        }
        catch (InvalidOperationException exception)
        {
            // Return a bad request when a business rule is violated.
            return BadRequest(new
            {
                message = exception.Message
            });
        }
    }
}