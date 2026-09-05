using CardiacPatientMonitoring.Api.Entities;

namespace CardiacPatientMonitoring.Api.Repositories;

// Defines the operations that any patient repository must provide.
public interface IPatientRepository
{
    // Gets a patient by their ID.
    Task<Patient?> GetByIdAsync(Guid id);
}