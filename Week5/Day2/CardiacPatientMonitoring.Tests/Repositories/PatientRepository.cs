using CardiacPatientMonitoring.Api.Data;
using CardiacPatientMonitoring.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace CardiacPatientMonitoring.Api.Repositories;

// This is the real repository that gets patient data from the database.
public class PatientRepository : IPatientRepository
{
    private readonly CardiacPatientMonitoringDbContext _context;

    // Receives the database context through dependency injection.
    public PatientRepository(CardiacPatientMonitoringDbContext context)
    {
        _context = context;
    }

    // Gets a patient from the database using the patient's ID.
    public async Task<Patient?> GetByIdAsync(Guid id)
    {
        return await _context.Patients.FindAsync(id);
    }
}