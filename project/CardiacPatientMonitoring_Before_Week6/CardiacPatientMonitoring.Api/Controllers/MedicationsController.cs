using CardiacPatientMonitoring.Api.Data;
using CardiacPatientMonitoring.Api.DTOs;
using CardiacPatientMonitoring.Api.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CardiacPatientMonitoring.Api.Controllers;

// Handles CRUD operations for medications.
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MedicationsController : ControllerBase
{
    private readonly CardiacPatientMonitoringDbContext _context;
    private readonly IValidator<CreateMedicationRequest> _createMedicationValidator;
    private readonly IValidator<UpdateMedicationRequest> _updateMedicationValidator;

    // Receives the database context and validators through dependency injection.
    public MedicationsController(
        CardiacPatientMonitoringDbContext context,
        IValidator<CreateMedicationRequest> createMedicationValidator,
        IValidator<UpdateMedicationRequest> updateMedicationValidator)
    {
        _context = context;
        _createMedicationValidator = createMedicationValidator;
        _updateMedicationValidator = updateMedicationValidator;
    }

    // Returns all medications.
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MedicationResponse>>> GetMedications()
    {
        var medications = await _context.Medications
            .AsNoTracking()
            .Select(medication => new MedicationResponse
            {
                Id = medication.Id,
                PatientId = medication.PatientId,
                Name = medication.Name,
                Dosage = medication.Dosage,
                Frequency = medication.Frequency,
                StartDate = medication.StartDate,
                EndDate = medication.EndDate
            })
            .ToListAsync();

        return Ok(medications);
    }

    // Returns one medication by ID.
    [HttpGet("{id:int}")]
    public async Task<ActionResult<MedicationResponse>> GetMedication(int id)
    {
        var medication = await _context.Medications
            .AsNoTracking()
            .Where(medication => medication.Id == id)
            .Select(medication => new MedicationResponse
            {
                Id = medication.Id,
                PatientId = medication.PatientId,
                Name = medication.Name,
                Dosage = medication.Dosage,
                Frequency = medication.Frequency,
                StartDate = medication.StartDate,
                EndDate = medication.EndDate
            })
            .FirstOrDefaultAsync();

        if (medication is null)
        {
            return NotFound(new
            {
                message = "Medication not found."
            });
        }

        return Ok(medication);
    }

    // Returns all medications for a specific patient.
    [HttpGet("patient/{patientId:guid}")]
    public async Task<ActionResult<IEnumerable<MedicationResponse>>> GetPatientMedications(
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

        var medications = await _context.Medications
            .AsNoTracking()
            .Where(medication => medication.PatientId == patientId)
            .OrderByDescending(medication => medication.StartDate)
            .Select(medication => new MedicationResponse
            {
                Id = medication.Id,
                PatientId = medication.PatientId,
                Name = medication.Name,
                Dosage = medication.Dosage,
                Frequency = medication.Frequency,
                StartDate = medication.StartDate,
                EndDate = medication.EndDate
            })
            .ToListAsync();

        return Ok(medications);
    }

    // Creates a new medication record.
    [HttpPost]
    public async Task<ActionResult<MedicationResponse>> CreateMedication(
        CreateMedicationRequest request)
    {
        // Validates the incoming create request.
        var validationResult = await _createMedicationValidator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        // Checks whether the patient exists.
        var patientExists = await _context.Patients
            .AnyAsync(patient => patient.Id == request.PatientId);

        if (!patientExists)
        {
            return NotFound(new
            {
                message = "Patient not found."
            });
        }

        var medication = new Medication
        {
            PatientId = request.PatientId,
            Name = request.Name,
            Dosage = request.Dosage,
            Frequency = request.Frequency,
            StartDate = request.StartDate,
            EndDate = request.EndDate
        };

        _context.Medications.Add(medication);

        await _context.SaveChangesAsync();

        var response = new MedicationResponse
        {
            Id = medication.Id,
            PatientId = medication.PatientId,
            Name = medication.Name,
            Dosage = medication.Dosage,
            Frequency = medication.Frequency,
            StartDate = medication.StartDate,
            EndDate = medication.EndDate
        };

        return CreatedAtAction(
            nameof(GetMedication),
            new { id = medication.Id },
            response);
    }

    // Updates an existing medication record.
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateMedication(
        int id,
        UpdateMedicationRequest request)
    {
        // Validates the incoming update request.
        var validationResult = await _updateMedicationValidator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var medication = await _context.Medications
            .FirstOrDefaultAsync(medication => medication.Id == id);

        if (medication is null)
        {
            return NotFound(new
            {
                message = "Medication not found."
            });
        }

        medication.Name = request.Name;
        medication.Dosage = request.Dosage;
        medication.Frequency = request.Frequency;
        medication.StartDate = request.StartDate;
        medication.EndDate = request.EndDate;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // Deletes an existing medication record.
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteMedication(int id)
    {
        var medication = await _context.Medications
            .FirstOrDefaultAsync(medication => medication.Id == id);

        if (medication is null)
        {
            return NotFound(new
            {
                message = "Medication not found."
            });
        }

        _context.Medications.Remove(medication);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}