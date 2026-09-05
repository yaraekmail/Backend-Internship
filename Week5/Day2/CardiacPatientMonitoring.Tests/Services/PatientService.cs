using CardiacPatientMonitoring.Api.Entities;
using CardiacPatientMonitoring.Api.Repositories;

namespace CardiacPatientMonitoring.Api.Services;

// Contains business logic related to patients.
public class PatientService
{
    private readonly IPatientRepository _patientRepository;

    // Receives the repository through dependency injection.
    public PatientService(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    // Gets a patient by ID through the repository.
    public async Task<Patient?> GetPatientAsync(Guid id)
    {
        return await _patientRepository.GetByIdAsync(id);
    }
}