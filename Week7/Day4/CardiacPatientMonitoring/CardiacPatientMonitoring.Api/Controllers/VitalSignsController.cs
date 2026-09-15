using CardiacPatientMonitoring.Api.Data;
using CardiacPatientMonitoring.Api.DTOs;
using CardiacPatientMonitoring.Api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
namespace CardiacPatientMonitoring.Api.Controllers;

// Handles CRUD operations for vital-sign measurements.
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class VitalSignsController : ControllerBase
{
    private readonly CardiacPatientMonitoringDbContext _context;
private readonly IValidator<CreateVitalSignRequest> _createVitalSignValidator;
private readonly IValidator<UpdateVitalSignRequest> _updateVitalSignValidator;
    // Receives the database context and validators through dependency injection.
    public VitalSignsController
    (CardiacPatientMonitoringDbContext context,
     IValidator<CreateVitalSignRequest> createVitalSignValidator,
     IValidator<UpdateVitalSignRequest> updateVitalSignValidator)
    {
        _context = context;
        _createVitalSignValidator = createVitalSignValidator;
        _updateVitalSignValidator = updateVitalSignValidator;
    }

    // Returns all vital-sign measurements.
    [HttpGet]
    public async Task<ActionResult<IEnumerable<VitalSignResponse>>> GetVitalSigns()
    {
        var vitalSigns = await _context.VitalSigns
            .AsNoTracking()
            .Select(vitalSign => new VitalSignResponse
            {
                Id = vitalSign.Id,
                PatientId = vitalSign.PatientId,
                RecordedAt = vitalSign.RecordedAt,
                HeartRate = vitalSign.HeartRate,
                SystolicBloodPressure = vitalSign.SystolicBloodPressure,
                DiastolicBloodPressure = vitalSign.DiastolicBloodPressure,
                OxygenSaturation = vitalSign.OxygenSaturation,
                Temperature = vitalSign.Temperature,
                RespiratoryRate = vitalSign.RespiratoryRate
            })
            .ToListAsync();

        return Ok(vitalSigns);
    }

    // Returns one vital-sign measurement by ID.
    [HttpGet("{id:int}")]
    public async Task<ActionResult<VitalSignResponse>> GetVitalSign(int id)
    {
        var vitalSign = await _context.VitalSigns
            .AsNoTracking()
            .Where(vitalSign => vitalSign.Id == id)
            .Select(vitalSign => new VitalSignResponse
            {
                Id = vitalSign.Id,
                PatientId = vitalSign.PatientId,
                RecordedAt = vitalSign.RecordedAt,
                HeartRate = vitalSign.HeartRate,
                SystolicBloodPressure = vitalSign.SystolicBloodPressure,
                DiastolicBloodPressure = vitalSign.DiastolicBloodPressure,
                OxygenSaturation = vitalSign.OxygenSaturation,
                Temperature = vitalSign.Temperature,
                RespiratoryRate = vitalSign.RespiratoryRate
            })
            .FirstOrDefaultAsync();

        if (vitalSign is null)
        {
            return NotFound(new
            {
                message = "Vital sign not found."
            });
        }

        return Ok(vitalSign);
    }

    // Returns all vital-sign measurements for a specific patient.
    [HttpGet("patient/{patientId:guid}")]
    public async Task<ActionResult<IEnumerable<VitalSignResponse>>> GetPatientVitalSigns(
        Guid patientId)
    {
        var patientExists = await _context.Patients
            .AnyAsync(patient => patient.Id == patientId);

        if (!patientExists)
        {
            return NotFound(new
            {
                message = "Patient not found."
            });
        }

        var vitalSigns = await _context.VitalSigns
            .AsNoTracking()
            .Where(vitalSign => vitalSign.PatientId == patientId)
            .OrderByDescending(vitalSign => vitalSign.RecordedAt)
            .Select(vitalSign => new VitalSignResponse
            {
                Id = vitalSign.Id,
                PatientId = vitalSign.PatientId,
                RecordedAt = vitalSign.RecordedAt,
                HeartRate = vitalSign.HeartRate,
                SystolicBloodPressure = vitalSign.SystolicBloodPressure,
                DiastolicBloodPressure = vitalSign.DiastolicBloodPressure,
                OxygenSaturation = vitalSign.OxygenSaturation,
                Temperature = vitalSign.Temperature,
                RespiratoryRate = vitalSign.RespiratoryRate
            })
            .ToListAsync();

        return Ok(vitalSigns);
    }

    // Creates a new vital-sign measurement.
    [HttpPost]
    public async Task<ActionResult<VitalSignResponse>> CreateVitalSign(
        CreateVitalSignRequest request)
    {  // Validates the incoming create request.
        var validationResult = await _createVitalSignValidator.ValidateAsync(request);

if (!validationResult.IsValid)
{
    return BadRequest(validationResult.Errors);
}
        var patientExists = await _context.Patients
            .AnyAsync(patient => patient.Id == request.PatientId);

        if (!patientExists)
        {
            return NotFound(new
            {
                message = "Patient not found."
            });
        }

        var vitalSign = new VitalSign
        {
            PatientId = request.PatientId,
            RecordedAt = request.RecordedAt,
            HeartRate = request.HeartRate,
            SystolicBloodPressure = request.SystolicBloodPressure,
            DiastolicBloodPressure = request.DiastolicBloodPressure,
            OxygenSaturation = request.OxygenSaturation,
            Temperature = request.Temperature,
            RespiratoryRate = request.RespiratoryRate
        };

        _context.VitalSigns.Add(vitalSign);

        await _context.SaveChangesAsync();

        var response = new VitalSignResponse
        {
            Id = vitalSign.Id,
            PatientId = vitalSign.PatientId,
            RecordedAt = vitalSign.RecordedAt,
            HeartRate = vitalSign.HeartRate,
            SystolicBloodPressure = vitalSign.SystolicBloodPressure,
            DiastolicBloodPressure = vitalSign.DiastolicBloodPressure,
            OxygenSaturation = vitalSign.OxygenSaturation,
            Temperature = vitalSign.Temperature,
            RespiratoryRate = vitalSign.RespiratoryRate
        };

        return CreatedAtAction(
            nameof(GetVitalSign),
            new { id = vitalSign.Id },
            response);
    }

    // Updates an existing vital-sign measurement.
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateVitalSign(
        int id,
        UpdateVitalSignRequest request)
    {
            // Validates the incoming update request.
    var validationResult = await _updateVitalSignValidator.ValidateAsync(request);

    if (!validationResult.IsValid)
    {
        return BadRequest(validationResult.Errors);
    }
        var vitalSign = await _context.VitalSigns
            .FirstOrDefaultAsync(vitalSign => vitalSign.Id == id);

        if (vitalSign is null)
        {
            return NotFound(new
            {
                message = "Vital sign not found."
            });
        }

        vitalSign.RecordedAt = request.RecordedAt;
        vitalSign.HeartRate = request.HeartRate;
        vitalSign.SystolicBloodPressure = request.SystolicBloodPressure;
        vitalSign.DiastolicBloodPressure = request.DiastolicBloodPressure;
        vitalSign.OxygenSaturation = request.OxygenSaturation;
        vitalSign.Temperature = request.Temperature;
        vitalSign.RespiratoryRate = request.RespiratoryRate;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // Deletes an existing vital-sign measurement.
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteVitalSign(int id)
    {
        var vitalSign = await _context.VitalSigns
            .FirstOrDefaultAsync(vitalSign => vitalSign.Id == id);

        if (vitalSign is null)
        {
            return NotFound(new
            {
                message = "Vital sign not found."
            });
        }

        _context.VitalSigns.Remove(vitalSign);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}
