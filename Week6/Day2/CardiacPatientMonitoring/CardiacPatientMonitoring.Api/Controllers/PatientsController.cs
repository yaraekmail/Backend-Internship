
using CardiacPatientMonitoring.Api.Data;
using CardiacPatientMonitoring.Api.DTOs;
using CardiacPatientMonitoring.Api.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CardiacPatientMonitoring.Api.Controllers;

// Handles CRUD operations for patients.
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PatientsController : ControllerBase
{
    private readonly CardiacPatientMonitoringDbContext _context;

    // Validators for create and update patient requests.
    private readonly IValidator<CreatePatientRequest> _createPatientValidator;
    private readonly IValidator<UpdatePatientRequest> _updatePatientValidator;

    // Receives the database context and validators through dependency injection.
    public PatientsController(
        CardiacPatientMonitoringDbContext context,
        IValidator<CreatePatientRequest> createPatientValidator,
        IValidator<UpdatePatientRequest> updatePatientValidator)
    {
        _context = context;
        _createPatientValidator = createPatientValidator;
        _updatePatientValidator = updatePatientValidator;
    }

    // Returns all patients.
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PatientResponse>>> GetPatients()
    {
        var patients = await _context.Patients
            .AsNoTracking()
            .Select(patient => new PatientResponse
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                DateOfBirth = patient.DateOfBirth,
                Gender = patient.Gender,
                Phone = patient.Phone,
                Email = patient.Email,
                Address = patient.Address,
                City = patient.City,
                State = patient.State
            })
            .ToListAsync();

        return Ok(patients);
    }

    // Returns one patient by ID.
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PatientResponse>> GetPatient(Guid id)
    {
        var patient = await _context.Patients
            .AsNoTracking()
            .Where(patient => patient.Id == id)
            .Select(patient => new PatientResponse
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                DateOfBirth = patient.DateOfBirth,
                Gender = patient.Gender,
                Phone = patient.Phone,
                Email = patient.Email,
                Address = patient.Address,
                City = patient.City,
                State = patient.State
            })
            .FirstOrDefaultAsync();

        if (patient is null)
        {
            return NotFound(new
            {
                message = "Patient not found."
            });
        }

        return Ok(patient);
    }

    // Creates a new patient.
    [HttpPost]
    public async Task<ActionResult<PatientResponse>> CreatePatient(
        CreatePatientRequest request)
    {
        // Validates the incoming create request.
        var validationResult = await _createPatientValidator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var patient = new Patient
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            DateOfBirth = request.DateOfBirth,
            Gender = request.Gender,
            Phone = request.Phone,
            Email = request.Email,
            Address = request.Address,
            City = request.City,
            State = request.State
        };

        _context.Patients.Add(patient);

        await _context.SaveChangesAsync();

        var response = new PatientResponse
        {
            Id = patient.Id,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            DateOfBirth = patient.DateOfBirth,
            Gender = patient.Gender,
            Phone = patient.Phone,
            Email = patient.Email,
            Address = patient.Address,
            City = patient.City,
            State = patient.State
        };

        return CreatedAtAction(
            nameof(GetPatient),
            new { id = patient.Id },
            response);
    }

    // Updates an existing patient.
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdatePatient(
        Guid id,
        UpdatePatientRequest request)
    {
        // Validates the incoming update request.
        var validationResult = await _updatePatientValidator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var patient = await _context.Patients
            .FirstOrDefaultAsync(patient => patient.Id == id);

        if (patient is null)
        {
            return NotFound(new
            {
                message = "Patient not found."
            });
        }

        patient.FirstName = request.FirstName;
        patient.LastName = request.LastName;
        patient.DateOfBirth = request.DateOfBirth;
        patient.Gender = request.Gender;
        patient.Phone = request.Phone;
        patient.Email = request.Email;
        patient.Address = request.Address;
        patient.City = request.City;
        patient.State = request.State;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // Deletes an existing patient.
    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeletePatient(Guid id)
    {
        var patient = await _context.Patients
            .FirstOrDefaultAsync(patient => patient.Id == id);

        if (patient is null)
        {
            return NotFound(new
            {
                message = "Patient not found."
            });
        }

        _context.Patients.Remove(patient);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}
