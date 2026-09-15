using CardiacPatientMonitoring.Api.Data;
using CardiacPatientMonitoring.Api.DTOs;
using CardiacPatientMonitoring.Api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
namespace CardiacPatientMonitoring.Api.Controllers;

// Handles CRUD operations for appointments.
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AppointmentsController : ControllerBase
{
    private readonly CardiacPatientMonitoringDbContext _context;
private readonly IValidator<CreateAppointmentRequest> _createAppointmentValidator;
private readonly IValidator<UpdateAppointmentRequest> _updateAppointmentValidator;
    // Receives the database context and validators through dependency injection.
    public AppointmentsController(
        CardiacPatientMonitoringDbContext context,
        IValidator<CreateAppointmentRequest> createAppointmentValidator,
        IValidator<UpdateAppointmentRequest> updateAppointmentValidator)
    {
        _context = context;
        _createAppointmentValidator = createAppointmentValidator;
        _updateAppointmentValidator = updateAppointmentValidator;
    }

    // Returns all appointments.
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppointmentResponse>>> GetAppointments()
    {
        var appointments = await _context.Appointments
            .AsNoTracking()
            .OrderBy(appointment => appointment.AppointmentDate)
            .Select(appointment => new AppointmentResponse
            {
                Id = appointment.Id,
                PatientId = appointment.PatientId,
                AppointmentDate = appointment.AppointmentDate,
                Reason = appointment.Reason,
                Status = appointment.Status,
                Notes = appointment.Notes
            })
            .ToListAsync();

        return Ok(appointments);
    }

    // Returns one appointment by ID.
    [HttpGet("{id:int}")]
    public async Task<ActionResult<AppointmentResponse>> GetAppointment(int id)
    {
        var appointment = await _context.Appointments
            .AsNoTracking()
            .Where(appointment => appointment.Id == id)
            .Select(appointment => new AppointmentResponse
            {
                Id = appointment.Id,
                PatientId = appointment.PatientId,
                AppointmentDate = appointment.AppointmentDate,
                Reason = appointment.Reason,
                Status = appointment.Status,
                Notes = appointment.Notes
            })
            .FirstOrDefaultAsync();

        if (appointment is null)
        {
            return NotFound(new
            {
                message = "Appointment not found."
            });
        }

        return Ok(appointment);
    }

    // Returns all appointments for a specific patient.
    [HttpGet("patient/{patientId:guid}")]
    public async Task<ActionResult<IEnumerable<AppointmentResponse>>> GetPatientAppointments(
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

        var appointments = await _context.Appointments
            .AsNoTracking()
            .Where(appointment => appointment.PatientId == patientId)
            .OrderBy(appointment => appointment.AppointmentDate)
            .Select(appointment => new AppointmentResponse
            {
                Id = appointment.Id,
                PatientId = appointment.PatientId,
                AppointmentDate = appointment.AppointmentDate,
                Reason = appointment.Reason,
                Status = appointment.Status,
                Notes = appointment.Notes
            })
            .ToListAsync();

        return Ok(appointments);
    }

  // Creates a new appointment.
[HttpPost]
public async Task<ActionResult<AppointmentResponse>> CreateAppointment(
    CreateAppointmentRequest request)
{
    // Validates the incoming create request.
    var validationResult = await _createAppointmentValidator.ValidateAsync(request);

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
        var appointment = new Appointment
        {
            PatientId = request.PatientId,
            AppointmentDate = request.AppointmentDate,
            Reason = request.Reason,
            Status = request.Status,
            Notes = request.Notes
        };

        _context.Appointments.Add(appointment);

        await _context.SaveChangesAsync();

        var response = new AppointmentResponse
        {
            Id = appointment.Id,
            PatientId = appointment.PatientId,
            AppointmentDate = appointment.AppointmentDate,
            Reason = appointment.Reason,
            Status = appointment.Status,
            Notes = appointment.Notes
        };

        return CreatedAtAction(
            nameof(GetAppointment),
            new { id = appointment.Id },
            response);
    }
// Updates an existing appointment.
[HttpPut("{id:int}")]
public async Task<IActionResult> UpdateAppointment(
    int id,
    UpdateAppointmentRequest request)
{
    // Validates the incoming update request.
    var validationResult = await _updateAppointmentValidator.ValidateAsync(request);

    if (!validationResult.IsValid)
    {
        return BadRequest(validationResult.Errors);
    }

    var appointment = await _context.Appointments
        .FirstOrDefaultAsync(appointment => appointment.Id == id);

    if (appointment is null)
    {
        return NotFound(new
        {
            message = "Appointment not found."
        });
    }

        appointment.AppointmentDate = request.AppointmentDate;
        appointment.Reason = request.Reason;
        appointment.Status = request.Status;
        appointment.Notes = request.Notes;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // Deletes an existing appointment.
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteAppointment(int id)
    {
        var appointment = await _context.Appointments
            .FirstOrDefaultAsync(appointment => appointment.Id == id);

        if (appointment is null)
        {
            return NotFound(new
            {
                message = "Appointment not found."
            });
        }

        _context.Appointments.Remove(appointment);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}
