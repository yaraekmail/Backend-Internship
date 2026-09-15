
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
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<IEnumerable<PatientResponse>>> GetPatients(
            int page = 1,
        int pageSize = 10,
        string? sort = null,
        string? gender = null,
        string? city = null)
    {
        // Validate pagination values.
        if (page < 1 || pageSize < 1)
        {
            return BadRequest("Page and pageSize must be greater than 0.");
        }

        // Build the query for patients.
        var query = _context.Patients
            .AsNoTracking();



        // Apply sorting based on the requested option.
        // Apply sorting based on the requested option.
        if (sort == "name")
        {
            query = query.OrderBy(patient => patient.LastName)
                         .ThenBy(patient => patient.FirstName);
        }
        else if (sort == "city")
        {
            query = query.OrderBy(patient => patient.City)
                         .ThenBy(patient => patient.LastName);
        }
        else
        {
            query = query.OrderBy(patient => patient.LastName)
                         .ThenBy(patient => patient.FirstName);
        }
        // Filter patients by gender when a value is provided.
        if (!string.IsNullOrWhiteSpace(gender))
        {
            query = query.Where(patient => patient.Gender == gender);
        }
        // Filter patients by city when a value is provided.
        if (!string.IsNullOrWhiteSpace(city))
        {
            query = query.Where(patient => patient.City == city);
        }
        // Count the total number of patients before pagination.
        var totalCount = await query.CountAsync();

        // Apply pagination.
        var patients = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
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


        // Return the patients together with pagination information.
        return Ok(new
        {
            page,
            pageSize,
            totalCount,
            patients
        });
    }

    // Returns one patient by ID.
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PatientResponse>> GetPatient(Guid id)
    {
        // Allow Admin users to access any patient.
        if (!User.IsInRole("Admin"))
        {
            var patientIdClaim = User.FindFirst("patientId")?.Value;

            if (!Guid.TryParse(patientIdClaim, out var currentPatientId))
            {
                return Forbid();
            }

            if (currentPatientId != id)
            {
                return Forbid();
            }
        }

        var patient = await _context.Patients.AsNoTracking()
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
    [Authorize(Roles = "Admin")]
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
        // Allow Admin users to update any patient.
        if (!User.IsInRole("Admin"))
        {
            var patientIdClaim = User.FindFirst("patientId")?.Value;

            if (!Guid.TryParse(patientIdClaim, out var currentPatientId))
            {
                return Forbid();
            }

            if (currentPatientId != id)
            {
                return Forbid();
            }
        }
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
    // Demonstrates the N+1 query problem by loading medications separately for each patient.
    [HttpGet("nplus1")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetPatientsWithMedicationsNPlusOne()
    {
        // Load all patients with one database query.
        var patients = await _context.Patients
            .AsNoTracking()
            .ToListAsync();

        var result = new List<object>();

        // Load medications separately for each patient.
        foreach (var patient in patients)
        {
            var medications = await _context.Medications
                .AsNoTracking()
                .Where(medication => medication.PatientId == patient.Id)
                .Select(medication => new
                {
                    medication.Id,
                    medication.Name,
                    medication.Dosage,
                    medication.Frequency
                })
                .ToListAsync();

            result.Add(new
            {
                patient.Id,
                patient.FirstName,
                patient.LastName,
                Medications = medications
            });
        }

        return Ok(result);
    }
    // Demonstrates an optimized approach that avoids the N+1 query problem.
    [HttpGet("optimized")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetPatientsWithMedicationsOptimized()
    {
        // Load all patients with one database query.
        var patients = await _context.Patients
            .AsNoTracking()
            .ToListAsync();

        var patientIds = patients
            .Select(patient => patient.Id)
            .ToList();

        // Load medications for all patients with one database query.
        var medications = await _context.Medications
            .AsNoTracking()
            .Where(medication => patientIds.Contains(medication.PatientId))
            .Select(medication => new
            {
                medication.PatientId,
                medication.Id,
                medication.Name,
                medication.Dosage,
                medication.Frequency
            })
            .ToListAsync();

        // Build the response using the data already loaded in memory.
        var result = patients.Select(patient => new
        {
            patient.Id,
            patient.FirstName,
            patient.LastName,
            Medications = medications
                .Where(medication => medication.PatientId == patient.Id)
                .Select(medication => new
                {
                    medication.Id,
                    medication.Name,
                    medication.Dosage,
                    medication.Frequency
                })
                .ToList()
        });

        return Ok(result);
    }
}
