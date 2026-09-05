// Provides xUnit testing features.
using Xunit;

// Provides Moq for creating mock objects.
using Moq;

// Provides the Patient entity.
using CardiacPatientMonitoring.Api.Entities;

// Provides the PatientService class.
using CardiacPatientMonitoring.Api.Services;

// Provides the IPatientRepository interface.
using CardiacPatientMonitoring.Api.Repositories;

namespace CardiacPatientMonitoring.Tests;

// Contains unit tests for PatientService.
public class PatientServiceTests
{
    [Fact]
    public async Task GetPatientAsync_WhenPatientExists_ReturnsPatient()
    {
        // Arrange
        // Create a fake patient ID for the test.
        Guid patientId = Guid.NewGuid();

        // Create the patient that we want the mock repository to return.
        var expectedPatient = new Patient
        {
            Id = patientId,
            FirstName = "Ahmad",
            LastName = "Ali",
            DateOfBirth = new DateTime(2000, 1, 1),
            Gender = "Male"
        };

        // Create a mock version of the patient repository.
        var mockRepository = new Mock<IPatientRepository>();

        // Tell the mock what to return when GetByIdAsync is called with this ID.
        mockRepository
            .Setup(repository => repository.GetByIdAsync(patientId))
            .ReturnsAsync(expectedPatient);

        // Create the service and give it the mock repository instead of the real database repository.
        var service = new PatientService(mockRepository.Object);

        // Act
        // Call the method we actually want to test.
        var result = await service.GetPatientAsync(patientId);

        // Assert
        // Check that the service returned the patient provided by the mock.
        Assert.Equal(expectedPatient, result);
    }

    // Tests what happens when the repository throws an exception.
[Fact]
public async Task GetPatientAsync_WhenRepositoryThrowsException_ThrowsException()
{
    // Arrange
    // Create a patient ID for the test.
    Guid patientId = Guid.NewGuid();

    // Create a mock version of the patient repository.
    var mockRepository = new Mock<IPatientRepository>();

    // Tell the mock to throw an exception when GetByIdAsync is called.
    mockRepository
        .Setup(repository => repository.GetByIdAsync(patientId))
        .ThrowsAsync(new InvalidOperationException("Database error"));

    // Create the service using the mock repository.
    var service = new PatientService(mockRepository.Object);

    // Act & Assert
    // Verify that the expected exception is thrown.
    await Assert.ThrowsAsync<InvalidOperationException>(
        () => service.GetPatientAsync(patientId));
}

// Verifies that the service calls the repository exactly once.
[Fact]
public async Task GetPatientAsync_CallsRepositoryExactlyOnce()
{
    // Arrange
    // Create a patient ID for the test.
    Guid patientId = Guid.NewGuid();

    // Create a mock repository.
    var mockRepository = new Mock<IPatientRepository>();

    // Tell the mock to return null for this patient ID.
    mockRepository
        .Setup(repository => repository.GetByIdAsync(patientId))
        .ReturnsAsync((Patient?)null);

    // Create the service using the mock repository.
    var service = new PatientService(mockRepository.Object);

    // Act
    // Call the service method.
    await service.GetPatientAsync(patientId);

    // Assert
    // Verify that GetByIdAsync was called exactly once with the expected ID.
    mockRepository.Verify(
        repository => repository.GetByIdAsync(patientId),
        Times.Once);
}
}